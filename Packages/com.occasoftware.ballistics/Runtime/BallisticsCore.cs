using System.Collections.Generic;

using UnityEngine;

namespace OccaSoftware.Ballistics.Runtime
{
    public static class BallisticsCore
    {
        public static BallisticsPrediction Predict(
            Projectile projectile,
            Environment environment,
            SimulationConfig simulationConfig
        )
        {
            if (projectile is null)
            {
                Debug.LogError("Projectile cannot be null. Prediction will not run.");
                return null;
            }

            if (environment is null)
            {
                Debug.LogError("Environment cannot be null. Prediction will not run.");
                return null;
            }

            if (simulationConfig is null)
            {
                Debug.LogError("Simulation State cannot be null. Prediction will not run");
                return null;
            }

            List<Kinematics> kinematicsHistory = new List<Kinematics>();

            Vector3 positionOrigin = projectile.Origin;
            Vector3 positionOld = positionOrigin;

            float distance = 0f;
            float timeElapsed = 0f;

            float timeDelta;
            switch (simulationConfig.UpdateFrequency)
            {
                case SimulationConfig.SimUpdateFrequency.CustomTiming:
                    timeDelta = simulationConfig.CustomUpdateFrequency;
                    break;
                case SimulationConfig.SimUpdateFrequency.OnEveryFrame:
                    timeDelta = 0.0167f;
                    break;
                case SimulationConfig.SimUpdateFrequency.OnFixedUpdate:
                    timeDelta = Time.fixedDeltaTime;
                    break;
                default:
                    timeDelta = 0.0167f;
                    break;
            }

            Kinematics kinematics = new Kinematics(projectile);

            while (distance < simulationConfig.MaxSimulationDistance)
            {
                Debug.Log(kinematics.Position);
                kinematics = ResolveKinematicsUpdate(
                    projectile,
                    kinematics,
                    environment,
                    timeDelta
                );
                timeElapsed += simulationConfig.CustomUpdateFrequency;
                distance = Vector3.Distance(kinematics.Position, positionOrigin);

                if (Physics.Linecast(positionOld, kinematics.Position, out RaycastHit hitInfo))
                {
                    hitInfo.distance =
                        Vector3.Distance(positionOld, positionOrigin) + hitInfo.distance;
                    return new BallisticsPrediction(
                        new BallisticsHitData(true, hitInfo, projectile, kinematics, timeElapsed),
                        kinematicsHistory
                    );
                }

                kinematicsHistory.Add(kinematics);
                positionOld = kinematics.Position;
            }

            return new BallisticsPrediction(
                new BallisticsHitData(false, projectile),
                kinematicsHistory
            );
        }

        internal static Kinematics ResolveKinematicsUpdate(
            Projectile projectile,
            Kinematics kinematics,
            Environment environment,
            float deltaTime
        )
        {
            Vector3 drag = SolveAccelerationFromDrag(
                kinematics.Velocity,
                projectile.BallisticCoefficient,
                environment.AirDensity,
                projectile.ProjectileMass,
                projectile.CrossSectionalArea,
                environment.SpeedOfSound
            );
            Vector3 acceleration = SolveAcceleration(drag, environment.GravityV3);
            Vector3 newPosition = SolveDisplacement(
                kinematics.Position,
                kinematics.Velocity,
                acceleration,
                deltaTime
            );
            Vector3 newVelocity = SolveVelocity(kinematics.Velocity, acceleration, deltaTime);
            return new Kinematics(newPosition, newVelocity, acceleration);
        }

        private static float GetReferenceDrag(float machNumber)
        {
            int machAdj = Mathf.RoundToInt(machNumber * 100);
            if (DragReferenceTables.g7.TryGetValue(machAdj, out float value))
            {
                return value;
            }

            return 0f;
        }

        // Returns acceleration
        private static Vector3 SolveAcceleration(Vector3 drag, Vector3 gravity)
        {
            Vector3 acceleration = gravity + drag;
            return acceleration;
        }

        private static float SolveMachNumber(Vector3 velocity, float speedOfSound)
        {
            return Vector3.Magnitude(velocity) / speedOfSound;
        }

        private static Vector3 SolveAccelerationFromDrag(
            Vector3 velocity,
            float ballisticCoefficient,
            float airDensity,
            float projectileMass,
            float crossSectionalArea,
            float speedOfSound = 340.2f
        )
        {
            float mach = SolveMachNumber(velocity, speedOfSound);

            const float multiplier = 2.85f;
            float coefficientDrag =
                (1f / ballisticCoefficient) * GetReferenceDrag(mach) * multiplier;
            float forceDrag =
                coefficientDrag * 0.5f * airDensity * velocity.sqrMagnitude * crossSectionalArea;

            Vector3 signedForceDrag = forceDrag * -velocity.normalized;
            Vector3 accelerationDrag = signedForceDrag / projectileMass;
            return accelerationDrag;
        }

        private static Vector3 SolveDisplacement(
            Vector3 position,
            Vector3 velocity,
            Vector3 acceleration,
            float deltaTime
        )
        {
            return (position)
                + (velocity * deltaTime)
                + ((deltaTime * deltaTime) * 0.5f * acceleration);
        }

        private static Vector3 SolveVelocity(
            Vector3 velocity,
            Vector3 acceleration,
            float deltaTime
        )
        {
            return velocity + acceleration * deltaTime;
        }

        internal static float CalculateKineticEnergy(float mass, float speed)
        {
            return 0.5f * mass * speed * speed;
        }
    }

    public class BallisticsPrediction
    {
        internal BallisticsPrediction(
            BallisticsHitData ballisticsHitData,
            List<Kinematics> kinematics
        )
        {
            BallisticsHitData = ballisticsHitData;
            Kinematics = kinematics;
        }

        public BallisticsHitData BallisticsHitData { get; }
        public List<Kinematics> Kinematics { get; }
    }

    public class BallisticsHitData
    {
        internal BallisticsHitData(bool didHit, Projectile projectile)
        {
            DidHit = didHit;
            Projectile = projectile;
        }

        internal BallisticsHitData(
            bool didHit,
            RaycastHit hitInfo,
            Projectile projectile,
            Kinematics kinematics,
            float timeInFlight
        )
        {
            DidHit = didHit;
            HitInfo = hitInfo;
            Projectile = projectile;
            Details = new Details(projectile.ProjectileMass, kinematics.Velocity, timeInFlight);
        }

        public bool DidHit { get; }

        public RaycastHit HitInfo { get; }

        public Projectile Projectile { get; }

        public Details Details { get; }
    }

    public struct Details
    {
        internal Details(float projectileMass, Vector3 velocity, float timeInFlight)
        {
            SpeedAtImpact = Vector3.Magnitude(velocity);
            KineticEnergyAtImpact = BallisticsCore.CalculateKineticEnergy(
                projectileMass,
                SpeedAtImpact
            );
            TimeInFlight = timeInFlight;
        }

        public float TimeInFlight { get; }

        public float SpeedAtImpact { get; }

        public float KineticEnergyAtImpact { get; }
        // Measured in Joules.
    }

    [System.Serializable]
    public class Environment : ISerializationCallbackReceiver
    {
        public Environment()
        {
            AirDensity = 1.225f;
            SpeedOfSound = 340.2f;
        }

        public Environment(
            float temperatureCelsius = 15f,
            float relativeHumidity = 0f,
            float airPressureAtmospheres = 1f,
            float gravity = -9.807f
        )
        {
            AirPressureAtmospheres = airPressureAtmospheres;
            RelativeHumidity = relativeHumidity;
            Temperature = temperatureCelsius;

            Validate();
            Recalculate();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() => OnValidate();

        void ISerializationCallbackReceiver.OnAfterDeserialize() { }

        private void OnValidate()
        {
            Validate();
            Recalculate();
        }

        private void Validate()
        {
            RelativeHumidity = Mathf.Clamp(RelativeHumidity, 0, 100);
            AirPressureAtmospheres = Mathf.Max(AirPressureAtmospheres, 0);
            Temperature = Mathf.Max(Temperature, -273.15f);
        }

        private void Recalculate()
        {
            SpeedOfSound = SolveSpeedOfSound(Temperature);
            AirDensity = CalculateAirDensity(Temperature, RelativeHumidity, AirPressurePascals);
        }

        public float Gravity = -9.807f;
        public Vector3 GravityV3
        {
            get { return new Vector3(0, Gravity, 0); }
        }

        // m/s^2
        // Standard of -9.807m/s^2 on earth at sea level.

        public float AirDensity = 0f;

        // kg/m^3
        // Standard of 1.225kg/m3 for dry air at sea level at 15C and 14.7 psi.

        public float AirPressureAtmospheres = 1f;

        public float Temperature = 15f;

        // Celsius

        public float SpeedOfSound = 340.2f;

        public float RelativeHumidity = 0f;

        private float AirPressurePascals
        {
            get { return AirPressureAtmospheres * 101325; }
        }

        // Temperature should be provided in degrees C
        private float SolveSpeedOfSound(float airTemperatureCelsius = 15f)
        {
            return 331.3f * Mathf.Sqrt(1 + airTemperatureCelsius / 273.15f);
        }

        private float CalculateSaturationVaporPressure(float temperatureCelsius)
        {
            return 6.1078f
                * Mathf.Pow(10.0f, 7.5f * temperatureCelsius / (temperatureCelsius + 237.3f));
        }

        private float CalculateActualVaporPressure(
            float saturationVaporPressure,
            float relativeHumidity
        )
        {
            return saturationVaporPressure * relativeHumidity;
        }

        private float CalculateDryAirPressure(float actualVaporPressure, float totalAirPressure)
        {
            return totalAirPressure - actualVaporPressure;
        }

        private float CalculateAirDensityFromPressure(
            float dryAirPressure,
            float actualVaporPressure,
            float temperatureCelsius
        )
        {
            const float dryAirSpecificGasConstant = 287.058f;
            const float waterVaporSpecificGasConstant = 461.495f;

            float temperatureKelvin = temperatureCelsius + 273.15f;
            return (dryAirPressure / (dryAirSpecificGasConstant * temperatureKelvin))
                + (actualVaporPressure / (waterVaporSpecificGasConstant * temperatureKelvin));
        }

        private float CalculateAirDensity(
            float temperatureCelsius,
            float relativeHumidity,
            float airPressure
        )
        {
            float saturationVaporPressure = CalculateSaturationVaporPressure(temperatureCelsius);
            float actualVaporPressure = CalculateActualVaporPressure(
                saturationVaporPressure,
                relativeHumidity
            );
            float dryAirPressure = CalculateDryAirPressure(actualVaporPressure, airPressure);
            return CalculateAirDensityFromPressure(
                dryAirPressure,
                actualVaporPressure,
                temperatureCelsius
            );
        }
    }

    [System.Serializable]
    public class SimulationConfig : ISerializationCallbackReceiver
    {
        public SimulationConfig(
            int maxSimulationDistance = 1000,
            SimUpdateFrequency updateFrequency = SimUpdateFrequency.CustomTiming,
            float customUpdateFrequency = 0.01f
        )
        {
            MaxSimulationDistance = maxSimulationDistance;
            UpdateFrequency = updateFrequency;
            CustomUpdateFrequency = customUpdateFrequency;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() => OnValidate();

        void ISerializationCallbackReceiver.OnAfterDeserialize() { }

        public int MaxSimulationDistance = 1000;
        public SimUpdateFrequency UpdateFrequency = SimUpdateFrequency.CustomTiming;
        public float CustomUpdateFrequency = 0.01f;

        private void OnValidate()
        {
            MaxSimulationDistance = Mathf.Max(0, MaxSimulationDistance);
            CustomUpdateFrequency = Mathf.Max(0, CustomUpdateFrequency);
        }

        public enum SimUpdateFrequency
        {
            CustomTiming,
            OnEveryFrame,
            OnFixedUpdate
        }
    }

    [System.Serializable]
    public class Projectile : ISerializationCallbackReceiver
    {
        public Projectile()
        {
            Origin = Vector3.zero;
            Direction = Vector3.zero;
            Validate();
            Recalculate();
        }

        public Projectile(
            Vector3 origin,
            Vector3 direction,
            float muzzleVelocity = 961f,
            float ballisticCoefficient = 0.151f,
            float crossSectionalArea = 0.00002427948f,
            float projectileMass = 0.062f
        )
        {
            Origin = origin;
            Direction = direction;
            MuzzleVelocity = muzzleVelocity;
            BallisticCoefficient = ballisticCoefficient;
            CrossSectionalArea = crossSectionalArea;
            ProjectileMass = projectileMass;
            Validate();
            Recalculate();
        }

        public Vector3 Origin = Vector3.zero;
        public Vector3 Direction = Vector3.zero;

        public void SetOriginAndDirection(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public float MuzzleVelocity = 961f;

        // Unit: m/s

        public float InitialKineticEnergy = 0f;

        // Unit: Joules

        public float BallisticCoefficient = 0.151f;

        // Unit: kg/m^2

        public float CrossSectionalArea = 0.00002427948f;

        // Unit: m^2

        public float ProjectileMass = 0.062f;

        // Unit: kg


        void ISerializationCallbackReceiver.OnBeforeSerialize() => OnValidate();

        void ISerializationCallbackReceiver.OnAfterDeserialize() { }

        private void OnValidate()
        {
            Validate();
            Recalculate();
        }

        private void Validate()
        {
            MuzzleVelocity = Mathf.Max(MuzzleVelocity, 0f);
            BallisticCoefficient = Mathf.Max(BallisticCoefficient, 0f);
            CrossSectionalArea = Mathf.Max(CrossSectionalArea, 0f);
            ProjectileMass = Mathf.Max(ProjectileMass, 0f);
        }

        private void Recalculate()
        {
            InitialKineticEnergy = BallisticsCore.CalculateKineticEnergy(
                ProjectileMass,
                MuzzleVelocity
            );
        }
    }

    public struct Kinematics
    {
        internal Kinematics(Vector3 position, Vector3 velocity, Vector3 acceleration)
        {
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
        }

        internal Kinematics(Projectile projectile)
        {
            Position = projectile.Origin;
            Velocity = projectile.Direction * projectile.MuzzleVelocity;
            Acceleration = Vector3.zero;
        }

        public Vector3 Position { get; }
        public Vector3 Velocity { get; }
        public Vector3 Acceleration { get; }
    }
}

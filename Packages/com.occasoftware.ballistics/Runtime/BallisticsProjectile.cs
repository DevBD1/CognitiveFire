using System.Collections;
using UnityEngine;

namespace OccaSoftware.Ballistics.Runtime
{
    public class BallisticsProjectile : MonoBehaviour
    {
        protected System.Action<BallisticsHitData> onBallisticsComplete = null;
        protected System.Action<Kinematics> onBallisticsUpdate = null;
        [SerializeField] protected ProjectileSO projectileSO = null;
        [SerializeField] protected EnvironmentSO environmentSO = null;
        [SerializeField] protected SimulationConfigSO simulationStateSO = null;

        public Projectile Projectile 
        { 
            get 
            {
                if(projectileSO is not null)
                    return projectileSO.projectile;

                return null;
            }
            set
            {
                Projectile = value;
            }
        }

        public Environment Environment 
        { 
            get 
            { 
                if(environmentSO is not null)
                    return environmentSO.environment;

                return null;
            }
            set
            {
                Environment = value;
            }
        }
        
        public SimulationConfig SimulationConfig 
        { 
            get 
            { 
                if(simulationStateSO is not null)
                    return simulationStateSO.simulationConfig;
                
                return null;
            }
            set
            {
                SimulationConfig = value;
            }
        }


        protected void Simulate()
		{
            StartCoroutine(SimulateCoroutine(Projectile, Environment, SimulationConfig, onBallisticsComplete, onBallisticsUpdate));
		}

        protected void Simulate(Projectile projectile, Environment environment, SimulationConfig simulationConfig, System.Action<BallisticsHitData> onBallisticsComplete, System.Action<Kinematics> onBallisticsUpdate)
		{
            StartCoroutine(SimulateCoroutine(projectile, environment, simulationConfig, onBallisticsComplete, onBallisticsUpdate));
        }


        private IEnumerator SimulateCoroutine(Projectile projectile, Environment environment, SimulationConfig simulationConfig, System.Action<BallisticsHitData> onBallisticsComplete, System.Action<Kinematics> onBallisticsUpdate)
        {
            if (Projectile is null)
            {
                Debug.LogError("Projectile cannot be null. Prediction will not run.");
                onBallisticsComplete(new BallisticsHitData(false, null));
                yield break;
            }

            if (Environment is null)
            {
                Debug.LogError("Environment cannot be null. Prediction will not run.");
                onBallisticsComplete(new BallisticsHitData(false, null));
                yield break;
            }

            if (SimulationConfig is null)
            {
                Debug.LogError("Simulation State cannot be null. Prediction will not run");
                onBallisticsComplete(new BallisticsHitData(false, null));
                yield break;
            }


            Vector3 positionOrigin, positionOld;
            positionOrigin = positionOld = Projectile.Origin;

            float timeOrigin, timeOld;
            timeOrigin = timeOld = Time.time;

            float distance = 0;

            Kinematics kinematics = new Kinematics(Projectile);

            while (distance < SimulationConfig.MaxSimulationDistance)
            {
                float timeDelta = Mathf.Max(0, Time.time - timeOld);
                kinematics = BallisticsCore.ResolveKinematicsUpdate(Projectile, kinematics, Environment, timeDelta);
                distance = Vector3.Distance(kinematics.Position, positionOrigin);

                if (Physics.Linecast(positionOld, kinematics.Position, out RaycastHit hitInfo))
                {
                    hitInfo.distance = Vector3.Distance(positionOld, positionOrigin) + hitInfo.distance;

                    if (onBallisticsComplete != null)
                    {
                        var ballistics = new BallisticsHitData(true, hitInfo, Projectile, kinematics, Time.time - timeOrigin);
                        // Note that the hit data is provided using Kinematic data that would not necessarily have been reached.
                        // (i.e., the velocity would be somewhere between the previous Velocity and the current Velocity.

                        onBallisticsComplete(ballistics);
                        yield break;
                    }
                }

                // We update the Kinematics after the Linecast because we don't want to provide Kinematic data that would not have been reached.
                if (onBallisticsUpdate != null)
                {
                    onBallisticsUpdate(kinematics);
                }

                timeOld = Time.time;
                positionOld = kinematics.Position;

                switch (SimulationConfig.UpdateFrequency)
                {
                    case SimulationConfig.SimUpdateFrequency.CustomTiming:
                        yield return new WaitForSeconds(SimulationConfig.CustomUpdateFrequency);
                        break;
                    case SimulationConfig.SimUpdateFrequency.OnFixedUpdate:
                        yield return new WaitForFixedUpdate();
                        break;
                    case SimulationConfig.SimUpdateFrequency.OnEveryFrame:
                        yield return null;
                        break;
                    default:
                        yield return new WaitForFixedUpdate();
                        break;
                }
            }

            onBallisticsComplete(new BallisticsHitData(false, Projectile));
        }

    }
}

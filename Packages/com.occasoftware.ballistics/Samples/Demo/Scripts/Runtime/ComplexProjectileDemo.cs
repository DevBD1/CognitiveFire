using OccaSoftware.Ballistics.Runtime;
using UnityEngine;


namespace OccaSoftware.Ballistics.Demo
{
    // This is a demo of how you might integrate Ballistics into your existing Projectile system.
    // This demo represents a custom projectile and custom environment with callbacks for both update and hit data using detailed classes from Inspector
    public class ComplexProjectileDemo : BallisticsProjectile
    {

        [SerializeField] GameObject onStartParticleSystem = null;
        GameObject onStartGO = null;
        [SerializeField] GameObject onHitParticleSystem = null;


        private void Start()
        {
            SimulateBallistics();
            CreateOnStartParticles();
        }

        private void SimulateBallistics()
        {
            Projectile.SetOriginAndDirection(transform.position, transform.forward);

            onBallisticsComplete += GetSimResults;
            onBallisticsUpdate += GetSimUpdate;

            Simulate(Projectile, Environment, SimulationConfig, onBallisticsComplete, onBallisticsUpdate);
        }

        private void CreateOnStartParticles()
		{
            onStartGO = Instantiate(onStartParticleSystem, transform.position, Quaternion.identity);
		}

        private void UpdateOnStartParticles(Vector3 newPosition)
		{
            if(onStartGO != null)
                onStartGO.transform.position = newPosition;
		}

        private void CreateOnHitParticles(Vector3 hitPoint)
		{
            Instantiate(onHitParticleSystem, hitPoint, Quaternion.identity);
		}


        // Receiving the results callback allows you to identify what object was hit by the projectile
        // This includes a RaycastHit struct with all the normal RaycastHit data along with additional data like kinetic energy at time of hit, elapsed time, etc.
        // You can use this information to assess damage to the target unit, spawn particles, etc..
        private void GetSimResults(BallisticsHitData ballisticsHitData)
        {
            
            if(ballisticsHitData.DidHit)
            {
                // Instantiate(impactVFX, ballisticsHitData.HitInfo.point);
                // ballisticsHitData.HitInfo.collider.gameObject.TryGetComponent<...>().DoDamage();

                float relativeKineticEnergy = ballisticsHitData.Details.KineticEnergyAtImpact / ballisticsHitData.Projectile.InitialKineticEnergy;
                Debug.Log($"Impact Point: {ballisticsHitData.HitInfo.point:0}. Relative Kinetic Energy at Impact: {relativeKineticEnergy:0.00}");
                
                UpdateOnStartParticles(ballisticsHitData.HitInfo.point);
                CreateOnHitParticles(ballisticsHitData.HitInfo.point);
            }
                
            onBallisticsComplete -= GetSimResults;
            onBallisticsUpdate -= GetSimUpdate;
            Destroy(gameObject);
        }


        // Receiving Update callbacks can allow you to update positional data in real time, as the projectile moves through space.
        // You could, for example, use this data to move a particle system each update frame or report kinematics data to a shader.
        private void GetSimUpdate(Kinematics kinematics)
        {
            transform.position = kinematics.Position;
            UpdateOnStartParticles(kinematics.Position);
        }


        private void OnDisable()
        {
            onBallisticsComplete -= GetSimResults;
            onBallisticsUpdate -= GetSimUpdate;
        }
    }
}

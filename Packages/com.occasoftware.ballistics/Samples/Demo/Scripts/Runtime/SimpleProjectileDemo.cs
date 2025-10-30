using OccaSoftware.Ballistics.Runtime;

namespace OccaSoftware.Ballistics.Demo
{
    // This is a demo of how you might integrate Ballistics into your existing Projectile system.
    // This demo represents a simple projectile with callbacks for hit data only and using barebones class constructors

    public class SimpleProjectileDemo : BallisticsProjectile
    {
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            Projectile.SetOriginAndDirection(transform.position, transform.forward);
            onBallisticsComplete += BallisticsResults;
            Simulate(Projectile, Environment, SimulationConfig, onBallisticsComplete, onBallisticsUpdate);
        }


        // Receiving the results callback allows you to identify what object was hit by the projectile
        // This includes a RaycastHit struct with all the normal RaycastHit data along with additional data like kinetic energy at time of hit, elapsed time, etc.
        // You can use this information to assess damage to the target unit, spawn particles, etc..
        private void BallisticsResults(BallisticsHitData ballisticsHitData)
        {
            if(ballisticsHitData.DidHit)
            {
                // This is where you know that the ballistic simulation is returning a hit on a collider. 
                // Do things like Instantiate hit vfx, do damage, etc.
                // Instantiate(impactVFX, ballisticsHitData.HitInfo.point);
                // ballisticsHitData.HitInfo.collider.gameObject.TryGetComponent<...>().DoDamage();
            }


            // Make sure to unsubscribe from the Action.
            onBallisticsComplete -= BallisticsResults;
            Destroy(gameObject);
        }


        private void OnDisable()
        {
            onBallisticsComplete -= BallisticsResults;
        }
    }
}

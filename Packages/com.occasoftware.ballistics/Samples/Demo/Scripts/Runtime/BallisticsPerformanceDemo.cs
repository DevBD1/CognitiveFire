using UnityEngine;
using OccaSoftware.Ballistics.Runtime;

namespace OccaSoftware.Ballistics.Demo
{
    public class BallisticsPerformanceDemo : MonoBehaviour
    {
        [SerializeField] private bool runPrediction = false;
        [SerializeField] private bool runSimulation = false;
        [SerializeField, Min(1)] private int testCount = 1;
        [SerializeField] private GameObject projectile = null;


        private void OnValidate()
        {
            if (runPrediction)
            {
                if (Application.isPlaying)
                    RunPrediction();
                runPrediction = !runPrediction;
            }

            if (runSimulation)
            {
                if(Application.isPlaying)
                    RunSimulation();
                runSimulation = !runSimulation;
            }
        }


        private void RunPrediction()
        {
            for(int i= 0; i < testCount; i++)
            {
                if(projectile.TryGetComponent(out BallisticsProjectile p))
                {
                    BallisticsPrediction prediction = BallisticsCore.Predict(p.Projectile, p.Environment, p.SimulationConfig);
                    if (prediction is not null)
                    {
                        if (prediction.BallisticsHitData.DidHit)
                        {
                            Debug.Log($"Hit {prediction.BallisticsHitData.HitInfo.collider.name}. Predicted Hit Point: {prediction.BallisticsHitData.HitInfo.point}.");
                        }
                        else
                        {
                            Debug.Log($"Did not hit any colliders. Last sampled point: {prediction.Kinematics[prediction.Kinematics.Count - 1].Position}.");
                        }
                    }
                }
            }
        }


        private void RunSimulation()
        {
            for(int i = 0; i < testCount; i++)
            {
                Instantiate(projectile, transform.position, transform.rotation);
            }
        }
    }
}

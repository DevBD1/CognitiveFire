using UnityEngine;

namespace OccaSoftware.Ballistics.Runtime
{
    [CreateAssetMenu(fileName = "SimulationConfig", menuName = "Ballistics/SimulationConfig")]
    public class SimulationConfigSO : ScriptableObject
    {
        public SimulationConfig simulationConfig = new SimulationConfig();
    }
}

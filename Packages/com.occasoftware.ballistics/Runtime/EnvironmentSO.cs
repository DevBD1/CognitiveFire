using UnityEngine;

namespace OccaSoftware.Ballistics.Runtime
{
    [CreateAssetMenu(fileName ="Environment", menuName = "Ballistics/Environment")]
    public class EnvironmentSO : ScriptableObject
    {
        public Environment environment = new Environment();
    }
}

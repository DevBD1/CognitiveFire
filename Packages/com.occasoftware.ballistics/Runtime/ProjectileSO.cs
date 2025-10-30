using UnityEngine;

namespace OccaSoftware.Ballistics.Runtime
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Ballistics/Projectile")]
    public class ProjectileSO : ScriptableObject
    {
        public Projectile projectile = new Projectile();
    }
}

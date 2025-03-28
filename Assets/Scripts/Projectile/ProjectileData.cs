using UnityEngine;

namespace SpaceInvadersClone.Projectiles
{
    [CreateAssetMenu(fileName = "NewProjectile", menuName = "BCS Interview Project/ProjectileData")]
    public class ProjectileData : ScriptableObject
    {
        [field: SerializeField] public Vector3 Direction { get; set; } = Vector2.down;
        [field: SerializeField] public float Speed { get; set; } = 1;

        [field: SerializeField, Tooltip("Objects with those layers won't be destroyed by this projectile")]
        public LayerMask UndestructableLayers { get; private set; }
    }
}

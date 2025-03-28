using UnityEngine;

namespace SpaceInvadersClone.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
    }
}

using UnityEngine;

namespace SpaceInvadersClone.Entities
{
    public abstract class EntityData : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}

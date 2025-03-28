using System;
using UnityEngine;

namespace SpaceInvadersClone.Entities
{
    public abstract class EntitySpawnManager<T> : MonoBehaviour where T : Entity
    {
        public event Action<T> OnSpawned = delegate {};

        [field: SerializeField] public int Columns {get; set; } = 11;

        protected EntitySpawner<T> spawner;

        public void NotifyOnSpawned(T entity)
        {
            OnSpawned.Invoke(entity);
        }

        public virtual T Spawn(Vector3 position, Transform parent = null)
        {
            return spawner.Spawn(position, parent);
        }
    }
}

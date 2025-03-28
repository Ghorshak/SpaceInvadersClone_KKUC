using SpaceInvadersClone.Entities;
using UnityEngine;

namespace SpaceInvadersClone.DesignPatterns.Factory
{
    public class EntityFactory<T> : IEntityFactory<T> where T : Entity
    {
        private EntityData data;

        public EntityFactory(EntityData data)
        {
            this.data = data;
        }

        public T Create(Vector3 spawnPoint, Transform parent = null)
        {
            var instance = GameObject.Instantiate(data.Prefab, spawnPoint, Quaternion.identity, parent);
            return instance.GetComponent<T>();
        }
    }
}

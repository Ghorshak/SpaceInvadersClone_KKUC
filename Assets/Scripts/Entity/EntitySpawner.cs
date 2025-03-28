using SpaceInvadersClone.DesignPatterns.Factory;
using UnityEngine;

namespace SpaceInvadersClone.Entities
{
    public class EntitySpawner<T> where T : Entity
    {
        private IEntityFactory<T> entityFactory;

        public EntitySpawner(IEntityFactory<T> entityFactory)
        {
            this.entityFactory = entityFactory;
        }

        public T Spawn(Vector3 position, Transform parent = null)
        {
            return entityFactory.Create(position, parent);
        }
    }
}

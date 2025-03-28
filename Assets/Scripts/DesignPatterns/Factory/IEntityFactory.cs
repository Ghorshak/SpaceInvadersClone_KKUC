using UnityEngine;
using SpaceInvadersClone.Entities;

namespace SpaceInvadersClone.DesignPatterns.Factory
{
    public interface IEntityFactory<T> where T : Entity
    {
        public T Create(Vector3 position, Transform parent = null);
    }
}

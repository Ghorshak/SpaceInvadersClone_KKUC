using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvadersClone.DesignPatterns.ObjectPooling
{
    public class ObjectPooler : MonoBehaviour
    {
        [field: SerializeField] private GameObject Prefab { get; set; }
        [field: SerializeField] private int Size { get; set; } = 10;
        [field: SerializeField] private Queue<GameObject> ObjectsInPool { get; set; } = new Queue<GameObject>();

        private Transform _transform;

        void Awake()
        {
            _transform = transform;

            for (int i = 0; i < Size; i++)
            {
                CreateNewPoolObject();
            }
        }

        private void CreateNewPoolObject()
        {
            GameObject newObject = Instantiate(Prefab, _transform);
            newObject.SetActive(false);

            ObjectsInPool.Enqueue(newObject);
        }

        public GameObject SpawnObjectFromPool(Vector3 position)
        {
            GameObject spawnedObject = ObjectsInPool.Dequeue();
            spawnedObject.SetActive(true);
            spawnedObject.transform.position = position;

            ObjectsInPool.Enqueue(spawnedObject);

            return spawnedObject;
        }
    }
}

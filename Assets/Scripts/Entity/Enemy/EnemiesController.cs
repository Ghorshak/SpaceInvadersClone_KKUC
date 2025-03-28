using System;
using System.Collections.Generic;
using System.Linq;
using SpaceInvadersClone.DesignPatterns.ObjectPooling;
using UnityEngine;

namespace SpaceInvadersClone.Entities.Enemies
{
    public class EnemiesController : MonoBehaviour
    {
        public static event Action<Enemy, bool> OnEnemyDestroyed = delegate {};

        [field: SerializeField] private EnemySwarmBehaviourData Data { get; set; }
        [field: SerializeField] private EnemySpawnManager SpawnManager { get; set; }
        [field: SerializeField] private BoxCollider2D SwarmCollider { get; set; } = new();
        [field: SerializeField] private ObjectPooler ProjectilesPool { get; set; }

        private Dictionary<int, List<Enemy>> EnemiesByColumnIndex { get; set; } = new();
        private EnemyMovementModule MovementModule = new();
        private EnemyAttackModule AttackModule = new();

        private void OnEnable()
        {
            SpawnManager.OnSpawned += RegisterEnemy;
        }

        private void RegisterEnemy(Enemy enemy)
        {
            if (EnemiesByColumnIndex.ContainsKey(enemy.columnIndex))
            {
                EnemiesByColumnIndex[enemy.columnIndex].Add(enemy);
            }
            else
            {
                EnemiesByColumnIndex.Add(enemy.columnIndex, new() {enemy} );
            }
            
            enemy.OnKill += UnregisterEnemy;
        }

        private void UnregisterEnemy(Enemy enemyKilled)
        {
            int columnIndex = enemyKilled.columnIndex;
            if (EnemiesByColumnIndex.TryGetValue(columnIndex, out var columnOfEnemies))
            {
                columnOfEnemies.Remove(enemyKilled);

                var wasLastEnemy = false;
                if (columnOfEnemies.Count == 0)
                {
                    EnemiesByColumnIndex.Remove(columnIndex);
                    wasLastEnemy = EnemiesByColumnIndex.Count == 0;
                    TryUpdateGameColumnState(wasLastEnemy);
                }

                OnEnemyDestroyed.Invoke(enemyKilled, wasLastEnemy);
            }
        }

        private void TryUpdateGameColumnState(bool waslastEnemy)
        {
            if (EnemiesByColumnIndex.Count > 0)
            {
                UpdateSwarmBounds();
            }
        }

        private void Start()
        {
            InitializeModules();
            UpdateSwarmBounds();
        }

        private void InitializeModules()
        {
            MovementModule.Initialize(Data.MovementBehaviour, transform);
            AttackModule.Initialize(Data.AttackBehaviour, EnemiesByColumnIndex, ProjectilesPool);
        }

        void UpdateSwarmBounds()
        {
            var baseCol = EnemiesByColumnIndex[EnemiesByColumnIndex.Keys.Min()].First().Collider.bounds;
            Bounds newBounds = new Bounds(baseCol.center, baseCol.size);
            newBounds.Encapsulate(EnemiesByColumnIndex[EnemiesByColumnIndex.Keys.Max()].Last().Collider.bounds);
            SwarmCollider.offset = newBounds.center - SwarmCollider.transform.position;
            SwarmCollider.size = newBounds.size;
        }

        private void Update()
        {
            MovementModule.UpdateState();
            AttackModule.UpdateState();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            MovementModule.AdvanceMovement();
        }

        private void OnDisable()
        {
            SpawnManager.OnSpawned -= RegisterEnemy;
        }
    }
}

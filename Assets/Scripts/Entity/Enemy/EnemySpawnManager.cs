using SpaceInvadersClone.DesignPatterns.Factory;
using UnityEngine;

namespace SpaceInvadersClone.Entities.Enemies
{
    public class EnemySpawnManager : EntitySpawnManager<Enemy>
    {
        [field: SerializeField] private EnemyData[] EnemyRowsData { get; set; }
        [field: SerializeField] private float SpacingMultiplier { get; set; } = 1.4f;
        [field: SerializeField] private Transform FirstRowReference { get; set; }

        private Transform _transform;

        private float spacing;

        private void Awake()
        {
            _transform = transform;
        }

        protected void Start()
        {
            SpawnAll();
        }

        private void SpawnAll()
        {
            for (int row = 0; row < EnemyRowsData.Length; row++)
            {
                spawner = new EntitySpawner<Enemy>(new EntityFactory<Enemy>(EnemyRowsData[row]));
                spacing = CalculateSpacing(row);

                Transform rowParent = new GameObject($"Row_{row}").transform;
                rowParent.parent = _transform;

                var width = spacing * (Columns - 1);
                var height = FirstRowReference.position.y - (spacing * row);
                Vector2 rowPosition = new Vector2(-width / 2, height);

                for (int col = 0; col < Columns; col++)
                {
                    Vector2 position = new Vector2(rowPosition.x + col * spacing, rowPosition.y);
                    Spawn(position, col, rowParent);
                }
            }
        }

        public float CalculateSpacing(int row)
        {
            return EnemyRowsData[row].Sprite.bounds.size.x * SpacingMultiplier;
        }

        private void Spawn(Vector3 position, int columnIndex, Transform parent = null)
        {
            Enemy enemy = Spawn(position, parent);
            enemy.columnIndex = columnIndex;
            NotifyOnSpawned(enemy);
        }
    }
}

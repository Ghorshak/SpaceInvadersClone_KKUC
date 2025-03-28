using System;
using UnityEngine;

namespace SpaceInvadersClone.Entities.Enemies
{
    public class Enemy : Entity
    {
        public event Action<Enemy> OnKill = delegate {};

        [field: SerializeField] public EnemyData EnemyData { get; private set; }
        [field: SerializeField] public BoxCollider2D Collider { get; private set; }
        [field: SerializeField] public Transform GunTransform { get; private set; }

        [HideInInspector] public int columnIndex;

        private void Awake()
        {
            SpriteRenderer.sprite = EnemyData.Sprite;
        }

        private void OnDestroy()
        {
            OnKill.Invoke(this);
        }
    }
}

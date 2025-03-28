using System;
using SpaceInvadersClone.DesignPatterns.ObjectPooling;
using UnityEngine;

namespace SpaceInvadersClone.Entities.Players
{
    public class Player : Entity
    {
        public static event Action OnKill = delegate {};

        [field: SerializeField] public PlayerData Data { get; private set; }
        [field: SerializeField] private Rigidbody2D Rigidbody { get; set; }
        [field: SerializeField] public BoxCollider2D Collider { get; private set; }
        [field: SerializeField] public ObjectPooler ProjectilePool { get; private set; }
        [field: SerializeField] public Transform GunTransform { get; private set; }

        private PlayerInputModule InputModule;

        private void Awake()
        {
            InputModule = new(Data, Rigidbody, ProjectilePool, GunTransform);
            SpriteRenderer.sprite = Data.Sprite;
        }

        private void OnEnable()
        {
            InputModule.AttachEvents();
        }

        private void OnDisable()
        {
            InputModule.DetachEvents();
        }

        private void FixedUpdate()
        {
            InputModule.UpdateState();
        }

        private void OnDestroy()
        {
            OnKill.Invoke();
        }
    }
}

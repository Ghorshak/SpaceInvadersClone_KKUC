using SpaceInvadersClone.DesignPatterns.ObjectPooling;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceInvadersClone.Entities.Players
{
    public class PlayerInputModule
    {
        private InputAction moveAction;
        private InputAction.CallbackContext moveContext = default;
        private bool isMoving = false;

        private InputAction attackAction;

        private PlayerData Data;
        private Rigidbody2D Rigidbody;
        private ObjectPooler ProjectilePool;
        private Transform GunTransform;

        private float timer;

        public PlayerInputModule(PlayerData data, Rigidbody2D rigidbody2D, ObjectPooler projectilePool, Transform gunTransform)
        {
            Data = data;
            Rigidbody = rigidbody2D;
            ProjectilePool = projectilePool;
            GunTransform = gunTransform;

            moveAction = InputSystem.actions.FindAction("Move");
            attackAction = InputSystem.actions.FindAction("Attack");
        }

        public void AttachEvents()
        {
            moveAction.started += MoveActionStarted;
            moveAction.canceled += MoveActionCanceled;
            attackAction.performed += AttackActionPerformed;
        }

        public void DetachEvents()
        {
            moveAction.started -= MoveActionStarted;
            moveAction.canceled -= MoveActionCanceled;
            attackAction.performed -= AttackActionPerformed;
        }

        private void MoveActionStarted(InputAction.CallbackContext context)
        {
            isMoving = true;
            moveContext = context;
        }

        private void MoveActionCanceled(InputAction.CallbackContext context)
        {
            isMoving = false;
            moveContext = default;
        }

        private void AttackActionPerformed(InputAction.CallbackContext context)
        {
            if (timer < Data.DelayBetweenShots)
                return;

            ProjectilePool.SpawnObjectFromPool(GunTransform.position);
            timer = 0;
        }

        public void UpdateState()
        {
            timer += Time.deltaTime;

            if (isMoving)
            {
                var movementVector = moveContext.ReadValue<Vector2>();

                if (Mathf.Abs(movementVector.x) < Data.DeadZoneValue)
                    return;

                Rigidbody.MovePosition(Rigidbody.position + (new Vector2(movementVector.x, 0) * Data.MovementSpeed * Time.deltaTime));
            }
        }
    }
}

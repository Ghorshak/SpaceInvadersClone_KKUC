using UnityEngine;

namespace SpaceInvadersClone.Entities.Enemies
{
    public class EnemyMovementModule
    {
        private MovementBehaviour Data { get; set; }

        private Vector3 direction = Vector3.right;
        private Vector3 downAdvancement = Vector3.down;

        private Transform _transform;

        private float timer = 0;
        private float currentSpeed;

        public void Initialize(MovementBehaviour data, Transform transform)
        {
            Data = data;
            currentSpeed = Data.Speed;
            _transform = transform;
        }

        public void UpdateState()
        {
            Move();
            ProcessTimeAndSpeed();
        }

        public void Move()
        {
            _transform.position += direction * currentSpeed * Time.deltaTime;
        }

        private void ProcessTimeAndSpeed()
        {
            timer += Time.deltaTime;
            if (timer > Data.SpeedIncreaseInterval)
            {
                timer = 0;
                currentSpeed *= Data.SpeedIncreaseMultiplier;
            }
        }

        public void AdvanceMovement()
        {
            direction.x *= -1;
            _transform.position += downAdvancement * Data.AdvancementDistance;
        }
    }
}

using UnityEngine;

namespace SpaceInvadersClone.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [field: SerializeField] private ProjectileData Data { get; set; }

        private Transform _transform;
        private GameObject _gameObject;

        private void Awake()
        {
            _transform = transform;
            _gameObject = gameObject;
        }

        private void Update()
        {
            _transform.position += Data.Direction * Data.Speed * Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var targetGO = other.gameObject;
            if ((Data.UndestructableLayers & (1 << targetGO.layer)) == 0)
                Destroy(targetGO);

            _gameObject.SetActive(false);
        }
    }
}

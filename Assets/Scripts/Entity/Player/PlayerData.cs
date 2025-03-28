using UnityEngine;

namespace SpaceInvadersClone.Entities.Players
{
    [CreateAssetMenu(fileName = "NewPlayer", menuName = "BCS Interview Project/PlayerData")]
    public class PlayerData : EntityData
    {
        [field: SerializeField] public float MovementSpeed { get; private set; } = 5f;
        [field: SerializeField] public float DeadZoneValue { get; private set; } = 0.1f;

        [field: SerializeField, Space] public float DelayBetweenShots { get; private set; } = 0.25f;
    }
}

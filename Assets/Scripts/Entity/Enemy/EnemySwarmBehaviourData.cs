using System;
using UnityEngine;

namespace SpaceInvadersClone.Entities.Enemies
{
    [CreateAssetMenu(fileName = "NewEnemySwarmBehaviour", menuName = "BCS Interview Project/EnemySwarmBehaviourData")]
    public class EnemySwarmBehaviourData : ScriptableObject
    {
        [field: SerializeField] public MovementBehaviour MovementBehaviour { get; private set; }
        [field: SerializeField, Space] public AttackBehaviour AttackBehaviour { get; private set; }
    }

    [Serializable]
    public class MovementBehaviour
    {
        [field: SerializeField] public float Speed { get; private set; } = 1;
        [field: SerializeField] public float SpeedIncreaseInterval { get; private set; } = 6;
        [field: SerializeField] public float SpeedIncreaseMultiplier { get; private set; } = 1.2f;
        [field: SerializeField, Space] public float AdvancementDistance { get; private set; } = 0.25f;
    }

    [Serializable]
    public class AttackBehaviour
    {
        [field: SerializeField] public float MinDelayBetweenAttacks { get; private set; } = 1;
        [field: SerializeField] public float AttackRateMin { get; private set; } = 1;
        [field: SerializeField] public float AttackRateMax { get; private set; } = 5;

        [field: SerializeField, Space] public float AttackRateIncreaseInterval { get; private set; } = 6;
        [field: SerializeField] public float AttackRateIncreaseMultiplier { get; private set; } = 1.2f;
    }
}

using UnityEngine;

namespace SpaceInvadersClone.Entities.Enemies
{
    [CreateAssetMenu(fileName = "NewEnemy", menuName = "BCS Interview Project/EnemyData")]
    public class EnemyData : EntityData
    {
        [field: SerializeField] public int Score { get; private set; }
    }
}

using System;
using SpaceInvadersClone.Entities.Enemies;
using SpaceInvadersClone.Entities.Players;
using UnityEngine;

namespace SpaceInvadersClone.Mangers
{
    public class GameplayManager : MonoBehaviour
    {
        public static Action<int> OnScoreUpdate = delegate {};

        [field: SerializeField] public static int Score { get; private set; } = 0;

        private void OnEnable()
        {
            EnemiesController.OnEnemyDestroyed += IncreaseScore;
            Player.OnKill += PauseGame;
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void UnpauseGame()
        {
            Time.timeScale = 1;
        }

        private void IncreaseScore(Enemy enemy, bool _)
        {
            Score += enemy.EnemyData.Score;
            OnScoreUpdate.Invoke(Score);
        }

        private void OnDisable()
        {
            EnemiesController.OnEnemyDestroyed += IncreaseScore;
            Player.OnKill -= PauseGame;
        }
    }
}

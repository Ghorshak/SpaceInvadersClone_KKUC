using SpaceInvadersClone.Entities.Enemies;
using SpaceInvadersClone.Entities.Players;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvadersClone.UI.EndGamePanel
{
    public class EndGamePanelView : MonoBehaviour
    {
        [field: SerializeField] private GameObject Panel { get; set; }
        [field: SerializeField] private MessageView GameOverMessage { get; set; }
        [field: SerializeField] private MessageView WinMessage { get; set; }
        [field: SerializeField, Space] private Button RetryButton { get; set; }
        [field: SerializeField] private Button ExitButton { get; set; }

        private void OnEnable()
        {
            RetryButton.onClick.AddListener(Retry);
            ExitButton.onClick.AddListener(ExitGame);

            Player.OnKill += ShowGameOverEndGamePanel;
            EnemiesController.OnEnemyDestroyed += ShowWinEndGamePanel;
        }

        private void Retry()
        {
            // TODO: reset game stae to new
        }

        private void ExitGame()
        {
            Debug.Log("Game Quit");
            Application.Quit();
        }

        public void ShowGameOverEndGamePanel()
        {
            Panel.SetActive(true);
            GameOverMessage.gameObject.SetActive(true);
        }

        public void ShowWinEndGamePanel(Enemy enemy, bool lastOne)
        {
            if (lastOne)
            {
                Panel.SetActive(true);
                WinMessage.gameObject.SetActive(true);
            }
        }

        public void CloseEndGamePanel()
        {
            Panel.SetActive(false);
            GameOverMessage.gameObject.SetActive(false);
            WinMessage.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            Player.OnKill -= ShowGameOverEndGamePanel;
            EnemiesController.OnEnemyDestroyed -= ShowWinEndGamePanel;
        }
    }
}

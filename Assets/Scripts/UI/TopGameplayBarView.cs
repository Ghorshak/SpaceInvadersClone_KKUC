using SpaceInvadersClone.Mangers;
using TMPro;
using UnityEngine;

namespace SpaceInvadersClone.UI.EndGamePanel
{
    public class TopGameplayBarView : MonoBehaviour
    {
        [field: SerializeField] private TMP_Text ScoreValueText { get; set; }

        private void OnEnable()
        {
            GameplayManager.OnScoreUpdate += UpdateScoreView;
        }

        private void UpdateScoreView(int score)
        {
            ScoreValueText.text = score.ToString();
        }

        private void OnDisable()
        {
            GameplayManager.OnScoreUpdate -= UpdateScoreView;
        }
    }
}

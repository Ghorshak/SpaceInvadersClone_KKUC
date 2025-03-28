using SpaceInvadersClone.Mangers;
using TMPro;
using UnityEngine;

namespace SpaceInvadersClone.UI.EndGamePanel
{
    public class MessageView : MonoBehaviour
    {
        [field: SerializeField] private TMP_Text ScoreValue { get; set; }

        private void OnEnable()
        {
            ScoreValue.text = GameplayManager.Score.ToString();
        }
    }
}

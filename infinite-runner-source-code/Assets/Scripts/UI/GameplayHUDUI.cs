using TMPro;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

namespace InfinityRunner.UI
{
    public sealed class GameplayHUDUI : MonoBehaviour
    {
        [Header("Score HUD Settings")]
        [SerializeField] private TextMeshProUGUI _currentScoreTMP;
        [SerializeField] private TextMeshProUGUI _multiplierScoreTMP;

        [Header("Difficult HUD Settings")]
        [SerializeField] private CanvasGroup _difficultGroup;
        [SerializeField] [Range(1.0f, 4.0f)] private float _difficultShowTime = 2.6f;

        public void RefreshScore(ref int currentScore, ref float multiplierScore)
        {
            string scoreText = "";

            if(currentScore < 10) { scoreText = "00" + currentScore; }
            else if(currentScore < 100) { scoreText = "0" + currentScore; }
            else if (currentScore >= 100) { scoreText = currentScore.ToString(); }

            _currentScoreTMP.text = scoreText;

            if(multiplierScore > 0)
            {
                float multiplierCount = 1.0f + multiplierScore;

                if(!_multiplierScoreTMP.gameObject.activeInHierarchy) { _multiplierScoreTMP.gameObject.SetActive(true); }

                _multiplierScoreTMP.text = multiplierCount + "x";
            }
        }

        public async void ShowDifficultInformation()
        {
            _difficultGroup.DOFade(1f, 0.128f);

            float delayTime = _difficultShowTime * 1000;
            await Task.Delay((int)delayTime);

            _difficultGroup.DOFade(0f, 0.128f);
        }
    }
}

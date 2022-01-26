using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace SGC.SDK.UI
{
    public sealed class UIFade : MonoBehaviour
    {
        #region SINGLETON
        public static UIFade Instance { get; private set; }
        #endregion

        [Header("Fade Settings")]
        [SerializeField] private CanvasGroup _fadeCanvasGroup;
        [SerializeField] [Range(0.2f, 2f)] private float _fadeTime = 1f;

        private void Awake() => Instance = this;

        private void Start() => FadeOut();

        public async void FadeOut()
        {
            await FadeTransition(0f);
        }

        public async void FadeIn()
        {
            await FadeTransition(1f);
        }

        public async Task TaskFadeIn()
        {
            await FadeTransition(1f);
        }

        async Task FadeTransition(float value)
        {
            _fadeCanvasGroup.DOFade(value, _fadeTime);

            int milliseconds = (int)_fadeTime * 1000;

            await Task.Delay(milliseconds);
        }
    }
}

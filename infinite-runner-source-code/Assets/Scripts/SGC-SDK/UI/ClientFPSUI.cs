using TMPro;
using UnityEngine;

namespace SGC.SDK.UI
{
    public sealed class ClientFPSUI : MonoBehaviour
    {
        #region SINGLETON
        public static ClientFPSUI Instance { get; private set; }
        #endregion

        [SerializeField] private TextMeshProUGUI _fpsText;
        public string FpsText { set { _fpsText.text = value; } }

        private void Awake() => Instance = this;
    }
}

using TMPro;
using UnityEngine;

namespace SGC.SDK.UI
{
    public sealed class VisualDebugTMP : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _visualDebugText;

        public string VisualDebugText { set { _visualDebugText.text = value; } }

        private void Start() => Destroy(gameObject, 6f);
    }
}

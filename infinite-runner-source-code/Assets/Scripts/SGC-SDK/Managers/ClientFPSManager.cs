using SGC.SDK.UI;
using UnityEngine;

namespace SGC.SDK.Managers
{
    public sealed class ClientFPSManager : MonoBehaviour
    {
        [SerializeField] private int _targetFPS = 144;

        private int _currentFps;

        private void Awake() => Application.targetFrameRate = _targetFPS;

        private void Update() 
        {
            _currentFps = (int) (1.0f / Time.smoothDeltaTime);

            ClientFPSUI.Instance.FpsText = _currentFps.ToString();
        }
    }
}

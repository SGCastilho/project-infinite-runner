using SGC.SDK.UI;
using UnityEngine;

namespace SGC.SDK.Managers
{
    public class VisualDebugManager : MonoBehaviour
    {
        #region SINGLETON
        public static VisualDebugManager Instance { get; private set; }
        #endregion

        private VisualDebugUI _visualDebugUI;

        private void Awake() 
        {
            Instance = this;
            _visualDebugUI = FindObjectOfType<VisualDebugUI>();
        }

        public void VisualDebugLog(string message)
        {
            _visualDebugUI.InstantiateVisualDebug(message);
        }
    }
}

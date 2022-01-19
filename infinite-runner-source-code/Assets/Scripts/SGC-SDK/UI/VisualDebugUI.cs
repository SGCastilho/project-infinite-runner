using UnityEngine;

namespace SGC.SDK.UI
{
    public sealed class VisualDebugUI : MonoBehaviour
    {
        private GameObject _visualDebugPrefab;

        private void Awake() => _visualDebugPrefab = (GameObject)Resources.Load("SGC-SDK/UI/VisualDebug_TMP");

        public void InstantiateVisualDebug(string message)
        {
            if(message != "" || message != null)
            {
                GameObject obj = Instantiate(_visualDebugPrefab, transform.position, Quaternion.identity, transform);
                
                obj.GetComponent<VisualDebugTMP>().VisualDebugText = message;
            }
        }
    }
}

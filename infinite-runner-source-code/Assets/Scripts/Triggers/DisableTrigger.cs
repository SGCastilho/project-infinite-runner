using InfinityRunner.Utilities;
using UnityEngine;

namespace InfinityRunner.Triggers
{
    public sealed class DisableTrigger : MonoBehaviour
    {
        private void OnTriggerExit(Collider other) 
        {
            if(other.CompareTag("Player"))
            {
                Transform parent = transform.parent;
                parent.gameObject.AddComponent<DisableObject>();
            }
        }
    }
}

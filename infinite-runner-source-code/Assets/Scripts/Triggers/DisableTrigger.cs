using InfinityRunner.Utilities;
using InfinityRunner.Manager;
using UnityEngine;

namespace InfinityRunner.Triggers
{
    public sealed class DisableTrigger : MonoBehaviour
    {
        [SerializeField] private Transform _disableParent;

        public bool generationTrigger = false;

        private void OnTriggerExit(Collider other) 
        {
            if(other.CompareTag("Player"))
            {
                if(generationTrigger)
                {
                    InfiniteRunGenerationManager.Instance.GenerateNextRun();
                    generationTrigger = false;
                }

                _disableParent.gameObject.AddComponent<DisableObject>();
            }
        }
    }
}

using InfinityRunner.Manager;
using UnityEngine;

namespace InfinityRunner.Triggers
{
    public sealed class CollectableTrigger : MonoBehaviour
    {
        [SerializeField] private int _collectableReward;

        private void OnTriggerEnter(Collider other) 
        {
            if(other.CompareTag("Player"))
            {
                ScoreManager.Instace.IncreasePlayerScore(ref _collectableReward);

                gameObject.SetActive(false);
            }
        }
    }
}

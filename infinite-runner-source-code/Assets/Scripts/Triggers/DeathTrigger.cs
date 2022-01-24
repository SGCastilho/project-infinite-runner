using InfinityRunner.Player;
using UnityEngine;

namespace InfinityRunner.Triggers
{
    public sealed class DeathTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) 
        {
            if(other.CompareTag("Player"))
            {
                PlayerBehaviour.Instance.PlayerDeath();
            }
        }
    }
}

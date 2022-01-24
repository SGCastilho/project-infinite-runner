using InfinityRunner.Player;
using SGC.SDK.Managers;
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

                VisualDebugManager.Instance.VisualDebugLog("Player lost the game!");
            }
        }
    }
}

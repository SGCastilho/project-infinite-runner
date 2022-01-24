using System.Threading.Tasks;
using InfinityRunner.Player;
using UnityEngine;

namespace InfinityRunner.Utilities
{
    public sealed class DisableObject : MonoBehaviour
    {
        private void Start() => FromTime();

        private async void FromTime()
        {
            await Task.Delay(1000);

            if(!PlayerBehaviour.Instance.IsDead)
            {
                try
                { 
                    gameObject.SetActive(false);
                    Destroy(gameObject.GetComponent<DisableObject>()); 
                }
                catch { return; }
            }
        }
    }
}

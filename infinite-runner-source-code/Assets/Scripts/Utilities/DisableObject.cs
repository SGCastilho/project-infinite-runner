using System.Threading.Tasks;
using UnityEngine;

namespace InfinityRunner.Utilities
{
    public sealed class DisableObject : MonoBehaviour
    {
        private void Start() => FromTime();

        private async void FromTime()
        {
            await Task.Delay(1000);

            try{ gameObject.SetActive(false); }
            catch { return; }
        }
    }
}

using UnityEngine;

namespace InfinityRunner.Utilities
{
    public sealed class ProjectileDetection : MonoBehaviour
    {
        [SerializeField] [Range(1, 4)] private int _projectileDamage = 1;

        private void OnTriggerEnter(Collider other) 
        {
            if(other.gameObject.GetComponent<IDestructible>() != null)
            {
                other.gameObject.GetComponent<IDestructible>().ApplyDamage(_projectileDamage);
                other.gameObject.SetActive(false);
            }
        }
    }
}

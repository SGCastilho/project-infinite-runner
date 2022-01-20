using UnityEngine;

namespace InfinityRunner.Utilities
{
    public sealed class ActiveChildsObjects : MonoBehaviour
    {
        [SerializeField] private GameObject[] _childToActive;

        private void OnEnable() => ActiveChilds();

        private void ActiveChilds()
        {
            if(_childToActive != null)
            {
                for(int i = 0; i < _childToActive.Length; i++)
                {
                    if(_childToActive[i].gameObject.activeInHierarchy == false)
                    {
                        _childToActive[i].gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}

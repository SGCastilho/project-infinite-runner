using System.Collections.Generic;
using UnityEngine;

namespace InfiniteRunner.Manager
{
    public sealed class ObjectPoolingManager : MonoBehaviour
    {
        #region SINGLETON
        public static ObjectPoolingManager Instance { get; private set; }
        #endregion

        [System.Serializable]
        public sealed class PoolingObject
        {
            public string _poolingKey;
            public int _poolingInstances;
            public GameObject _poolingPrefab;
        }

        [SerializeField] private List<PoolingObject> _poolingObjects;

        private Dictionary<string, Queue<GameObject>> _poolingDictionary;

        private void Awake() => Instance = this;

        private void OnEnable() => SetupPooling();

        private void SetupPooling()
        {
            _poolingDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach(PoolingObject pool in _poolingObjects)
            {
                Queue<GameObject> objectQueue = new Queue<GameObject>();

                for(int j = 0; j < pool._poolingInstances; j++)
                {
                    GameObject poolingObj = Instantiate(pool._poolingPrefab, transform.position, Quaternion.identity);
                    poolingObj.gameObject.name = pool._poolingPrefab.gameObject.name+"_"+j;
                    poolingObj.SetActive(false);

                    objectQueue.Enqueue(poolingObj);
                }

                _poolingDictionary.Add(pool._poolingKey, objectQueue);
            }
        }

        public GameObject SpawnPooling(string poolingKey)
        {
            if(_poolingDictionary.ContainsKey(poolingKey))
            {
                GameObject poolingObject = _poolingDictionary[poolingKey].Dequeue();
                poolingObject.SetActive(true);

                _poolingDictionary[poolingKey].Enqueue(poolingObject);

                return poolingObject;
            }
            else { return null; }
        }
    }
}

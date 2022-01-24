using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfinityRunner.Triggers;
using SGC.SDK.Managers;
using UnityEngine;

namespace InfinityRunner.Manager
{
    public sealed class InfiniteRunGenerationManager : MonoBehaviour
    {
        #region SINGLETON
        public static InfiniteRunGenerationManager Instance { get; private set; }
        #endregion

        [System.Serializable]
        public sealed class ObjectGeneration
        {
            [Header("Object Settings")]
            public string _objectKey;
            public int _objectCountMin;
            public int _objectCountMax;
            public int _maxObjectPerRun = 6;
            public float _objectSize;

            [Header("Collectables Settings")]
            public bool _spawnCollectable = true;
        }
        
        [Header("Objects Generation Settings")]
        [SerializeField] private List<ObjectGeneration> _objectGeneration;

        [Header("Collectables Generation Settings")]
        [SerializeField] private int _maxCollectableGenerated = 4;
        [SerializeField] private int _minCollectableGenerated = 6;
        [SerializeField] [Range(0.7f, 1.2f)] private float _minCollectableSpacing = 0.7f;
        [SerializeField] [Range(0.7f, 1.2f)] private float _maxCollectableSpacing = 1.0f;

        [Header("Hole Generation Settings")]
        [SerializeField] [Range(6, 24)] private int _holeSizeMin = 6;
        [SerializeField] [Range(6, 24)] private int _holeSizeMax = 6;
        public int IncreaseHoleSize 
        { 
            set 
            { 
                _holeSizeMax += value;
                VisualDebugManager.Instance.VisualDebugLog("O tamanho do buraco foi aumentado: " + _holeSizeMax);
            }  
        }

        [SerializeField] private int[] _runGeneration;
        [SerializeField] private float _lastGenerationX;

        private void Awake() => Instance = this;

        private void Start() => GenerateStartRun();

        private async void GenerateStartRun()
        {
            await GeneratePercurse(0, _objectGeneration[0]._objectCountMin);

            await GenerateHoleGround(_holeSizeMin);

            await GeneratePercurse(0);

            GenerateNextRun();
        }

        public async void GenerateNextRun()
        {
            await GenerateRunSequence();

            await GeneratePercurseSequence();
        }

        async Task GenerateRunSequence()
        {
            _runGeneration = new int [_objectGeneration.Count+1];
            for(int i = 0; i < _runGeneration.Length; i++)
            {
                _runGeneration[i] = i;

                await Task.Yield();
            }
            
            _runGeneration = _runGeneration.OrderBy(t=> System.Guid.NewGuid()).ToArray();

            if(_runGeneration[0] == 2)
            {
                int switchSequence = Random.Range(1, _runGeneration.Length);
                _runGeneration[0] = _runGeneration[switchSequence];
                _runGeneration[switchSequence] = 2;
            }
        }

        async Task GeneratePercurseSequence()
        {
            for(int i = 0; i < _runGeneration.Length; i++)
            {
                if(_runGeneration[i] == 0)
                {
                    await GeneratePercurse(0);
                }
                else if(_runGeneration[i] == 1)
                {
                    await GeneratePercurse(1);
                }
                else if(_runGeneration[i] == 2)
                {
                    await GenerateHoleGround();
                }

                await Task.Yield();
            }
        }

        #region Percurse Generation Functions
        async Task GeneratePercurse(int percurseID)
        {
            int objectCount = Random.Range(_objectGeneration[percurseID]._objectCountMin, _objectGeneration[percurseID]._objectCountMax);

            bool generateCollectables = false;

            for(int i = 0; i < objectCount; i++)
            {
                GameObject plataform = ObjectPoolingManager.Instance.SpawnPooling(_objectGeneration[percurseID]._objectKey);

                plataform.transform.position = new Vector3(_lastGenerationX, plataform.transform.position.y, plataform.transform.position.z);

                if(i >= objectCount-1 && percurseID < 1)
                {
                    plataform.GetComponentInChildren<DisableTrigger>().generationTrigger = true;
                }

                if(_objectGeneration[percurseID]._spawnCollectable && i >= 1 && generateCollectables == false)
                {
                    Transform platafromPos = plataform.transform;

                    int collectableGenerated = Random.Range(_minCollectableGenerated, _maxCollectableGenerated);
                    float collectableSpacing = Random.Range(_minCollectableSpacing, _maxCollectableSpacing);

                    float lastCollectableSpacing = 0;

                    for(int j = 0; j < collectableGenerated; j++)
                    {
                        GameObject collectable = ObjectPoolingManager.Instance.SpawnPooling("Collectable_Coin");

                        collectable.transform.position = platafromPos.position;

                        if(j < 1) 
                        {
                            Vector3 collectablePos = new Vector3(collectable.transform.position.x - collectableSpacing, 1.6f,
                            collectable.transform.position.z); 

                            lastCollectableSpacing = collectablePos.x;

                            collectable.transform.position = collectablePos;
                        }
                        else
                        {
                            Vector3 collectablePos = new Vector3(lastCollectableSpacing + collectableSpacing, 1.6f, 
                            collectable.transform.position.z);

                            lastCollectableSpacing = collectablePos.x; 

                            collectable.transform.position = collectablePos;
                        }
                    }

                    generateCollectables = true;
                }

                _lastGenerationX = plataform.transform.position.x + _objectGeneration[percurseID]._objectSize;
            }

            await Task.Yield();
        }

        async Task GeneratePercurse(int percurseID, int specificSize)
        {
            for(int i = 0; i < specificSize; i++)
            {
                GameObject plataform = ObjectPoolingManager.Instance.SpawnPooling(_objectGeneration[percurseID]._objectKey);

                plataform.transform.position = new Vector3(_lastGenerationX, plataform.transform.position.y, plataform.transform.position.z);

                _lastGenerationX = plataform.transform.position.x + _objectGeneration[percurseID]._objectSize;
            }

            await Task.Yield();
        }
        #endregion

        #region Hole Generation Functions
        async Task GenerateHoleGround()
        {
            float holeSize = Random.Range(_holeSizeMin, _holeSizeMax);

            GameObject deathTrigger = ObjectPoolingManager.Instance.SpawnPooling("Trigger_Death");
            deathTrigger.transform.localPosition = new Vector3(_lastGenerationX-holeSize, -2.8f, deathTrigger.transform.position.z);

            _lastGenerationX += holeSize;

            await Task.Yield();
        }

        async Task GenerateHoleGround(int specificSize)
        {
            GameObject deathTrigger = ObjectPoolingManager.Instance.SpawnPooling("Trigger_Death");
            deathTrigger.transform.localPosition = new Vector3(_lastGenerationX-specificSize, -2.8f, deathTrigger.transform.position.z);

            _lastGenerationX += specificSize;

            await Task.Yield();
        }
        #endregion

        public void IncreaseObjectsGeneration(int amount, string objectKey)
        {
            foreach(ObjectGeneration obj in _objectGeneration)
            {
                if(obj._objectKey == objectKey)
                {
                    obj._objectCountMax += amount;
                    if(obj._objectCountMax > obj._maxObjectPerRun)
                    {
                        obj._objectCountMax = obj._maxObjectPerRun;
                    }

                    VisualDebugManager.Instance.VisualDebugLog(objectKey + " foi incrementado com sucesso!");
                    break;
                }
            }
        }
    }
}

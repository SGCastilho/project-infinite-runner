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
            public string _objectKey;
            [Range(1, 6)] public int _objectCountMin;
            [Range(1, 6)] public int _objectCountMax;
            public float _objectSize;
        }
        
        [Header("Objects Generation Settings")]
        [SerializeField] private List<ObjectGeneration> _objectGeneration;

        [Header("Hole generation Settings")]
        [SerializeField] [Range(6, 24)] private int _holeSizeMin = 6;
        [SerializeField] [Range(6, 24)] private int _holeSizeMax = 6;

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

            for(int i = 0; i < objectCount; i++)
            {
                GameObject plataform = ObjectPoolingManager.Instance.SpawnPooling(_objectGeneration[percurseID]._objectKey);

                plataform.transform.position = new Vector3(_lastGenerationX, plataform.transform.position.y, plataform.transform.position.z);

                if(i >= objectCount-1 && percurseID < 1)
                {
                    plataform.GetComponentInChildren<DisableTrigger>().generationTrigger = true;
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
            float holeSize = Random.Range(_holeSizeMin, _holeSizeMax) + _lastGenerationX;

            _lastGenerationX = holeSize;

            await Task.Yield();
        }

        async Task GenerateHoleGround(int specificSize)
        {
            _lastGenerationX += specificSize;

            await Task.Yield();
        }
        #endregion
    }
}

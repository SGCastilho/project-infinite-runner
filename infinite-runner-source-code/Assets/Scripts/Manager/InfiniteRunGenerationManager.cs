using System.Linq;
using System.Collections.Generic;
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
        }
        
        [Header("Objects Generation Settings")]
        [SerializeField] private List<ObjectGeneration> _objectGeneration;

        [Header("Hole generation Settings")]
        [SerializeField] [Range(6, 24)] private int _holeSizeMin = 6;
        [SerializeField] [Range(6, 24)] private int _holeSizeMax = 6;

        private const float OBJECT_MOVEMENT_X = 12f;

        private int[] _runGeneration;
        [SerializeField] private float _lastGenerationX;

        private void Awake() => Instance = this;

        private void Start() => GenerateStartRun();

        private void Update() 
        {
            if(Input.GetKeyDown(KeyCode.F1))
            {
                GenerateNextRun();
            }
        }

        private void GenerateStartRun()
        {
            GeneratePercurse(0, ref _objectGeneration[0]._objectCountMin);
            GenerateHoleGround(ref _holeSizeMin);
            GeneratePercurse(0);
        }

        public void GenerateNextRun()
        {
            GenerateNextRunSequence();

            foreach(int generation in _runGeneration)
            {
                switch(generation)
                {
                    case 0:
                        GenerateHoleGround();
                        break;
                    case 1:
                        GeneratePercurse(0);
                        break;
                    case 2:
                        GeneratePercurse(1);
                        break;
                }   
            }
        }

        #region Percurse Generation Functions
        private void GeneratePercurse(int percurseID)
        {
            int objectCount = Random.Range(_objectGeneration[percurseID]._objectCountMin, _objectGeneration[percurseID]._objectCountMax);

            for(int i = 0; i < objectCount; i++)
            {
                GameObject plataform = ObjectPoolingManager.Instance.SpawnPooling(_objectGeneration[percurseID]._objectKey);

                plataform.transform.position = new Vector3(_lastGenerationX, plataform.transform.position.y, plataform.transform.position.z);

                _lastGenerationX = plataform.transform.position.x + OBJECT_MOVEMENT_X;
            }
        }

        private void GeneratePercurse(int percurseID, ref int specificSize)
        {
            for(int i = 0; i < specificSize; i++)
            {
                GameObject plataform = ObjectPoolingManager.Instance.SpawnPooling(_objectGeneration[percurseID]._objectKey);

                plataform.transform.position = new Vector3(_lastGenerationX, plataform.transform.position.y, plataform.transform.position.z);

                _lastGenerationX = plataform.transform.position.x + OBJECT_MOVEMENT_X;
            }
        }
        #endregion

        #region Hole Generation Functions
        private void GenerateHoleGround()
        {
            int holeSize = Random.Range(_holeSizeMin, _holeSizeMax);

            _lastGenerationX += holeSize;
        }

        private void GenerateHoleGround(ref int specificSize)
        {
            _lastGenerationX += specificSize;
        }
        #endregion

        private void GenerateNextRunSequence()
        {
            _runGeneration = new int [_objectGeneration.Count+1];
            for(int i = 0; i < _runGeneration.Length; i++)
            {
                _runGeneration[i] = i;
            }

            _runGeneration = _runGeneration.OrderBy(t=> System.Guid.NewGuid()).ToArray();
        }
    }
}

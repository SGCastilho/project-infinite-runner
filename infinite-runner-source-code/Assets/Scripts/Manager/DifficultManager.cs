using System.Collections.Generic;
using SGC.SDK.Managers;
using InfinityRunner.Player;
using UnityEngine;

namespace InfinityRunner.Manager
{
    public sealed class DifficultManager : MonoBehaviour
    {
        #region SINGLETON
        public static DifficultManager Instance;
        #endregion

        [System.Serializable]
        public sealed class GenerationDifficult
        {
            public string _generationKey;
            public int _generationIncrease;
        }


        [Header("Difficult Settings")]
        [SerializeField] private int _currentDifficult;
        internal int CurrentDifficult { get { return _currentDifficult; } }
        [SerializeField] private int _minDifficult = 0;
        [SerializeField] private int _maxDifficult = 6;

        [Header("Player Difficult Settings")]
        [SerializeField] private float _playerMovimentSpeedIncrease = 4;

        [Header("Generation Difficult Settings")]
        [SerializeField] private List<GenerationDifficult> _generationDifficult;

        [Header("Hole Generation Difficult Settings")]
        [SerializeField] private int _holeSizeIncrease;

        private void Awake() => Instance = this;

        private void Start() => _currentDifficult = _minDifficult;

        private void Update() 
        {
            if(Input.GetKeyDown(KeyCode.F1))
            {
                IncreaseDifficult();
            }
        }

        public void IncreaseDifficult()
        {
            if(_currentDifficult < _maxDifficult)
            {
                PlayerBehaviour.Instance.PlayerMoviment.IncreaseMovementSpeed(_playerMovimentSpeedIncrease);

                for(int i = 0; i < _generationDifficult.Count; i++)
                {
                    InfiniteRunGenerationManager.Instance.IncreaseObjectsGeneration(_generationDifficult[i]._generationIncrease, 
                    _generationDifficult[i]._generationKey);
                }

                InfiniteRunGenerationManager.Instance.IncreaseHoleSize = _holeSizeIncrease;

                ScoreManager.Instace.IncreasePassiveScore();

                _currentDifficult++;
                if(_currentDifficult > _maxDifficult) { _currentDifficult = _maxDifficult; }

                VisualDebugManager.Instance.VisualDebugLog("Dificuldade aumentada para: " + _currentDifficult);
            }
        }
    }
}

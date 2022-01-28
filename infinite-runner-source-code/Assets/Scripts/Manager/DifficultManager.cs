using System.Collections.Generic;
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
        
        public delegate void IncreasePlayerMovimentSpeed(float amount);
        public event IncreasePlayerMovimentSpeed OnIncreasePlayerMovimentSpeed;

        public delegate void IncreasePassiveScore();
        public event IncreasePassiveScore OnIncreasePassiveScore;

        public delegate void IncreaseObjectGeneration(int amount, string key);
        public event IncreaseObjectGeneration OnIncreaseObjectGeneration;

        public delegate void DifficultChange();
        public event DifficultChange OnDifficultChange;

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

        public void IncreaseDifficult()
        {
            if(_currentDifficult < _maxDifficult)
            {
                if(OnIncreasePlayerMovimentSpeed != null) { OnIncreasePlayerMovimentSpeed(_playerMovimentSpeedIncrease); }

                for(int i = 0; i < _generationDifficult.Count; i++)
                {
                    if(OnIncreaseObjectGeneration != null) 
                    { OnIncreaseObjectGeneration(_generationDifficult[i]._generationIncrease, _generationDifficult[i]._generationKey); }
                }

                InfiniteRunGenerationManager.Instance.IncreaseHoleSize = _holeSizeIncrease;

                if(OnIncreasePassiveScore != null) { OnIncreasePassiveScore(); }

                _currentDifficult++;
                if(_currentDifficult > _maxDifficult) { _currentDifficult = _maxDifficult; }

                if(OnDifficultChange != null) { OnDifficultChange(); }
            }
        }
    }
}

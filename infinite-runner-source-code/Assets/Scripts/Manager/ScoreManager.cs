using UnityEngine;

namespace InfinityRunner.Manager
{
    public sealed class ScoreManager : MonoBehaviour
    {
        #region SINGLETON
        public static ScoreManager Instace;
        #endregion

        public delegate void ScoreIncrease(ref int currentScore, ref float multiplierScore);
        public event ScoreIncrease OnScoreIncrease;

        public delegate void IncreaseDifficult();
        public event IncreaseDifficult OnIncreaseDifficult;

        [Header("Score Settings")]
        [SerializeField] private int _playerScore;
        public int PlayerScore { get { return _playerScore; } }

        [SerializeField] private int _passiveScoreIncrease = 20;
        [SerializeField] [Range(0.1f, 2.0f)] private float _passiveScoreTimer = 1.0f;
        private float _currentPassiveScoreTimer = 0;
        [SerializeField] [Range(0.1f, 0.6f)] private float _passiveScoreTimerReduction = 0.4f;
        [SerializeField] [Range(0.1f, 2.0f)] private float _minPassiveScoreTimer = 0.1f;
        [SerializeField] [Range(0f, 1f)] private float _scoreMultiply = 0f;
        [SerializeField] [Range(0.1f, 0.6f)] private float _scoreMultiplyIncrease = 0.2f;
        [SerializeField] [Range(0.1f, 1f)] private float _maxScoreMultiply = 1.0f;

        [Header("Score Difficult Increase Settings")]
        [SerializeField] private int[] _scoreToChangeDifficult;

        private void Awake() => Instace = this;

        private void Update() => ScorePassiveTimer();

        public void IncreasePlayerScore(ref int amount)
        {
            if(_scoreMultiply > 0)
            {
                float finalAmount =  (amount * _scoreMultiply) + _passiveScoreIncrease;
                _playerScore += (int)finalAmount;
            }
            else 
            { 
                _playerScore += amount;
            }

            if(DifficultManager.Instance.CurrentDifficult < 6){ CheckCurrentScore(); }

            if(OnScoreIncrease != null) { OnScoreIncrease(ref _playerScore, ref _scoreMultiply); }
        }

        public void IncreasePassiveScore()
        {
            _passiveScoreTimer -= _passiveScoreTimerReduction;
            if(_passiveScoreTimer < _minPassiveScoreTimer) { _passiveScoreTimer = _minPassiveScoreTimer; }

            _scoreMultiply += _scoreMultiplyIncrease;
            if(_scoreMultiply > _maxScoreMultiply) { _scoreMultiply = _maxScoreMultiply; }

            if(OnScoreIncrease != null) { OnScoreIncrease(ref _playerScore, ref _scoreMultiply); }
        }

        private void ScorePassiveTimer()
        {
            _currentPassiveScoreTimer += Time.deltaTime;
            if(_currentPassiveScoreTimer >= _passiveScoreTimer)
            {
                IncreasePlayerScore(ref _passiveScoreIncrease);
                _currentPassiveScoreTimer = 0;
            }
        }

        private void CheckCurrentScore()
        {
            int difficult = DifficultManager.Instance.CurrentDifficult;
            for(int i = 0; i < _scoreToChangeDifficult.Length; i++)
            {
                if(_playerScore >= _scoreToChangeDifficult[difficult])
                {
                    if(OnIncreaseDifficult != null) { OnIncreaseDifficult(); }
                    break;
                }
            }
        }
    }
}

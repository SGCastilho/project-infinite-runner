using UnityEngine;

namespace InfinityRunner.Manager
{
    public sealed class GameManager : MonoBehaviour
    {
        public delegate void FinishGame(int playerScore, string playerRank);
        public event FinishGame OnFinishGame;

        [Header("Finish Game Settings")]
        [SerializeField] private int _finalPlayerScore;
        [SerializeField] private string _finalPlayerRank;

        [System.Serializable]
        public sealed class RankRequeriment
        {
            public string _scoreRank;
            public int _scoreRequeriment;
        }

        [Header("Player Rank Settings")]
        [SerializeField] private RankRequeriment[] _playerRankScoreRequirement;

        public void RunFinish()
        {
            _finalPlayerScore = ScoreManager.Instace.PlayerScore;

            ScoreManager.Instace.enabled = false;      

            CheckPlayerRank();

            if(OnFinishGame != null) { OnFinishGame(_finalPlayerScore, _finalPlayerRank); }
        }

        private void CheckPlayerRank()
        {
            for(int i = 0; i < _playerRankScoreRequirement.Length; i++)
            {
                if(_finalPlayerScore <= _playerRankScoreRequirement[i]._scoreRequeriment)
                {
                    _finalPlayerRank = _playerRankScoreRequirement[i]._scoreRank;
                    break;
                }
                else if(_finalPlayerScore >= _playerRankScoreRequirement[11]._scoreRequeriment)
                {
                    _finalPlayerRank = _playerRankScoreRequirement[11]._scoreRank;
                    break;
                }
            }
        }
    }
}

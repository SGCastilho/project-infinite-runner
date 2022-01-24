using UnityEngine;

namespace InfinityRunner.Manager
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private int _finalPlayerScore;

        public void FinishGame()
        {
            _finalPlayerScore = ScoreManager.Instace.PlayerScore;
            
            ScoreManager.Instace.enabled = false;
        }
    }
}

using InfinityRunner.Player;
using InfinityRunner.UI;
using UnityEngine;

namespace InfinityRunner.Manager
{
    public sealed class GameplayEventManager : MonoBehaviour
    {
        private PlayerBehaviour _playerBehaviour;

        [Header("Cache Manager Scripts")]
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private InfiniteRunGenerationManager _infiniteRunGenerationManager;
        [SerializeField] private DifficultManager _difficultManager;

        [Header("HUD Manager Scripts")]
        [SerializeField] private GameplayHUDUI _gameplayHUD;

        private void Awake() => _playerBehaviour = GameObject.FindObjectOfType<PlayerBehaviour>();

        private void OnEnable() => SetupEvents();

        private void OnDestroy() => DestroyEvents();

        private void SetupEvents()
        {
            _playerBehaviour.OnPlayerDead += _gameManager.RunFinish;

            _scoreManager.OnScoreIncrease += _gameplayHUD.RefreshScore;
            _scoreManager.OnIncreaseDifficult += _difficultManager.IncreaseDifficult;

            _difficultManager.OnDifficultChange += _gameplayHUD.ShowDifficultInformation;
            _difficultManager.OnIncreaseObjectGeneration += _infiniteRunGenerationManager.IncreaseObjectsGeneration;
            _difficultManager.OnIncreasePlayerMovimentSpeed += _playerBehaviour.PlayerMoviment.IncreaseMovementSpeed;
            _difficultManager.OnIncreasePassiveScore += _scoreManager.IncreasePassiveScore;

            _gameManager.OnFinishGame += _gameplayHUD.ShowFinishRunWindow;
        }

        private void DestroyEvents()
        {
            _playerBehaviour.OnPlayerDead -= _gameManager.RunFinish;

            _scoreManager.OnScoreIncrease -= _gameplayHUD.RefreshScore;
            _scoreManager.OnIncreaseDifficult -= _difficultManager.IncreaseDifficult;

            _difficultManager.OnDifficultChange -= _gameplayHUD.ShowDifficultInformation;
            _difficultManager.OnIncreaseObjectGeneration -= _infiniteRunGenerationManager.IncreaseObjectsGeneration;
            _difficultManager.OnIncreasePlayerMovimentSpeed -= _playerBehaviour.PlayerMoviment.IncreaseMovementSpeed;
            _difficultManager.OnIncreasePassiveScore -= _scoreManager.IncreasePassiveScore;

            _gameManager.OnFinishGame -= _gameplayHUD.ShowFinishRunWindow;
        }
    }
}

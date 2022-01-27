using SGC.SDK.Managers;
using UnityEngine;

namespace InfinityRunner.UI
{
    public sealed class RunEndWindowButtons : MonoBehaviour
    {
        [SerializeField] private GameplayHUDUI _gameplayHUDUI;

        public void RestartRun()
        {
            _gameplayHUDUI._sceneEventSystem.SetSelectedGameObject(null);

            LoadingManager loadingManager = LoadingManager.Instance;
            loadingManager.LoadLevel(loadingManager.currentScene);
        }

        public void BackToMainMenu()
        {
            _gameplayHUDUI._sceneEventSystem.SetSelectedGameObject(null);

            LoadingManager loadingManager = LoadingManager.Instance;
            loadingManager.LoadLevel("MainMenu");
        }
    }
}

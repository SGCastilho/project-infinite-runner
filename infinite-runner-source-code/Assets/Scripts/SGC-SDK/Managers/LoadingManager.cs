using SGC.SDK.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace SGC.SDK.Managers
{
    public sealed class LoadingManager : MonoBehaviour
    {
        #region SINGLETON
        public static LoadingManager Instance { get; private set; }
        #endregion

        public string currentScene;

        private void Awake() => Instance = this;

        private void Start() => currentScene = SceneManager.GetActiveScene().name;

        public async void LoadLevel(string levelToLoad)
        {
            await UIFade.Instance.TaskFadeIn();

            await Task.Delay(1000);

            SceneManager.LoadScene(levelToLoad);
        }

        public async void LoadLevel(int levelToLoad)
        {
            await UIFade.Instance.TaskFadeIn();

            await Task.Delay(1000);

            SceneManager.LoadScene(levelToLoad);
        }
    }
}

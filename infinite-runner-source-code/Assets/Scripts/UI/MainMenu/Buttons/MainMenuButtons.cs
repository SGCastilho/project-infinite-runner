using SGC.SDK.Managers;
using UnityEngine;

namespace InfinityRunner.UI.MainMenu
{
    public sealed class MainMenuButtons : MonoBehaviour
    {
        public void StartRun() => LoadingManager.Instance.LoadLevel("Gameplay");

        public void Records()
        {

        }

        public void Options()
        {

        }

        public void Quit() => Application.Quit();
    }
}

using UnityEngine.EventSystems;
using UnityEngine;

namespace InfinityRunner.UI.MainMenu
{
    public sealed class MainMenuUI : MonoBehaviour
    {
        [Header("Main Menu Settings")]
        [SerializeField] private EventSystem _sceneEventManager;

        [SerializeField] private GameObject _firstButtonSelected;

        private void Start() => _sceneEventManager.SetSelectedGameObject(_firstButtonSelected);
    }
}

using UnityEngine;

namespace InfinityRunner.Manager.MainMenu
{
    public sealed class MainMenuManager : MonoBehaviour
    {
        private PlayerInputActions _inputActions;

        private void Awake() => _inputActions = new PlayerInputActions();

        private void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.Gameplay.Disable();
            _inputActions.UI.Enable();

            //TEMPORARIO PRA CARALHO, PUTA GAMBIARRA
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            //TEMPORARIO PRA CARALHO, PUTA GAMBIARRA
        }

        private void OnDisable()
        {
            _inputActions.Disable();
            _inputActions.Gameplay.Enable();
            _inputActions.UI.Disable();
        }
    }
}

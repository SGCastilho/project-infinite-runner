using UnityEngine;

namespace InfiniteRunner.Player
{
    public sealed class PlayerInputs : MonoBehaviour
    {
        private PlayerInputActions _inputActions;

        private void Awake() => _inputActions = new PlayerInputActions();

        private void OnEnable() 
        {
            SetupInputEvents(true);
            _inputActions.Enable();
        }

        private void OnDisable() 
        {
            SetupInputEvents(false);
            _inputActions.Disable();
        }

        private void SetupInputEvents(bool setup)
        {
            //Set input events
        }
    }
}

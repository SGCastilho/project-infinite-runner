using UnityEngine;

namespace InfinityRunner.Player
{
    public sealed class PlayerInputs : MonoBehaviour
    {
        [SerializeField] private PlayerBehaviour _playerBehaviour;

        private PlayerInputActions _inputActions;

        private void Awake() => _inputActions = new PlayerInputActions();

        private void OnEnable() 
        {
            _inputActions.Enable();
            SetupInputEvents(true);
        }

        private void OnDisable() 
        {
            _inputActions.Disable();
            SetupInputEvents(false);
        }

        private void SetupInputEvents(bool setup)
        {
            if(setup)
            {
                _inputActions.Gameplay.Jump.performed += ctx => _playerBehaviour.PlayerMoviment.Jump();
            }
            else
            {
                _inputActions.Gameplay.Jump.performed -= ctx => _playerBehaviour.PlayerMoviment.Jump();
            }
        }
    }
}

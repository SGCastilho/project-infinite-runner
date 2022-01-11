using UnityEngine;

namespace InfiniteRunner.Player
{
    public sealed class PlayerBehaviour : MonoBehaviour
    {
        [Header("Player Scripts")]
        [SerializeField] private PlayerInputs _playerInputs;
        [SerializeField] private PlayerMoviment _playerMoviment;

        public PlayerInputs PlayerInputs { get { return _playerInputs; } }
        public PlayerMoviment PlayerMoviment { get { return _playerMoviment; } }
    }
}

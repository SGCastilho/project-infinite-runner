using UnityEngine;

namespace InfinityRunner.Player
{
    public sealed class PlayerBehaviour : MonoBehaviour
    {
        [Header("Player Scripts")]
        [SerializeField] private PlayerInputs _playerInputs;
        [SerializeField] private PlayerMoviment _playerMoviment;
        [SerializeField] private PlayerShoot _playerShoot;

        public PlayerInputs PlayerInputs { get { return _playerInputs; } }
        public PlayerMoviment PlayerMoviment { get { return _playerMoviment; } }
        public PlayerShoot PlayerShoot { get { return _playerShoot; } }
    }
}

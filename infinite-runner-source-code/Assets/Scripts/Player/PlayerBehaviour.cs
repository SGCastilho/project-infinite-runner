using UnityEngine;

namespace InfinityRunner.Player
{
    public sealed class PlayerBehaviour : MonoBehaviour
    {
        #region SINGLETON
        public static PlayerBehaviour Instance { get; private set; }
        #endregion

        public delegate void PlayerDead();
        public event PlayerDead OnPlayerDead;

        [Header("Player Scripts")]
        [SerializeField] private PlayerInputs _playerInputs;
        [SerializeField] private PlayerMoviment _playerMoviment;
        [SerializeField] private PlayerShoot _playerShoot;
        
        private bool _isDead = false;
        public bool IsDead { get { return _isDead; } }

        public PlayerInputs PlayerInputs { get { return _playerInputs; } }
        public PlayerMoviment PlayerMoviment { get { return _playerMoviment; } }
        public PlayerShoot PlayerShoot { get { return _playerShoot; } }

        private void Awake() => Instance = this;

        public void PlayerDeath()
        {
            _isDead = true;

            _playerInputs.GameplayInputs(false);
            gameObject.SetActive(false);

            if(OnPlayerDead != null) { OnPlayerDead(); }
        }
    }
}

using DG.Tweening;
using UnityEngine;

namespace InfinityRunner.Player
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerMoviment : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;

        [Header("Player Moviment Settings")]
        [SerializeField] [Range(6f, 20f)] private float _movimentSpeed = 6f;
        [SerializeField] private float _maxMovimentSpeed = 20f;

        [SerializeField] [Range(1, 3)] private int _playerJumpCount = 2;
        private int _playerCurrentJumpCount;
        private Vector3 _xVelocity;

        [Header("Player Gravity Settings")]
        [SerializeField] private float _maxHeight = 2f;
        [SerializeField] private float _timeToPeak = 1f;
        private float _jumpSpeed;
        private float _gravity;
        private Vector3 _yVelocity;

        private Vector3 _finalVelocity;

        private void Start() => GravityCalculate();

        private void Update()
        {
            _xVelocity = Vector3.right * _movimentSpeed;

            _yVelocity += _gravity * Time.deltaTime * Vector3.down;

            if(_characterController.isGrounded)
            {
                if(_yVelocity.y < -1)
                {
                    _playerCurrentJumpCount = 0;
                    _yVelocity = Vector3.down;
                }
            }

            _finalVelocity = _xVelocity + _yVelocity;

            _characterController.Move(_finalVelocity * Time.deltaTime);
        }

        private void GravityCalculate()
        {
            _gravity = (2 * _maxHeight) / Mathf.Pow(_timeToPeak, 2);

            _jumpSpeed = _gravity * _timeToPeak;

            _playerCurrentJumpCount = 0;
        }

        internal void Jump()
        {
            if(_playerCurrentJumpCount < _playerJumpCount)
            {
                _playerCurrentJumpCount++;
                _yVelocity = _jumpSpeed * Vector3.up;
            }
        }

        public void IncreaseMovementSpeed(float amount)
        {
            float finalSpeed = _movimentSpeed + amount;

            if(finalSpeed > _maxMovimentSpeed)
            {
                float diference = finalSpeed - _maxMovimentSpeed;
                finalSpeed -= diference;
            }            

            DOTween.To(x => _movimentSpeed = x, _movimentSpeed, finalSpeed, 0.6f);
        }
    }
}

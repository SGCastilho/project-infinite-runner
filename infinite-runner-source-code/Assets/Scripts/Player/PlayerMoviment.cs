using UnityEngine;

namespace InfiniteRunner.Player
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerMoviment : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;

        [Header("Player Moviment Settings")]
        [SerializeField] [Range(6f, 40f)] private float _movementSpeed = 20f;

        private void FixedUpdate()
        {
            _characterController.SimpleMove(Vector3.right * _movementSpeed);
        }
    }
}

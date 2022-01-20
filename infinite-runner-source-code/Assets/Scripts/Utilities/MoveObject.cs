using System.Collections;
using UnityEngine;

namespace InfinityRunner.Utilities
{
    public sealed class MoveObject : MonoBehaviour
    {
        private enum MoveDirection
        {
            RIGHT,
            LEFT
        }

        [Header("Moviment Settings")]
        [SerializeField] [Range(12f, 28f)] private float _movementSpeed = 12f;
        [SerializeField] private MoveDirection _moveDirection = MoveDirection.RIGHT;

        [Header("Disable Settings")]
        [SerializeField] [Range(6f, 12f)] private float _disableTime = 6f;

        private void OnEnable() => StartCoroutine(DisableTimer());

        private void OnDisable() => StopAllCoroutines();

        private void Update() 
        {
            switch(_moveDirection)
            {
                case MoveDirection.RIGHT:
                    transform.Translate(Vector3.right * _movementSpeed * Time.deltaTime);
                    break;
                case MoveDirection.LEFT:
                    transform.Translate(Vector3.left * _movementSpeed * Time.deltaTime);
                    break;
            }
        }

        private IEnumerator DisableTimer()
        {
            yield return new WaitForSeconds(_disableTime);
            gameObject.SetActive(false);
        }
    }
}

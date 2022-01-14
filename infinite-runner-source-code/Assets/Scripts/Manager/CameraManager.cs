using UnityEngine;

namespace InfiniteRunner.Manager
{
    public sealed class CameraManager : MonoBehaviour
    {
        [Header("Camera Manager Settings")]
        [SerializeField] private GameObject _followTransform;
        [SerializeField] private float _xOffset;
        private Vector3 _followVector;

        private void LateUpdate() 
        {
            _followVector = new Vector3(_followTransform.transform.position.x + _xOffset, transform.position.y, transform.position.z);

            transform.position = _followVector;
        }
    }
}

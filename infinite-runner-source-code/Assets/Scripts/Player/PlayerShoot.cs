using System.Collections;
using InfinityRunner.Manager;
using UnityEngine;

namespace InfinityRunner.Player
{
    public sealed class PlayerShoot : MonoBehaviour
    {
        [Header("Shooting Settings")]
        [SerializeField] private string _projectileKey;

        [SerializeField] private Transform[] _shootingPoints;
        private int _currentShootingSide = 0;

        [Header("Fire Rate Settings")]
        [SerializeField] [Range(0.1f, 1f)] private float _shootingRate = 1f;
        private bool _canShoot = true;

        internal void Shoot()
        {
            if(_canShoot)
            {
                GameObject projectile = ObjectPoolingManager.Instance.SpawnPooling(_projectileKey);
                projectile.transform.position = _shootingPoints[_currentShootingSide].position;

                if(_currentShootingSide < 1) { _currentShootingSide = 1; }
                else if(_currentShootingSide > 0) { _currentShootingSide = 0; }

                _canShoot = false;
                
                StartCoroutine(ShootFireRate());
            }
        }

        private IEnumerator ShootFireRate()
        {
            yield return new WaitForSeconds(_shootingRate);
            _canShoot = true;
        }
    }
}

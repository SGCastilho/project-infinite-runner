using InfinityRunner.Manager;
using UnityEngine;

namespace InfinityRunner.Utilities
{
    public sealed class DestructibleObject : MonoBehaviour, IDestructible
    {
        [Header("Destructible Settings")]
        [SerializeField] [Range(2, 12)] private int _destructibleHealth = 2;
        private int _currentDestructibleHealth;
        [SerializeField] private int _destructibleReward;

        private void OnEnable() => _currentDestructibleHealth = _destructibleHealth;

        public void ApplyDamage(int damage)
        {
            _currentDestructibleHealth -= damage;
            if(_currentDestructibleHealth <= 0)
            {
                _currentDestructibleHealth = 0;
                ScoreManager.Instace.IncreasePlayerScore(ref _destructibleReward);
                gameObject.SetActive(false);
            }
        }
    }
}

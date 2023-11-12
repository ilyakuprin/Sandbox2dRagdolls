using UnityEngine;

namespace Sandbox
{
    public class HealthHandler : MonoBehaviour
    {
        [SerializeField, Range(1, 10)] private int _maxHealth;
        [SerializeField, Range(1, 10)] private int _health;

        public void Reduce(int value)
        {
            if (value > 0)
            {
                _health -= value;
                if (_health < 0)
                    _health = 0;
            }
        }

        public void Increase(int value)
        {
            if (value > 0)
            {
                _health += value;
                if (_health > _maxHealth)
                    _health = _maxHealth;
            }
        }

        private void OnValidate()
        {
            if (_health > _maxHealth)
                _health = _maxHealth;
        }
    }
}

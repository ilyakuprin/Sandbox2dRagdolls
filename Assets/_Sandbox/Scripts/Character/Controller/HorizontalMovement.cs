using UnityEngine;

namespace Sandbox
{
    public class HorizontalMovement : MonoBehaviour, IMove
    {
        [SerializeField] private JoystickDirection _joystickDirection;
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D[] _rigidbody;

        public void Move(Vector2 direction)
        {
            foreach (Rigidbody2D rigidbody in _rigidbody)
                rigidbody.velocity = new Vector2(direction.x * _speed,
                                                 rigidbody.velocity.y);
        }

        private void OnEnable()
            => _joystickDirection.DirectionSet += Move;

        private void OnDisable()
            => _joystickDirection.DirectionSet -= Move;
    }
}

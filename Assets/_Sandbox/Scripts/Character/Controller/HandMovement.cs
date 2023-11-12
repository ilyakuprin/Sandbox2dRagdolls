using UnityEngine;

namespace Sandbox
{
    public class HandMovement : MonoBehaviour, IMove
    {
        [SerializeField] private JoystickDirection _joystickDirection;
        [SerializeField] private Balance[] _hand;
        private float _idleValueBalance;

        private void Awake()
            => _idleValueBalance = _hand[0].GetTargetRotation;

        public void Move(Vector2 direction)
        {
            if (direction != Vector2.zero)
            {
                float angle = Vector2.Angle(Vector2.right, direction);

                if (direction.y < 0)
                    angle *= -1;

                foreach (Balance partOfHand in _hand)
                    partOfHand.SetTargetRotation(angle);
            }
            else
            {
                foreach (Balance partOfHand in _hand)
                    partOfHand.SetTargetRotation(_idleValueBalance);
            }
        }

        private void OnEnable()
            => _joystickDirection.DirectionSet += Move;

        private void OnDisable()
            => _joystickDirection.DirectionSet -= Move;
    }
}

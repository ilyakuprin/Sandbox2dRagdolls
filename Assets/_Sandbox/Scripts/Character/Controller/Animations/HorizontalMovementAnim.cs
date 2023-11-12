using UnityEngine;

namespace Sandbox
{
    [RequireComponent(typeof(Animator))]
    public class HorizontalMovementAnim : MonoBehaviour, IMove
    {
        [SerializeField] private JoystickDirection _joystickDirection;
        [SerializeField] private Animator _animator;
        private int _walkBack;
        private int _walkStraight;

        private void Awake()
        {
            HashingAnimation hashing = new HashingAnimation();
            _walkBack = hashing.WalkBack;
            _walkStraight = hashing.WalkStraight;
        }

        public void Move(Vector2 direction)
        {
            if (direction.x > 0)
            {
                _animator.SetBool(_walkBack, false);
                _animator.SetBool(_walkStraight, true);
            }
            else if (direction.x < 0)
            {
                _animator.SetBool(_walkBack, true);
                _animator.SetBool(_walkStraight, false);
            }
            else
            {
                _animator.SetBool(_walkBack, false);
                _animator.SetBool(_walkStraight, false);
            }
        }

        private void OnEnable()
            => _joystickDirection.DirectionSet += Move;

        private void OnDisable()
            => _joystickDirection.DirectionSet += Move;

        private void OnValidate()
            => _animator ??= GetComponent<Animator>();
    }
}

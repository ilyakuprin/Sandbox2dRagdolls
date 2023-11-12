using System;
using UnityEngine;

namespace Sandbox
{
    public class PickUpItem : MonoBehaviour
    {
        [SerializeField] private ButtonInteraction _buttonInteraction;
        [SerializeField] private Transform _pointForWeapon;
        [SerializeField] private Transform _overlapForTake;
        [SerializeField, Range(0.2f, 3f)] private float _circleTakeRadius;
        [SerializeField] private LayerMask _weaponLayer;
        private bool _isWeapon;
        private bool _isInHand;
        private Transform _weapon;
        private FireWeapon _fireWeapon;

        private Transform _startParent;
        private Vector2 _startPosition;
        private Quaternion _startRotation;

        private void PickUp()
        {
            if (_isWeapon && !_isInHand)
            {
                _isInHand = true;

                _startParent = _weapon.parent;
                _startPosition = _weapon.localPosition;
                _startRotation = _weapon.localRotation;

                SetWeaponOptions(_pointForWeapon,
                                 Vector2.zero,
                                 Quaternion.Euler(Vector2.zero));

                _fireWeapon.SetIsPickUp(true);
            }
            else if (_isInHand)
            {
                _isInHand = false;

                SetWeaponOptions(_startParent,
                                 new Vector2(_weapon.position.x, _startPosition.y),
                                 _startRotation);

                _fireWeapon.SetIsPickUp(false);
            }
        }

        private void SetWeaponOptions(Transform parant,
                                      Vector2 localPosition,
                                      Quaternion localRotation)
        {
            _weapon.parent = parant;
            _weapon.SetLocalPositionAndRotation(localPosition, localRotation);
        }

        private void FixedUpdate()
        {
            if (!_isInHand)
            {
                Vector3 overlapCirclePosition = _overlapForTake.position;

                _isWeapon = Physics2D.OverlapCircle(overlapCirclePosition, _circleTakeRadius, _weaponLayer);

                if (_isWeapon)
                {
                    _weapon = Physics2D.OverlapCircle(overlapCirclePosition, _circleTakeRadius, _weaponLayer).transform;
                    _fireWeapon = _weapon.GetComponent<FireWeapon>();
                }
            }
        }

        private void OnEnable()
            => _buttonInteraction.ButtonPressed += PickUp;

        private void OnDisable()
            => _buttonInteraction.ButtonPressed -= PickUp;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_overlapForTake.position, _circleTakeRadius);
        }
    }
}

using System.Collections;
using UnityEngine;

namespace Sandbox
{
    public class FireWeapon : MonoBehaviour
    {
        [SerializeField] private JoystickDirection _joystickHands;
        [SerializeField] private PatronHandler _patronHandler;
        [SerializeField] private PickUpItem _pickUpItem;
        [SerializeField] private Transform _firePoint;
        [SerializeField, Range(0, 10)] private float _intervalBetweenShots;
        private Coroutine _coroutineShoot;
        private bool _isWeaponPickUp;
        private bool _isPressed;

        public void SetIsPickUp(bool value)
        {
            _isWeaponPickUp = value;

            if (!value)
                StopShoot();
            else
                StartShoot();
        }

        private void SetIsPressed(bool value)
        {
            _isPressed = value;

            if (!value)
                StopShoot();
            else
                StartShoot();
        }

        private void StartShoot()
        {
            if (_coroutineShoot == null && _isWeaponPickUp && _isPressed)
                _coroutineShoot = StartCoroutine(Shoot());
        }

        private void StopShoot()
        {
            if (_coroutineShoot != null)
            {
                StopCoroutine(_coroutineShoot);
                _coroutineShoot = null;
            }
        }

        private IEnumerator Shoot()
        {
            while (true)
            {
                yield return new WaitForSeconds(_intervalBetweenShots);

                PatronImpulse patron = null;

                while (patron == null)
                {
                    patron = _patronHandler.GetPatron();
                    yield return null;
                }

                patron.SetFirePoint(_firePoint);
                patron.gameObject.SetActive(true);
            }
        }     

        private void OnEnable()
            => _joystickHands.Pressed += SetIsPressed;

        private void OnDisable()
            => _joystickHands.Pressed -= SetIsPressed;
    }
}

using System.Collections;
using UnityEngine;

namespace Sandbox
{
    public class FireWeapon : MonoBehaviour
    {
        [SerializeField] private PickUpItem _pickUpItem;
        [SerializeField] private PatronImpulse _patron;
        [SerializeField] private Transform _firePoint;
        [SerializeField, Range(0, 10)] private float _intervalBetweenShots;
        private readonly int _numberCreatedPatron = 2;
        private PatronImpulse[] _createdPatrons;
        private Coroutine _coroutineShoot;

        private void Awake()
        {
            _createdPatrons = new PatronImpulse[_numberCreatedPatron];

            for (int i = 0; i < _numberCreatedPatron; i++)
            {
                _createdPatrons[i] = Instantiate(_patron);
                _createdPatrons[i].SetFirePoint(_firePoint);
                _createdPatrons[i].gameObject.SetActive(false);
            }
        }

        public void StartShoot()
            => _coroutineShoot = StartCoroutine(Shoot());

        public void StopShoot()
        {
            if (_coroutineShoot != null)
                StopCoroutine(_coroutineShoot);
        }

        private IEnumerator Shoot()
        {
            while (true)
            {
                yield return new WaitForSeconds(_intervalBetweenShots);

                PatronImpulse patron = null;

                while (patron == null)
                {
                    for (int i = 0; i < _numberCreatedPatron; i++)
                    {
                        if (!_createdPatrons[i].gameObject.activeInHierarchy)
                        {
                            patron = _createdPatrons[i];
                            break;
                        }
                    }

                    yield return null;
                }

                patron.gameObject.SetActive(true);
            }
        }

        private void OnEnable()
        {
            _pickUpItem.PickedUp += StartShoot;
            _pickUpItem.PickedDown += StopShoot;
        }

        private void OnDisable()
        {
            _pickUpItem.PickedUp -= StartShoot;
            _pickUpItem.PickedDown -= StopShoot;
        }
    }
}

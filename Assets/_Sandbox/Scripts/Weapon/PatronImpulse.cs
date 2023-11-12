using UnityEngine;

namespace Sandbox
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PatronImpulse : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField, Range(1, 100)] private float _impulse;
        private Transform _firePoint;

        public void SetFirePoint(Transform firePoint)
            => _firePoint = firePoint;

        private void OnEnable()
        {
            if (_firePoint != null)
            {
                transform.position = _firePoint.position;
                _rigidbody.velocity = transform.TransformDirection(_firePoint.position) * _impulse;
                _firePoint = null;
            }
        }

        private void OnBecameInvisible()
            => gameObject.SetActive(false);

        private void OnValidate()
            => _rigidbody ??= GetComponent<Rigidbody2D>();
    }
}

using UnityEngine;

namespace Sandbox
{
    public class Balance : MonoBehaviour
    {
        [SerializeField] private float _targetRotation;
        [SerializeField] private float _force;
        [SerializeField] private Rigidbody2D _rigidbody;

        public float GetTargetRotation { get => _targetRotation; }

        public void SetTargetRotation(float targetRotation)
            => _targetRotation = targetRotation;

        private void FixedUpdate()
            => _rigidbody.MoveRotation(Mathf.LerpAngle(_rigidbody.rotation,
                                                       _targetRotation,
                                                       _force * Time.fixedDeltaTime));

        private void OnValidate()
            => _rigidbody ??= GetComponent<Rigidbody2D>();
    }
}

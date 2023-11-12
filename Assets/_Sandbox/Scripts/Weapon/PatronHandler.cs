using UnityEngine;

namespace Sandbox
{
    public class PatronHandler : MonoBehaviour
    {
        [SerializeField, Range(1, 10)] private int _numberCreatedPatron;
        [SerializeField] private PatronImpulse _patron;
        private PatronImpulse[] _createdPatrons;

        private void Awake()
        {
            _createdPatrons = new PatronImpulse[_numberCreatedPatron];

            for (int i = 0; i < _numberCreatedPatron; i++)
            {
                _createdPatrons[i] = Instantiate(_patron);
                _createdPatrons[i].gameObject.SetActive(false);
            }
        }

        public PatronImpulse GetPatron()
        {
            for (int i = 0; i < _numberCreatedPatron; i++)
            {
                if (!_createdPatrons[i].gameObject.activeInHierarchy)
                    return _createdPatrons[i];
            }

            return null;
        }
    }
}

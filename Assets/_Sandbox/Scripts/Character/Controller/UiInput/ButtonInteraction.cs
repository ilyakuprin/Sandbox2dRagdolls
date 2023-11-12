using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sandbox
{
    public class ButtonInteraction : MonoBehaviour
    {
        public event Action ButtonPressed;

        [SerializeField] private Button _button;

        private void Press()
            => ButtonPressed?.Invoke();

        private void OnEnable()
            => _button.onClick.AddListener(Press);

        private void OnDisable()
            => _button.onClick.RemoveListener(Press);
    }
}

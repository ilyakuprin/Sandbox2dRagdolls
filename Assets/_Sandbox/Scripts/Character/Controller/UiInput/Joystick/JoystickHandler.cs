using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sandbox
{
    public abstract class JoystickHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public event Action<bool> Pressed;

        [SerializeField] private RectTransform _joystickArea;
        [SerializeField] private RectTransform _joystickBackground;
        [SerializeField] private Image _joystick;

        [SerializeField] private Color _inactiveJoystickColor;
        [SerializeField] private Color _activeJoystickColor;

        private Vector2 _joystickCreationStartPosition;
        private Vector2 _inputVector;

        protected Vector2 GetInputVector { get => _inputVector; }

        private void Awake()
        {
            _joystick.color = _inactiveJoystickColor;
            _joystickCreationStartPosition = _joystickBackground.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground,
                                                                        eventData.position,
                                                                        null,
                                                                        out Vector2 joystickPosition))
            {
                joystickPosition.x = joystickPosition.x * 2 / _joystickBackground.sizeDelta.x;
                joystickPosition.y = joystickPosition.y * 2 / _joystickBackground.sizeDelta.y;

                _inputVector = new Vector2(joystickPosition.x, joystickPosition.y);

                _inputVector = _inputVector.normalized;

                _joystick.rectTransform.anchoredPosition = new Vector2(_inputVector.x * (_joystickBackground.sizeDelta.x / 2),
                                                                       _inputVector.y * (_joystickBackground.sizeDelta.y / 2));
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _joystick.color = _activeJoystickColor;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickArea,
                                                                        eventData.position,
                                                                        null,
                                                                        out Vector2 joysicBackgroundPosition))
            {
                _joystickBackground.anchoredPosition = new Vector2(joysicBackgroundPosition.x, joysicBackgroundPosition.y);
            }

            Pressed?.Invoke(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _joystick.color = _inactiveJoystickColor;

            _joystickBackground.anchoredPosition = _joystickCreationStartPosition;

            _inputVector = Vector2.zero;
            _joystick.rectTransform.anchoredPosition = Vector2.zero;

            Pressed?.Invoke(false);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Core.UI.AndroidUI
{

    public class VirtualJoystick : MonoBehaviour
    {
        public Image joystickBackground;
        public Image joystickKnob;
        private Vector2 inputVector;

        public Vector2 GetInputDirection()
        {
            return inputVector;
        }

        public void OnDrag(UnityEngine.EventSystems.PointerEventData ped)
        {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground.rectTransform, ped.position, ped.pressEventCamera, out pos))
            {
                pos.x = pos.x / joystickBackground.rectTransform.sizeDelta.x;
                pos.y = pos.y / joystickBackground.rectTransform.sizeDelta.y;

                inputVector = new Vector2(pos.x * 2, pos.y * 2);
                inputVector = inputVector.magnitude > 1.0f ? inputVector.normalized : inputVector;

                // Move joystick knob
                joystickKnob.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBackground.rectTransform.sizeDelta.x / 2), inputVector.y * (joystickBackground.rectTransform.sizeDelta.y / 2));
            }
        }

        public void OnPointerDown(UnityEngine.EventSystems.PointerEventData ped)
        {
            OnDrag(ped);
        }

        public void OnPointerUp(UnityEngine.EventSystems.PointerEventData ped)
        {
            inputVector = Vector2.zero;
            joystickKnob.rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
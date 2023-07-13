using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour, IUpdateSelectedHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private ButtonType _buttonType;
    
    private bool _isPressed;
    
    public void OnUpdateSelected(BaseEventData eventData)
    {
        if (_isPressed)
        {
            switch (_buttonType)
            {
                case ButtonType.Up:
                    InputManager.IsUp = true;
                    break;
                case ButtonType.Down:
                    InputManager.IsDown = true;
                    break;
                case ButtonType.Left:
                    InputManager.IsLeft = true;
                    break;
                case ButtonType.Right:
                    InputManager.IsRight = true;
                    break;
                case ButtonType.Fire:
                    InputManager.IsFire = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
    }

    public enum ButtonType
    {
        Up,
        Down,
        Left,
        Right,
        Fire
    }
}
using UnityEngine;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerMobileInput : MonoBehaviour
{
    [SerializeField] private Vector2 _stickSize = new Vector2(300, 300);
    [SerializeField] private FloatingStick _moveStick;
    [SerializeField] private FloatingStick _shootStick;

    private ETouch.Finger _movementFinger;
    private ETouch.Finger _shootFinger;

    public Vector2 MovementAxis;
    public Vector2 ShootAxis;

    private void OnEnable()
    {
        ETouch.EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleFingerUp;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleFingerUp;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        ETouch.EnhancedTouchSupport.Disable();
    }

    private void HandleFingerDown(ETouch.Finger finger)
    {
        if (_movementFinger == null && finger.screenPosition.x <= Screen.width / 2f)
        {
            _movementFinger = finger;
            _moveStick.gameObject.SetActive(true);
            _moveStick.RectTransform.sizeDelta = _stickSize;
            _moveStick.RectTransform.anchoredPosition = ClampScreenPosition(finger.screenPosition);
            MovementAxis = Vector2.zero;
        }
        if (_shootFinger == null && finger.screenPosition.x > Screen.width / 2f)
        {
            _shootFinger = finger;
            _shootStick.gameObject.SetActive(true);
            _shootStick.RectTransform.sizeDelta = _stickSize;
            _shootStick.RectTransform.anchoredPosition = ClampScreenPosition(finger.screenPosition);
            ShootAxis = Vector2.zero;
        }
    }
    private void HandleFingerUp(ETouch.Finger finger)
    {
        if (finger == _movementFinger)
        {
            _movementFinger = null;
            _moveStick.Knob.anchoredPosition = Vector2.zero;
            _moveStick.gameObject.SetActive(false);
            MovementAxis = Vector2.zero;
        }
        if (finger == _shootFinger)
        {
            _shootFinger = null;
            _shootStick.Knob.anchoredPosition = Vector2.zero;
            _shootStick.gameObject.SetActive(false);
            ShootAxis = Vector2.zero;
        }
    }
    private void HandleFingerMove(ETouch.Finger finger)
    {
        Vector2 knobP;
        float maxMove = _stickSize.x / 2f;
        ETouch.Touch currentTouch = finger.currentTouch;
        
        if (finger == _movementFinger)
        {
            if (Vector2.Distance(currentTouch.screenPosition, _moveStick.RectTransform.anchoredPosition) > maxMove)
                knobP = (currentTouch.screenPosition - _moveStick.RectTransform.anchoredPosition).normalized * maxMove;
            else knobP = currentTouch.screenPosition - _moveStick.RectTransform.anchoredPosition;

            _moveStick.Knob.anchoredPosition = knobP;
            MovementAxis = knobP / maxMove;
        }
        
        if (finger == _shootFinger)
        {
            if (Vector2.Distance(currentTouch.screenPosition, _shootStick.RectTransform.anchoredPosition) > maxMove)
                knobP = (currentTouch.screenPosition - _shootStick.RectTransform.anchoredPosition).normalized * maxMove;
            else knobP = currentTouch.screenPosition - _shootStick.RectTransform.anchoredPosition;

            _shootStick.Knob.anchoredPosition = knobP;
            ShootAxis = knobP / maxMove;
        }
    }

    private Vector2 ClampScreenPosition(Vector2 screenPosition)
    {
        if (screenPosition.x < _stickSize.x / 2) screenPosition.x = _stickSize.x / 2;
        if (screenPosition.y < _stickSize.y / 2) screenPosition.y = _stickSize.y / 2;
        else if (screenPosition.y > Screen.height - _stickSize.y / 2)
            screenPosition.y = Screen.height - _stickSize.y / 2;
        return screenPosition;
    }
}


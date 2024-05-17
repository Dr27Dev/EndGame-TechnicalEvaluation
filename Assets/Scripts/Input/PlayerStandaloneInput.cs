using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStandaloneInput : MonoBehaviour
{
    public Vector2 MovementAxis;
    public Vector2 ShootAxis;

    private Gamepad _gamepad;
    private CustomInput _customInput;

    private void Awake() => _customInput = new CustomInput();

    private void Update()
    {
        MovementAxis = _customInput.Player.Movement.ReadValue<Vector2>();
        ShootAxis = _customInput.Player.Look.ReadValue<Vector2>();
    }

    public void RumblePulse(float lowFrequency, float highFrequency, float duration)
    {
        _gamepad = Gamepad.current;
        if (_gamepad != null)
        {
            _gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
            StartCoroutine(StopRumble(duration, _gamepad));
        }
    }

    private IEnumerator StopRumble(float duration, Gamepad gamepad)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        gamepad.SetMotorSpeeds(0f,0f);
    }

    private void OnEnable() => _customInput.Enable();
    private void OnDisable() => _customInput.Disable();

}

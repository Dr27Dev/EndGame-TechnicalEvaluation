using UnityEngine;

#region ComponentRequirements

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerMobileInput))]
[RequireComponent(typeof(PlayerStandaloneInput))]

#endregion

public class PlayerMovement : MonoBehaviour
{
    private PlayerMobileInput _mobileInput;
    private PlayerStandaloneInput _standaloneInput; 
    private CharacterController _characterController;
    
    [SerializeField] private float _movementSpeed = 1;
    [SerializeField] private float _gravity = 9.81f;
    [SerializeField] private Animator _animator;
    
    private Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        _mobileInput = GetComponent<PlayerMobileInput>();
        _standaloneInput = GetComponent<PlayerStandaloneInput>();
        _characterController = GetComponent<CharacterController>();
        _animator.SetFloat("Speed", 0);
    }

    private void Update()
    {
        Vector3 inputAxis = new Vector3(GetMovementAxis().x, 0, GetMovementAxis().y);
        Vector3 moveDirection = -_movementSpeed * inputAxis;
        _velocity.y -= _gravity * Time.deltaTime;
        
        _characterController.Move((moveDirection + _velocity) * Time.deltaTime);
        _animator.SetFloat("Speed", _characterController.velocity.magnitude);
        
        if (_characterController.isGrounded) _velocity.y = 0f;
    }

    private Vector2 GetMovementAxis()
    { return GameManager.Instance.IsMobile ? _mobileInput.MovementAxis : _standaloneInput.MovementAxis; }
}

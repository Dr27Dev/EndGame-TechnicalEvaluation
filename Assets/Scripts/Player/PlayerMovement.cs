using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _runningSpeed = 13;
    [SerializeField] private float _shootingMoveSpeed = 8;
    [SerializeField] private float _gravity = 9.81f;
    
    private PlayerController _playerController;
    private Vector3 _velocity = Vector3.zero;

    private void Awake() => _playerController = GetComponent<PlayerController>();
    
    private void Update()
    {
        float moveSpeed = _playerController.IsShooting ? _shootingMoveSpeed : _runningSpeed;
        Vector3 inputAxis = new Vector3(_playerController.GetMoveAxis().x, 0, _playerController.GetMoveAxis().y);
        Vector3 moveDirection = -moveSpeed * inputAxis;
        _velocity.y -= _gravity * Time.deltaTime;
        
        _playerController.CharacterController.Move((moveDirection + _velocity) * Time.deltaTime);
        _playerController.Animator.SetFloat("Speed", inputAxis.magnitude);
        
        if (_playerController.CharacterController.isGrounded) _velocity.y = 0f;
    }
}

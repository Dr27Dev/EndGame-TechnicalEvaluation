using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private PlayerMobileInput _mobileInput;
    private PlayerStandaloneInput _standaloneInput; 
    private CharacterController _characterController;
    
    [SerializeField] private float _rotationSpeed = 10;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _mobileInput = GetComponent<PlayerMobileInput>();
        _standaloneInput = GetComponent<PlayerStandaloneInput>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 inputAxis = new Vector3(GetShootAxis().x, 0, GetShootAxis().y);

        if (inputAxis != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(-inputAxis);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
            _animator.SetBool("Shooting", true);
        }
        else _animator.SetBool("Shooting", false);
    }

    private Vector2 GetShootAxis()
    { return GameManager.Instance.IsMobile ? _mobileInput.ShootAxis : _standaloneInput.ShootAxis; }
}

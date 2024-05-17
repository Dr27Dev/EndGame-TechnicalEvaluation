using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region ComponentRequirements

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerMobileInput))]
[RequireComponent(typeof(PlayerStandaloneInput))]

#endregion

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerMobileInput MobileInput;
    [HideInInspector] public PlayerStandaloneInput StandaloneInput;
    [HideInInspector] public CharacterController CharacterController;
    [HideInInspector] public Animator Animator;
    public bool IsShooting;
    
    private void Awake()
    {
        MobileInput = GetComponent<PlayerMobileInput>();
        StandaloneInput = GetComponent<PlayerStandaloneInput>();
        CharacterController = GetComponent<CharacterController>();
        Animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        IsShooting = false;
        Animator.SetFloat("Speed", 0);
        Animator.SetBool("Shooting", IsShooting);
    }

    private void Update() => Animator.SetBool("Shooting", IsShooting);

    public Vector2 GetShootAxis()
    { return GameManager.Instance.IsMobile ? MobileInput.ShootAxis : StandaloneInput.ShootAxis; }
    
    public Vector2 GetMoveAxis()
    { return GameManager.Instance.IsMobile ? MobileInput.MovementAxis : StandaloneInput.MovementAxis; }
}

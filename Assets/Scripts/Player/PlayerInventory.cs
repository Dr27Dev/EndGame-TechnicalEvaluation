using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Image[] _keySlots;
    [SerializeField] private Image _weaponSlot;
    [SerializeField] private Image _grenadeSlot;
    [SerializeField] private GameObject _switchWeaponHint;
    [SerializeField] private TextMeshProUGUI _keylockHint;

    [SerializeField] private PlayerWeapon _playerWeapon;
    [SerializeField] private PlayerGrenade _playerGrenade;
    
    private readonly List<Pickup_Key> _keys = new List<Pickup_Key>();

    private void Awake()
    {
        _playerWeapon = GetComponent<PlayerWeapon>();
        _playerGrenade = GetComponent<PlayerGrenade>();
        DOTween.Restart("KeylockHint_Set");
        DOTween.Play("KeylockHint_Set");
    }

    private void Start()
    {
        foreach (var keySlot in _keySlots) keySlot.gameObject.SetActive(false);
        _grenadeSlot.gameObject.SetActive(false);
        _switchWeaponHint.SetActive(false);
        SwitchWeapon(0);
        _keylockHint.text = "";
        if (!GameManager.Instance.IsMobile)
            GetComponent<PlayerController>().StandaloneInput.Input_SwitchWeapon += SwitchWeapon;
    }

    public void AddKey(Pickup_Key key)
    {
        _keys.Add(key);
        _keySlots[key.DoorCode].gameObject.SetActive(true);
    }

    public void EnableGrenade()
    {
        _grenadeSlot.gameObject.SetActive(true);
        if (!GameManager.Instance.IsMobile) _switchWeaponHint.SetActive(true);
    }
    
    public void SwitchWeapon()
    {
        if (!_playerWeapon.enabled)
        {
            _playerWeapon.enabled = true;
            _playerGrenade.enabled = false;
            _weaponSlot.color = new Color(1,1,1, 1);
            _grenadeSlot.color = new Color(1,1,1, 0.3f);
        }
        else if (_grenadeSlot.gameObject.activeSelf)
        {
            _playerWeapon.enabled = false;
            _playerGrenade.enabled = true;
            _grenadeSlot.color = new Color(1,1,1, 1);
            _weaponSlot.color = new Color(1,1,1, 0.3f);
        }
    }

    public void SwitchWeapon(int weapon) // 0 = scar, 1 = grenade
    {
        if (weapon == 0)
        {
            _playerWeapon.enabled = true;
            _playerGrenade.enabled = false;
            _weaponSlot.color = new Color(1,1,1, 1);
            _grenadeSlot.color = new Color(1,1,1, 0.3f);
        }
        else if(weapon == 1)
        {
            _playerWeapon.enabled = false;
            _playerGrenade.enabled = true;
            _grenadeSlot.color = new Color(1,1,1, 1);
            _weaponSlot.color = new Color(1,1,1, 0.3f);
        }
    }
    
    public bool CheckHasKey(int doorCode)
    {
        if (_keys.Count > 0)
        {
            if (_keys.Any(key => key.DoorCode == doorCode))
            {
                _keySlots[doorCode].gameObject.SetActive(false);
                return true;
            }
        }
        _keylockHint.text = $"You need the key <b>#{doorCode}</b> to access";
        DOTween.Restart("KeylockHint_In");
        DOTween.Play("KeylockHint_In");
        return false;
    }
}

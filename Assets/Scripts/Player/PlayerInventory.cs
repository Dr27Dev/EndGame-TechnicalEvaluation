using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject[] _keySlots;
    [SerializeField] private GameObject _grenadeSlot;
    [SerializeField] private TextMeshProUGUI _keylockHint;
    
    private List<Pickup_Key> _keys = new List<Pickup_Key>();

    private void Awake()
    {
        foreach (var keySlot in _keySlots) keySlot.SetActive(false);
        _grenadeSlot.SetActive(false);
        _keylockHint.text = "";
        DOTween.Restart("KeylockHint_Set");
        DOTween.Play("KeylockHint_Set");
    }

    public void AddKey(Pickup_Key key)
    {
        _keys.Add(key);
        _keySlots[key.DoorCode].SetActive(true);
    }

    public bool CheckHasKey(int doorCode)
    {
        if (_keys.Count > 0)
        {
            if (_keys.Any(key => key.DoorCode == doorCode))
            {
                _keySlots[doorCode].SetActive(false);
                return true;
            }
        }
        _keylockHint.text = $"You need the key <b>#{doorCode}</b> to access";
        DOTween.Restart("KeylockHint_In");
        DOTween.Play("KeylockHint_In");
        return false;
    }
}

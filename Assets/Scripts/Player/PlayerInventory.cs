using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<Pickup_Key> _keys = new List<Pickup_Key>();

    public void AddKey(Pickup_Key key) => _keys.Add(key);

    public bool CheckHasKey(int doorCode)
    {
        if (_keys.Count > 0) return _keys.Any(key => key.DoorCode == doorCode);
        return false;
    }
}

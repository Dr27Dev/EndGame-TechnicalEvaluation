using UnityEngine;
public class Pickup : MonoBehaviour
{
    protected PlayerInventory _playerInventory;
    private void Start() => _playerInventory = GameManager.Instance.Player.GetComponent<PlayerInventory>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) HandlePickup(ref _playerInventory);
    }
    protected virtual void HandlePickup(ref PlayerInventory inventory)
    {
        gameObject.SetActive(false);
    }
}

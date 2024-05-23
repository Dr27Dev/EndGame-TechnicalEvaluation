using UnityEngine;
public class Pickup : MonoBehaviour
{
    protected PlayerInventory _playerInventory;
    [SerializeField] private GameObject _mesh;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _pickupSFX;
    private void Start() => _playerInventory = GameManager.Instance.Player.GetComponent<PlayerInventory>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) HandlePickup(ref _playerInventory);
    }
    protected virtual void HandlePickup(ref PlayerInventory inventory)
    {
        _audioSource.PlayOneShot(_pickupSFX);
        _mesh.SetActive(false);
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 0.4f);
    }
}

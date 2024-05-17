using UnityEngine;
using UnityEngine.XR;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10;
    private PlayerController _playerController;

    private void Awake() => _playerController = GetComponent<PlayerController>();
    private void Update()
    {
        Vector3 shootAxis = new Vector3(_playerController.GetShootAxis().x, 0, _playerController.GetShootAxis().y);
        Vector3 moveAxis = new Vector3(_playerController.GetMoveAxis().x, 0, _playerController.GetMoveAxis().y);
        
        if (shootAxis != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(-shootAxis);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
            _playerController.IsShooting = true;
        }
        else
        {
            if (moveAxis != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(-moveAxis);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
            }
            _playerController.IsShooting = false;
        }   
    }

}

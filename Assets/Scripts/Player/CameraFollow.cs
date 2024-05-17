using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    private void Update()
    {
        var playerPos = _playerTransform.position;
        Vector3 position = new Vector3(playerPos.x, transform.position.y, playerPos.z);
        transform.position = position;
    }
}

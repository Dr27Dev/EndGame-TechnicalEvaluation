using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float disableTime = 2f;
    private float disableTimer;

    private Rigidbody rb;

    private void Awake() => rb = GetComponent<Rigidbody>();

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            disableTimer += Time.deltaTime;
            if (disableTimer >= disableTime) gameObject.SetActive(false);
        }
    }
    
    public void ShotBullet(Transform bulletSpawnPos, float bulletSpeed)
    {
        disableTimer = 0;
        if (rb != null) rb.velocity = bulletSpawnPos.forward * bulletSpeed;
    }
}

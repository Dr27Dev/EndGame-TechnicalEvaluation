using System;
using Unity.VisualScripting;
using UnityEngine;

public enum WeaponTarget { Enemy, Player }
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _disableTime = 2f;
    [SerializeField] private ParticleSystem _impactVFX;
    [SerializeField] private Transform _impactVFXSpawnPoint;
    private float _disableTimer;
    private Rigidbody _rb;
    public WeaponTarget Target = WeaponTarget.Enemy;

    private void Awake() => _rb = GetComponent<Rigidbody>();

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            _disableTimer += Time.deltaTime;
            if (_disableTimer >= _disableTime) gameObject.SetActive(false);
        }
    }
    
    public void ShotBullet(Transform bulletSpawnPos, float bulletSpeed)
    {
        _disableTimer = 0;
        if (_rb != null) _rb.velocity = bulletSpawnPos.forward * bulletSpeed;
    }

    private void BulletCollision()
    {
        var impactFX = Instantiate(_impactVFX);
        impactFX.transform.position = _impactVFXSpawnPoint.position;
        impactFX.Play();
        Destroy(impactFX.gameObject,0.5f);
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        switch (Target)
        {
            case WeaponTarget.Enemy:
                if (other.transform.CompareTag("Enemy")) other.GetComponent<StatsHandler>().ReceiveDamage(1);
                BulletCollision();
                break;
            case WeaponTarget.Player:
                if (other.transform.CompareTag("Player")) other.GetComponent<StatsHandler>().ReceiveDamage(1);
                BulletCollision();
                break;
        }
    }

    private void OnCollisionEnter(Collision other) {if (other.gameObject.CompareTag("Environment")) BulletCollision();}
}

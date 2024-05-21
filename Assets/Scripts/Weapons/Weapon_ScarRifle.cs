using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_ScarRifle : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPos;
    [SerializeField] private Transform _bulletsParent;
    [SerializeField] private ParticleSystem _muzzleVFX;
    private readonly List<Bullet> _bullets = new List<Bullet>();
    
    [SerializeField] private float _attackSpeed = 2f;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _shootDelay = 0.5f;
    [SerializeField] private WeaponTarget _target = WeaponTarget.Enemy;
    
    private float _shootTimer;
    private bool _isShooting;
    private bool _initialShot;

    public bool IsShooting;

    private void Start()
    {
        _shootTimer = 0f;
        _isShooting = false;
        _initialShot = false;
        SetBulletPool(20);
    }

    protected virtual void Update()
    {
        _shootTimer += Time.deltaTime;

        if (IsShooting && !_initialShot)
        {
            _initialShot = true;
            StartCoroutine(ShootWithInitialDelay(_shootDelay));
        }

        if (IsShooting && _initialShot && _shootTimer >= 1 / _attackSpeed && !_isShooting)
        {
            Shoot();
            _shootTimer = 0f;
        }

        if (!IsShooting)
        {
            _initialShot = false;
            _muzzleVFX.Stop();
        }
    }

    protected IEnumerator ShootWithInitialDelay(float delay)
    {
        _isShooting = true;
        yield return new WaitForSeconds(delay);
        Shoot();
        _isShooting = false;
    }

    protected virtual void Shoot()
    {
        Bullet bullet = GetBulletFromPool();
        bullet.ShotBullet(_bulletSpawnPos, _bulletSpeed);
        _muzzleVFX.Play();
    }
    
    protected void SetBulletPool(int poolSize)
    {
        for (int i = 0; i < poolSize; ++i)
        {
            GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPos.position, _bulletSpawnPos.rotation);
            bullet.transform.SetParent(_bulletsParent);
            bullet.SetActive(false);
            bullet.GetComponent<Bullet>().Target = _target;
            _bullets.Add(bullet.GetComponent<Bullet>());
        }
    }

    private Bullet GetBulletFromPool()
    {
        Bullet bullet = _bullets.Find(b => !b.gameObject.activeSelf);
        if (bullet == null)
        {
            GameObject newBullet = Instantiate(_bulletPrefab, _bulletSpawnPos.position, _bulletSpawnPos.rotation);
            bullet = newBullet.GetComponent<Bullet>();
            bullet.Target = _target;
            _bullets.Add(bullet);
        }
        bullet.gameObject.SetActive(true);
        bullet.transform.position = _bulletSpawnPos.position;
        bullet.transform.rotation = _bulletSpawnPos.rotation;
        return bullet;
    }
}

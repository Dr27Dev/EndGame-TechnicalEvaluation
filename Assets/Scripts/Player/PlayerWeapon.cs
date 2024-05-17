using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPos;
    [SerializeField] private Transform _bulletsParent;
    private readonly List<Bullet> _bullets = new List<Bullet>();
    
    [SerializeField] private float _attackSpeed = 2f;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _shootDelay = 0.5f;

    private PlayerController _playerController;
    private float _shootTimer;
    private bool _isShooting;
    private bool _initialShot;

    private void Awake() => _playerController = GetComponent<PlayerController>();

    private void Start()
    {
        _shootTimer = 0f;
        _isShooting = false;
        _initialShot = false;
        InitPool(20);
    }

    private void Update()
    {
        _shootTimer += Time.deltaTime;

        if (_playerController.IsShooting && !_initialShot)
        {
            _initialShot = true;
            StartCoroutine(ShootWithInitialDelay(_shootDelay));
        }

        if (_playerController.IsShooting && _initialShot && _shootTimer >= 1 / _attackSpeed && !_isShooting)
        {
            Shoot();
            _shootTimer = 0f;
        }

        if (!_playerController.IsShooting) _initialShot = false;
    }

    private IEnumerator ShootWithInitialDelay(float delay)
    {
        _isShooting = true;
        yield return new WaitForSeconds(delay);
        Shoot();
        _isShooting = false;
    }

    private void Shoot()
    {
        Bullet bullet = GetBulletFromPool();
        bullet.ShotBullet(_bulletSpawnPos, _bulletSpeed);
        if (!GameManager.Instance.IsMobile)
            _playerController.StandaloneInput.RumblePulse(0.1f, 1f, 0.1f);
    }
    
    private void InitPool(int poolSize)
    {
        for (int i = 0; i < poolSize; ++i)
        {
            GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPos.position, _bulletSpawnPos.rotation);
            bullet.transform.SetParent(_bulletsParent);
            bullet.SetActive(false);
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
            _bullets.Add(bullet);
        }
        bullet.gameObject.SetActive(true);
        bullet.transform.position = _bulletSpawnPos.position;
        bullet.transform.rotation = _bulletSpawnPos.rotation;
        return bullet;
    }
}
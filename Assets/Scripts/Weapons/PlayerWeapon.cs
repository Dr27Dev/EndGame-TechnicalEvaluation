public class PlayerWeapon : Weapon_ScarRifle
{
    private PlayerController _playerController;
    private void Awake() => _playerController = GetComponent<PlayerController>();

    protected override void Update()
    {
        base.Update();
        IsShooting = _playerController.IsShooting;
    }

    protected override void Shoot()
    {
        base.Shoot();
        if (!GameManager.Instance.IsMobile)
            _playerController.StandaloneInput.RumblePulse(0.1f, 1f, 0.1f);
    }
}
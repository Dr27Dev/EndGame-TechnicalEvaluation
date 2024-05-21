public class EnemyWeapon : Weapon_ScarRifle
{
    private EnemyController _enemy;
    private void Awake() => _enemy = GetComponent<EnemyController>();

    protected override void Update()
    {
        base.Update();
        IsShooting = _enemy.IsShooting;
        _enemy.Animator.SetBool("Shooting", IsShooting);
    }
}
using UnityEngine;

public class EnemyStats : StatsHandler
{
    [SerializeField] private ParticleSystem _deathVFX;
    protected override void Die()
    {
        gameObject.SetActive(false);
        var deathFX = Instantiate(_deathVFX);
        deathFX.transform.position = transform.position;
        deathFX.Play();
        Destroy(deathFX, 1);
    }
}

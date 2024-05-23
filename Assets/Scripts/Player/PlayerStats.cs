using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : StatsHandler
{
    [SerializeField] private RawImage _lifePanel;
    [SerializeField] private AnimationCurve _lifeCurve;
    [SerializeField] private ParticleSystem _deathVFX;
    [SerializeField] private DeathScreen _deathScreen;
    
    protected override void Start()
    {
        base.Start();
        _lifePanel.material.SetFloat("_VisibilityAmount", 0);
    }

    public override void ReceiveDamage(int damageAmount)
    {
        base.ReceiveDamage(damageAmount);
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        float normalizedHealth = (float)_health / _maxHealth;
        float visibilityAmount = _lifeCurve.Evaluate(1 - normalizedHealth);
        _lifePanel.material.SetFloat("_VisibilityAmount", visibilityAmount);
    }

    public void Heal()
    {
        _health = _maxHealth;
        _lifePanel.material.SetFloat("_VisibilityAmount", 0);
    }
    protected override void Die()
    {
        gameObject.SetActive(false);
        var deathFX = Instantiate(_deathVFX);
        deathFX.transform.position = transform.position;
        deathFX.Play();
        _deathScreen.Die();
        Destroy(deathFX, 1);
    }
}

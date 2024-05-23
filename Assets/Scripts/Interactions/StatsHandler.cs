using UnityEngine;

public class StatsHandler : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] protected int _maxHealth = 10;
    [SerializeField] private CharacterAudio _characterAudio;

    protected virtual void Start() => _health = _maxHealth;

    public virtual void ReceiveDamage(int damageAmount)
    {
        _health -= damageAmount;
        _characterAudio.GetHurt();
        if (_health <= 0) Die();
    }

    protected virtual void Die()
    {
        print($"{gameObject.transform.name} died");
    }
}

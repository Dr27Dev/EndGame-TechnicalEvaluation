using UnityEngine;

public class StatsHandler : MonoBehaviour
{
    [SerializeField] int _health;
    [SerializeField] private int _maxHealth = 10;

    private void Start() => _health = _maxHealth;

    public void ReceiveDamage(int damageAmount)
    {
        _health -= damageAmount;
        if (_health <= 0) Die();
    }

    protected virtual void Die()
    {
        print($"{gameObject.transform.name} died");
    }
}

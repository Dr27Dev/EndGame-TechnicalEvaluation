using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private int _explosionDamage = 10;
    [SerializeField] private LayerMask _enemiesLayer;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private GameObject _explosionEffect;
    [SerializeField] private float _explosionGroundOffset = 0;

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius, _enemiesLayer);
        foreach (Collider col in colliders) col.GetComponent<EnemyStats>().ReceiveDamage(_explosionDamage);
        if (Physics.Raycast(transform.position, Vector3.down, out var hit, 10f, _groundLayer))
        {
            var effect = Instantiate(_explosionEffect);
            effect.transform.position = hit.point + new Vector3(0, _explosionGroundOffset, 0);
            effect.GetComponent<ParticleSystem>().Play();
            Destroy(effect, 1);
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
    
    private void OnCollisionEnter(Collision other) => Explode();
}

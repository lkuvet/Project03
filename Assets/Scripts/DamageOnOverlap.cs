using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageOnOverlap : MonoBehaviour
{

    [SerializeField] private int _damage = 10;
    [SerializeField] private float _damageFrequency = .2f;

    private Collider2D _triggerCollider;

    private void Awake()
    {
        _triggerCollider = GetComponent<Collider2D>();
        _triggerCollider.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
            Debug.Log("Attack!");
            _triggerCollider.enabled = false;
            Invoke(nameof(ReadyAttack), _damageFrequency);
        }
    }

    private void ReadyAttack()
    {
        _triggerCollider.enabled = true;
    }
}

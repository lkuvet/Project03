using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class AttackSystem : MonoBehaviour
{
    public string Name { get; private set; }
    [Header("General")]
    int _damage = 1;
    float _moveSpeed = 2;
    float _lifetime = 2;
    float _cooldown = 1;
    Projectile _projectile = null;

    Projectile _projectile2 = null;

    AudioClip _spawnSound = null;

    [Header("Detection")]
    private bool _onlyFireIfNearbyTargets = false;
    private float _detectionRadius = 10;
    private ContactFilter2D _targetFilter;
    private List<Collider2D> _targetsDetected = new List<Collider2D>();

    private Vector2 _direction = Vector2.zero;

    public void SetupAttack(AttackData data)
    {
        Name = data.Name;
        _damage = data.Damage;
        _moveSpeed = data.MoveSpeed;
        _lifetime = data.Lifetime;
        _cooldown = data.Cooldown;
        _projectile = data.Projectile;
        _onlyFireIfNearbyTargets = data.OnlyFireIfNearbyTargets;
        _detectionRadius = data.DetectionRadius;
        _targetFilter = data.TargetFilter;
        _spawnSound = data.SpawnSound;
    }

    private void Start()
    {
        InvokeRepeating(nameof(Attack), _cooldown, _cooldown);
    }

    public void Attack()
    {

        if (_onlyFireIfNearbyTargets)
        {
            Collider2D target = DetectClosestTarget();
            if (target == null) return;

            _direction = target.transform.position - transform.position;
            _direction.Normalize();
            Debug.Log(target.gameObject.name);
        }

        Projectile newProjectile = Instantiate
            (_projectile, transform.position, Quaternion.identity);
        newProjectile.Spawn(_direction, _damage, _moveSpeed);
        Destroy(newProjectile.gameObject, _lifetime);
    }

    private Collider2D DetectClosestTarget()
    {
        Physics2D.OverlapCircle(transform.position, _detectionRadius,
            _targetFilter, _targetsDetected);
        if (_targetsDetected == null) return null;

        Collider2D closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D target in _targetsDetected)
        {
            Vector3 distanceVector = target.transform.position - transform.position;
            float currentDistance = distanceVector.sqrMagnitude;
            if (currentDistance <= closestDistance)
            {
                closestTarget = target;
                closestDistance = currentDistance;
            }
        }
        return closestTarget;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }

}
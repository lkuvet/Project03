using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackData_",
    menuName = "ScriptableObjects/AttackData")]

public class AttackData : ScriptableObject
{
    [Header("General")]
    [SerializeField] string _name = "Default";
    [SerializeField] int _damage = 1;
    [SerializeField] float _moveSpeed = 2;
    [SerializeField] float _lifetime = 2;
    [SerializeField] float _cooldown = 1;
    [SerializeField] Projectile _projectile = null;

    [SerializeField] AudioClip _spawnSound = null;

    [Header("Detection")]
    [SerializeField] private bool _onlyFireIfNearbyTargets = false;
    [SerializeField] private float _detectionRadius = 10;
    [SerializeField] private ContactFilter2D _targetFilter;
    private List<Collider2D> _targetsDetected = new List<Collider2D>();

    public string Name => _name;
    public int Damage => _damage;
    public float MoveSpeed => _moveSpeed;
    public float Lifetime => _lifetime;
    public float Cooldown => _cooldown;
    public Projectile Projectile => _projectile;
    public bool OnlyFireIfNearbyTargets => _onlyFireIfNearbyTargets;
    public float DetectionRadius => _detectionRadius;
    public ContactFilter2D TargetFilter => _targetFilter;
    public AudioClip SpawnSound => _spawnSound;


}
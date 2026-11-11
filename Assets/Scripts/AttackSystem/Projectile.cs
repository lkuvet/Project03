using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] AudioClip _spawnSound;
    private int _damage = 5;

    private float _moveSpeed;
    private Vector2 _direction = Vector2.zero;

    public void Spawn()
    {

    }

    public void FixedUpdate()
    {
        Vector2 offsetPost = _direction * _moveSpeed * Time.deltaTime;
        _rb.MovePosition(_rb.position + offsetPost);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
            Debug.Log("Weapon Damage: " + _damage);
            Destroy(gameObject);
        }
    }

    public void Spawn(Vector2 direction, int damage, float moveSpeed)
    {
        _direction = direction.normalized;
        _damage = damage;
        _moveSpeed = moveSpeed;
    }
}

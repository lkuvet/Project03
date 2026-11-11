using UnityEngine;

public class Enemy : MonoBehaviour
{
    private PlayerCharacter _player;
    [SerializeField] private float _moveSpeed = 1;

    private Rigidbody2D _rigidbody2D;

    public void Initialize(PlayerCharacter player)
    {
        _player = player;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (_player == null) return;

        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();

        Vector2 offsetPos = direction * _moveSpeed * Time.deltaTime;

        _rigidbody2D.MovePosition(_rigidbody2D.position + offsetPos);
    }

    public void Spawn(Enemy enemyToSpawn, Vector2 position)
    {
        Enemy newEnemy = Instantiate
            (enemyToSpawn, position, Quaternion.identity);
        newEnemy.Initialize(_player);
    }
}
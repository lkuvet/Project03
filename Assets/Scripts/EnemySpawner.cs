using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemySpawner : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerCharacter _playerCharacter;
    [Header("Spawn Settings")]
    [SerializeField] private Enemy[] _possibleEnemiesToSpawn;
    [SerializeField] private float _spawnRate = 1;
    [SerializeField] private float _timeBewteenSpawnRateChange = 10;
    [SerializeField] private float _spawnRateReductionAmount = 0.1f;
    [SerializeField] private float _minSpawnRate = .2f;

    [SerializeField] private LayerMask _layersToTest;
    [SerializeField] private float _spawnDistanceFromPlayer = 3;

    private int _maxSpawnAttempts = 4;
    private float _timeSinceLastSpawnRateChange = 0;
    private Coroutine _spawnRoutine;

    public void Spawn(Enemy enemyToSpawn, Vector2 position)
    {
        Enemy newEnemy = Instantiate
            (enemyToSpawn, position, Quaternion.identity);
        newEnemy.Initialize(_playerCharacter);
    }

    public void Start()
    {
        StartSpawning();
    }

    private IEnumerator SpawnRoutine()
    {
        Vector2 spawnPoint;

        while (_playerCharacter != null)
        {
            yield return new WaitForSeconds(_spawnRate);
            if (_playerCharacter == null)
            {
                StopSpawning();
                yield break;
            }
            yield return new WaitForSeconds(_spawnRate);
            spawnPoint = GetValidWorldSpawnPoint();
            if (spawnPoint != Vector2.zero)
            {
                Debug.Log("Spawn: " + spawnPoint);
                Spawn(ChooseRandomEnemy(), spawnPoint);
            }
            //while (true)
            // {
            //  yield return new WaitForSeconds(_spawnRate);
            // spawnPoint = GetValidWorldSpawnPoint();
            // if (spawnPoint != Vector2.zero)
            // {
            //    Debug.Log("Spawn: " + spawnPoint);
            //    Spawn(ChooseRandomEnemy(), spawnPoint);
            //   // }
        }
    }

    public void StopSpawning()
    {
        if (_spawnRoutine != null)
            StopCoroutine(_spawnRoutine);
    }

    public void StartSpawning()
    {
        if (_spawnRoutine != null)
            StopCoroutine(_spawnRoutine);
        _spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public Vector2 GetValidWorldSpawnPoint()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector2 playerPos = new Vector2
            (_playerCharacter.transform.position.x,
            _playerCharacter.transform.position.y);
        Vector2 testPoint = playerPos
            + (randomDirection * _spawnDistanceFromPlayer);
        for (int i = 0; i < _maxSpawnAttempts; i++)
        {
            if (Physics2D.OverlapCircle
                (testPoint, .5f, _layersToTest) == null)
                return testPoint;
            else
                testPoint = new Vector2(testPoint.y, -testPoint.x);
        }
        Debug.Log("Could not spawn");
        return testPoint = Vector2.zero;
    }

    private Enemy ChooseRandomEnemy()
    {
        int randomEnemyIndex;
        randomEnemyIndex = Random.Range
            (0, _possibleEnemiesToSpawn.Length);
        return _possibleEnemiesToSpawn[randomEnemyIndex];
    }

    private void Update()
    {
        _timeSinceLastSpawnRateChange += Time.deltaTime;
        if (_timeSinceLastSpawnRateChange >= _timeBewteenSpawnRateChange)
        {
            _spawnRate -= _spawnRateReductionAmount;
            if (_spawnRate < _minSpawnRate)
                _spawnRate = _minSpawnRate;

            _timeSinceLastSpawnRateChange = 0;
        }
    }

    public void DestroyAllEnemies()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>
            (FindObjectsSortMode.None);
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }
}

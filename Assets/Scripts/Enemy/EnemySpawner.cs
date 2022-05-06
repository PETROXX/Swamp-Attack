using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(EnemyWave))]

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _zombie;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _firstWaveSpawnCooldown;
    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private int _maxEnemyCount;

    private float _spawnRate;
    private int _enemiesPerSpawn = 1;

    private EnemyWave _wave;
    private bool _isWaveEnabled;

    private void Start()
    {
        _wave = GetComponent<EnemyWave>();
        _spawnRate = _firstWaveSpawnCooldown;
        _isWaveEnabled = true;
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        _enemiesPerSpawn = _wave.WaveNumber + Random.Range(0, _wave.WaveNumber + 1);

        for (int i = 0; i < _enemiesPerSpawn; i++)
        {
            GameObject enemy = Instantiate(_zombie, _spawnPoint.position, Quaternion.identity);

            if (_enemies.Count >= _maxEnemyCount)
            {
                _enemies.RemoveAt(0);
                _enemies.Add(enemy);
            }
            else
                _enemies.Add(enemy);

        }

        yield return new WaitForSeconds(_spawnRate);

        if (_isWaveEnabled)
            StartCoroutine(Spawn());
    }

    public void StopSpawner()
    {
        _isWaveEnabled = false;
    }

    public void RestartWave()
    {
        _isWaveEnabled = true;
        StartCoroutine(Spawn());
        _spawnRate = _firstWaveSpawnCooldown / Mathf.Pow(_wave.WaveNumber, 1/3);
    }

    public void SetEnemiesPerSpawnAndSpawnRate(int enemiesPerSpawn, float spawnRate)
    {
        _spawnRate = spawnRate;
        _enemiesPerSpawn = enemiesPerSpawn;
    }
}

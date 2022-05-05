using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Wave))]

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _zombie;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _firstWaveSpawnCooldown;
    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private int _maxEnemyCount;

    private float _spawnCooldown;
    private int _enemyPerSpawn;

    private Wave _wave;
    private bool _isWaveEnabled;

    private void Start()
    {
        _spawnCooldown = _firstWaveSpawnCooldown;
        _wave = GetComponent<Wave>();
        _isWaveEnabled = true;
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        _enemyPerSpawn = _wave.WaveNumber + Random.Range(0, _wave.WaveNumber + 1);

        for (int i = 0; i < _enemyPerSpawn; i++)
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

        yield return new WaitForSeconds(_spawnCooldown);

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
        _spawnCooldown = _firstWaveSpawnCooldown / Mathf.Pow(_wave.WaveNumber, 1/3);
    }
}

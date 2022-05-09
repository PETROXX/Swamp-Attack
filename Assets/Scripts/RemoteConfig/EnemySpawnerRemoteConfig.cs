using System;
using UnityEngine;
using Unity.RemoteConfig;

public class EnemySpawnerRemoteConfig : MonoBehaviour
{
    [SerializeField] private EnemyWave _enemyWave;
    [SerializeField] private EnemySpawner _enemySpawner;

    public struct userAttributes { }
    public struct appAttributes { }

    private const string _enemySpawnRate = "spawnRate";
    private const string _waveLenght = "enemySpeed";
    private const string _enemiesPerSpawn = "enemiesPerSpawn";


    private void Awake()
    {
        ConfigManager.FetchCompleted += GetDataFromResponse;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    private void GetDataFromResponse(ConfigResponse obj)
    {
        float spawnRate = ConfigManager.appConfig.GetFloat(_enemySpawnRate);
        float waveLength = ConfigManager.appConfig.GetFloat(_waveLenght);
        int enemiesPerSpawn = ConfigManager.appConfig.GetInt(_enemiesPerSpawn);

        _enemyWave.SetWaveLeght(waveLength);
        _enemySpawner.SetEnemiesPerSpawnAndSpawnRate(enemiesPerSpawn, spawnRate);
    }
}

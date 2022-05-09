using System;
using UnityEngine;
using Unity.RemoteConfig;

public class EnemyRemoteConfig : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    private const string _health = "enemyHealth";
    private const string _speed = "enemySpeed";
    private const string _damage = "enemyDamage";

    public event Action<float, float, float> OnParametersLoaded;

    private void Awake()
    {
        ConfigManager.FetchCompleted += GetDataFromResponse;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    private void GetDataFromResponse(ConfigResponse obj)
    {
        float health  = ConfigManager.appConfig.GetFloat(_health);
        float speed = ConfigManager.appConfig.GetFloat(_speed);
        float damage = ConfigManager.appConfig.GetFloat(_damage);

        OnParametersLoaded?.Invoke(health, speed, damage);
    }
}

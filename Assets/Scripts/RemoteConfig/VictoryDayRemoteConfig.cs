using System;
using Unity.RemoteConfig;
using UnityEngine;
using UnityEngine.UI;

public class VictoryDayRemoteConfig : MonoBehaviour
{
    [SerializeField] private Sprite _victoryDayBackground;
    [SerializeField] private Image _backgroundImage;

    public struct userAttributes { }
    public struct appAttributes { }

    private const string _isVictoryDay = "isVictoryDay";

    private void Awake()
    {
        ConfigManager.FetchCompleted += GetDataFromResponse;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    private void GetDataFromResponse(ConfigResponse obj)
    {
        bool isVictoryDay = ConfigManager.appConfig.GetBool(_isVictoryDay);

        if (isVictoryDay)
            DoVictoryDay();
    }

    private void DoVictoryDay()
    {
        print("wow! it's really victory day!");
    }
}

using System.Diagnostics;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(EnemySpawner))]

public class EnemyWave : MonoBehaviour
{
    private Stopwatch _waveStopwatch;
    private Stopwatch _cooldownStopwatch;

    [SerializeField] private int _waveNumber;
    [Tooltip("Длина первой волны, потом это значение будет увеличиваться по формуле")]
    [SerializeField] private float _basicWaveLength;
    [SerializeField] private float _waveCooldown;
    [SerializeField] private TMP_Text _timerText;

    [SerializeField] private Transform _progressbar;
    [SerializeField] private TMP_Text _waveText;

    [SerializeField] private TextController _textController;

    private EnemySpawner _spawner;

    private float _waveLength;
    private bool _isWaveEnded;

    public int WaveNumber => _waveNumber;

    private void Start()
    {
        _spawner = GetComponent<EnemySpawner>();
        _waveLength = _basicWaveLength;
        _waveNumber = 1;
        _cooldownStopwatch = new Stopwatch();
        _waveStopwatch = new Stopwatch();
        _waveStopwatch.Start();
        _waveText.text = $"Wave {_waveNumber}";
    }

    private void Update()
    {
        if (!_isWaveEnded)
        {
            _timerText.text = $"{_waveStopwatch.Elapsed.Minutes}:{_waveStopwatch.Elapsed.Seconds}";

            if (_waveStopwatch.Elapsed.TotalSeconds >= _waveLength)
                EndWave();

            float xSize = _waveStopwatch.ElapsedMilliseconds / 1000f / _waveLength;
            _progressbar.localScale = new Vector3(xSize, 1, 1);
        }
        else
        {
            _timerText.text = $"{_cooldownStopwatch.Elapsed.Minutes}:{_cooldownStopwatch.Elapsed.Seconds}";

            if (_cooldownStopwatch.Elapsed.TotalSeconds >= _waveCooldown)
                EndCooldown();
        }
    }

    private void EndWave()
    {
        _isWaveEnded = true;
        _waveNumber++;
        _waveLength = _basicWaveLength * Mathf.Sqrt(_waveNumber);
        _waveStopwatch.Reset();
        _cooldownStopwatch.Start();
        _spawner.StopSpawner();
        _waveText.text = $"Cooldown {_waveCooldown}s";
        _textController.SetText("Wave completed!", Color.green, 5f);
    }

    private void EndCooldown()
    {
        _isWaveEnded = false;
        _cooldownStopwatch.Reset();
        _waveStopwatch.Start();
        _spawner.RestartWave();
        _waveText.text = $"Wave {_waveNumber}";
    }

    public void SetWaveLeght(float length)
    {
        _basicWaveLength = length;
    }
}

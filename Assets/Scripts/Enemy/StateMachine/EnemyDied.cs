using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDied : Transition
{
    private Enemy _enemy;

    private void OnEnable()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (_enemy.IsDead)
            NeedTransit = true;
    }
}

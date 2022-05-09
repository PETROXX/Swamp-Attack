using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeftAttackZone : Transition
{
    [SerializeField] private float _transitionRange;

    private void Update()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) > _transitionRange)
            NeedTransit = true;
    }
}

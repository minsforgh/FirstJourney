using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatrolInitializer", menuName = "Initializers/PatrolInitializer", order = 6)]
public class PatrolInitializer : IComponentInitializer
{
    public override void Initialize(GameObject enemy, IEnemySettings settings)
    {
        if (settings != null && settings.PatrolSettings != null)
        {
            InitializePatrol(enemy, settings.PatrolSettings);
        }
    }

    private void InitializePatrol(GameObject enemy, PatrolSettings patrolSettings)
    {
        if (patrolSettings.doesPatrol)
        {
            var patrol = enemy.transform.parent.gameObject.AddComponent<Patrol>();
            patrol.DoesPatrol = patrolSettings.doesPatrol;
            patrol.patrolRoutePrefab = patrolSettings.patrolRoute;
            patrol.patrolSpeed = patrolSettings.patrolSpeed;
        }
    }
}


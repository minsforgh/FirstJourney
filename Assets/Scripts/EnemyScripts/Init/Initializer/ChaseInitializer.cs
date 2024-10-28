using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChaseInitializer", menuName = "Initializers/ChaseInitializer", order = 2)]
public class ChaseInitializer : IComponentInitializer
{
    public override void Initialize(GameObject enemy, IEnemySettings settings)
    {   
        ChaseSettings chaseSettings = settings?.ChaseSettings;
        
        if (chaseSettings != null)
        {
           InitializeChase(enemy, chaseSettings);
        }
    }

    private void InitializeChase(GameObject enemy, ChaseSettings chaseSettings)
    {
        switch (chaseSettings.chaseType)
        {
            case ChaseSettings.ChaseType.Normal:
                var chase = enemy.AddComponent<EnemyChase>();
                chase.chaseSpeed = chaseSettings.chaseSpeed;
                chase.chaseRange = chaseSettings.chaseRange;
                chase.obstacleLayer = chaseSettings.obstacleLayer;
                break;

            case ChaseSettings.ChaseType.Enhanced:
                chase = enemy.AddComponent<EnhancedChase>();
                chase.chaseSpeed = chaseSettings.chaseSpeed;
                chase.chaseRange = chaseSettings.chaseRange;
                chase.obstacleLayer = chaseSettings.obstacleLayer;
                break;
        }
    }
}


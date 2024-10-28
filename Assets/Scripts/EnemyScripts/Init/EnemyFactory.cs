using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;    
    [SerializeField] private List<IComponentInitializer> initializers = new List<IComponentInitializer>();

    public GameObject CreateEnemy(IEnemySettings settings, Transform spawnPoint)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        List<Type> validInitializer = settings.GetInitializers();
        foreach (var initializer in initializers)
        {
            if (validInitializer.Contains(initializer.GetType()))   
            {
                initializer.Initialize(enemy.transform.GetChild(0).gameObject, settings);
            }
        }
        return enemy;
    }
}

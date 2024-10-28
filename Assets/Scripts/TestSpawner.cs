using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public EnemyFactory enemyFactory;
    public IEnemySettings settings;
    public Transform spawnPoint;

    private void Start()
    {
        enemyFactory.CreateEnemy(settings, spawnPoint);
    }
}

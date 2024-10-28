using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthInitializer", menuName = "Initializers/HealthInitializer", order = 4)]
public class HealthInitializer : IComponentInitializer
{
    public override void Initialize(GameObject enemy, IEnemySettings settings)
    {
        if (settings != null && settings.HealthSettings != null)
        {
            InitializeHealth(enemy, settings.HealthSettings);
        }
    }

    private void InitializeHealth(GameObject enemy, HealthSettings healthSettings)
    {
        var health = enemy.AddComponent<EnemyHealth>();
        health.floatingDamage = healthSettings.floatingDamagePrefab;
        health.MaxHealth = healthSettings.maxHealth;
        health.CurrentHealth = healthSettings.maxHealth;

        var animController = enemy.GetComponent<EnemyAnimController>();
        var state = enemy.GetComponent<EnemyState>();

        health.TakeDamageEvent.AddListener(animController.TriggerHit);
        health.TakeDamageEvent.AddListener(state.EnableHitChase);

        health.DieEvent.AddListener(animController.TriggerDie);
        health.DieEvent.AddListener(state.DisableIsAlive);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DropInitializer", menuName = "Initializers/DropInitializer", order = 5)]
public class DropInitializer : IComponentInitializer
{
    public override void Initialize(GameObject enemy, IEnemySettings settings)
    {
        if (settings != null && settings.DropSettings != null)
        {
            InitializeDrop(enemy, settings.DropSettings);
        }
    }

    private void InitializeDrop(GameObject enemy, DropSettings dropSettings)
    {
        var drop = enemy.AddComponent<EnemyDrop>();
        drop.dropTable = dropSettings.dropTable;
        drop.dropItemPrefab = dropSettings.dropItem;
    }
}

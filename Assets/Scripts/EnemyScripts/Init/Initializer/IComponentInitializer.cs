using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IComponentInitializer : ScriptableObject
{
    public abstract void Initialize(GameObject enemy, IEnemySettings settings);
}


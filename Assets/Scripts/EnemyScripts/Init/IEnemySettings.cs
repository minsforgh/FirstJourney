using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEnemySettings : ScriptableObject
{
    public abstract BasicSettings BasicSettings { get; }
    public abstract ChaseSettings ChaseSettings { get; }
    public abstract AttackSettings AttackSettings { get; }
    public abstract HealthSettings HealthSettings { get; }
    public abstract DropSettings DropSettings { get; }
    public abstract PatrolSettings PatrolSettings { get; }

    public abstract List<Type> GetInitializers();
}

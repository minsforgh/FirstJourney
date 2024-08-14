using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "Settings/EnemySettings", order = -1)]
public class EnemySettings : ScriptableObject
{
    public BasicSettings basicSettings;
    public ChaseSettings chaseSettings;
    public AttackSettings attackSettings;
    public HealthSettings healthSettings;
    public DropSettings dropSettings;
    public PatrolSettings patrolSettings;
}

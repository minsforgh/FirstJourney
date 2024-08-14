using UnityEngine;

[CreateAssetMenu(fileName = "ChaseSettings", menuName = "Settings/ChaseSettings")]
public class ChaseSettings : ScriptableObject
{
    public ChaseType chaseType;
    public float chaseSpeed;
    public float chaseRange;
    public LayerMask obstacleLayer;

    public enum ChaseType
    {
        Normal,
        Enhanced
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "EnemySettings")]
public class EnemySettings : ScriptableObject
{
    public RendererSettings rendererSettings;
    public RigidbodySettings rigidbodySettings;
    public ColliderSettings colliderSettings;
    public AnimatorSettings animatorSettings;

    public MovementSettings movementSettings;
    public AttackSettings attackSettings;
    public HealthSettings healthSettings;
    public DropSettings dropSettings;

    public PatrolSettings patrolSettings;
}

[System.Serializable]
public class RendererSettings
{
    public Sprite sprite;
    public string sortingLayerName;
}

[System.Serializable]
public class RigidbodySettings
{
    public RigidbodyType2D bodyType;
    public bool UseFullKinematic = true;
}

[System.Serializable]
public class ColliderSettings
{   
    public ColliderType colliderType;
    public Vector2 offset;
    public Vector2 size;
    public enum ColliderType
    {
        Box,
        Capsule
    }
}

[System.Serializable]
public class AnimatorSettings
{
    public AnimatorController controller;
}

[System.Serializable]
public class MovementSettings
{
    public float chaseSpeed;
    public float chaseRange;
    public float attackRange;
}

[System.Serializable]
public class AttackSettings
{   
    public LayerMask targetLayerMask;
    public AttackType attackType;
    public float beforeAttackDelay;
    public float afterAttackDelay;
    public int[] damages;
    public string[] attackTriggers;
    public float knockbackForce;

    public float meleeAttackRadius; // only for melee
    public GameObject projectilePrefab; // only for ranged;

    public enum AttackType
    {
        Melee,
        Ranged
    }
}

[System.Serializable]
public class HealthSettings
{   
    public float maxHealth;
    public GameObject floatingDamagePrefab;
}

[System.Serializable]
public class DropSettings
{
    public DropTable dropTable;
    public GameObject dropItem;
}

[System.Serializable]
public class PatrolSettings
{
    public bool doesPatrol;
    public GameObject patrolRoute;
    public float patrolSpeed;
}

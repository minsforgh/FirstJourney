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

    public ChaseSettings movementSettings;
    public AttackSettings attackSettings;
    public HealthSettings healthSettings;
    public DropSettings dropSettings;

    public PatrolSettings patrolSettings;
}

[System.Serializable]
public class RendererSettings
{
    public Sprite sprite;
    public bool flipX;
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
public class ChaseSettings
{
    public float chaseSpeed;
    public float chaseRange;
}

[System.Serializable]
public class AttackSettings
{   
    public List<AttackType> attackTypes; // 공격 타입 리스트
    public float beforeAttackDelay; // 공격 전 딜레이
    public float contactDamage;  // 접촉 시 데미지

    // for melee attack
    public float[] meleeCooldowns; // 근접 공격의 쿨타임
    public string[] meleeTriggers; // 애니메이션 트리거 배열
    public int[] meleeDamages; // 각 공격의 데미지 배열
    public float meleeEffectiveRange; // 근접 공격의 유효 사거리
    public float meleeAttackRadius; // 공격 범위  
    public float knockbackForce; // 플레이어를 넉백시킬 힘

    //for ranged attack
    public GameObject[] projectilePrefabs; // 발사할 투사체 프리팹 배열
    public string[] rangedTriggers; // 애니메이션 트리거 배열
    public float[] rangedCooldowns; // 근접 공격의 쿨타임
    public float rangedEffectiveRange; // 원거리 공격의 유효 사거리
    
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

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class EnemyCommonComponents : MonoBehaviour
{
    [Header("Enemy Settings")]
    public EnemySettings settings;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;

    private EnemyAnimController animController;
    private EnemyState state;
    private EnemyChase chase;
    private EnemyHealth health;
    private EnemyDrop drop;
    private Patrol patrol;

    void Awake()
    {
        InitializeBasicComponents();
        InitializeAnimController();
        InitializeState();
        InitializeChase();
        InitializeAttack();
        InitializeHealth();
        InitializeDrop();
        InitializePatrol();
    }

    void InitializeBasicComponents()
    {
        if (settings != null && settings.BasicSettings != null)
        {
            InitializeRenderer();
            InitializeRigidbody();
            InitializeCollider();
            InitializeAnimator();
        }
    }

    void InitializeRenderer()
    {
        if (settings.BasicSettings.RendererSettings != null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = settings.BasicSettings.rendererSettings.sprite;
            spriteRenderer.flipX = settings.BasicSettings.rendererSettings.flipX;
            spriteRenderer.sortingLayerName = settings.BasicSettings.rendererSettings.sortingLayerName;
        }
    }

    void InitializeRigidbody()
    {
        if (settings.BasicSettings.RigidbodySettings != null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = settings.BasicSettings.rigidbodySettings.bodyType;
            rb.freezeRotation = settings.BasicSettings.rigidbodySettings.freezeRotation;
            rb.mass = settings.BasicSettings.rigidbodySettings.mass;
            rb.drag = settings.BasicSettings.rigidbodySettings.linearDrag;
            rb.gravityScale = settings.BasicSettings.rigidbodySettings.gravityScale;
            rb.collisionDetectionMode = settings.BasicSettings.rigidbodySettings.collisionDetectionMode;
        }   
    }

    void InitializeCollider()
    {
        if (settings.BasicSettings.ColliderSettings != null)
        {
            switch (settings.BasicSettings.colliderSettings.colliderType)
            {
                case ColliderSettings.ColliderType.Box:
                    var boxCollider = gameObject.AddComponent<BoxCollider2D>();
                    boxCollider.offset = settings.BasicSettings.colliderSettings.offset;
                    boxCollider.size = settings.BasicSettings.colliderSettings.size;
                    break;

                case ColliderSettings.ColliderType.Capsule:
                    var capsuleCollider = gameObject.AddComponent<CapsuleCollider2D>();
                    capsuleCollider.offset = settings.BasicSettings.colliderSettings.offset;
                    capsuleCollider.size = settings.BasicSettings.colliderSettings.size;
                    break;
            }
        }
    }

    void InitializeAnimator()
    {
        if (settings.BasicSettings.AnimatorSettings != null)
        {
            animator = gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = settings.BasicSettings.animatorSettings.controller;
        }
    }

    void InitializeAnimController()
    {
        animController = gameObject.AddComponent<EnemyAnimController>();
    }

    void InitializeState()
    {
        state = gameObject.AddComponent<EnemyState>();
    }

    void InitializeChase()
    {
        if (settings != null && settings.ChaseSettings != null)
        {
            switch (settings.ChaseSettings.chaseType)
            {
                case ChaseSettings.ChaseType.Normal:
                    chase = gameObject.AddComponent<EnemyChase>();
                    chase.chaseSpeed = settings.ChaseSettings.chaseSpeed;
                    chase.chaseRange = settings.ChaseSettings.chaseRange;
                    chase.obstacleLayer = settings.ChaseSettings.obstacleLayer;
                    break;

                case ChaseSettings.ChaseType.Enhanced:
                    chase = gameObject.AddComponent<EnhancedChase>();
                    chase.chaseSpeed = settings.ChaseSettings.chaseSpeed;
                    chase.chaseRange = settings.ChaseSettings.chaseRange;
                    chase.obstacleLayer = settings.ChaseSettings.obstacleLayer;
                    break;
            }
        }
    }

    void InitializeAttack()
    {
        if (settings != null && settings.AttackSettings != null)
        {
            var enemyAttackManager = gameObject.AddComponent<EnemyAttackManager>();
            enemyAttackManager.beforeAttackDelay = settings.AttackSettings.beforeAttackDelay;
            enemyAttackManager.contactDamage = settings.AttackSettings.contactDamage;
            enemyAttackManager.contactKnockbackForce = settings.AttackSettings.contactKnockbackForce;

            if(settings.AttackSettings.MeleeSettings != null)
            {
                var meleeAtk = gameObject.AddComponent<EnemyMeleeAttack>();
                meleeAtk.cooldowns = settings.AttackSettings.MeleeSettings.meleeCooldowns;
                meleeAtk.attackTriggers = settings.AttackSettings.MeleeSettings.meleeTriggers;
                meleeAtk.damages = settings.AttackSettings.MeleeSettings.meleeDamages;
                meleeAtk.effectiveRange = settings.AttackSettings.MeleeSettings.meleeEffectiveRange;
                meleeAtk.attackRadius = settings.AttackSettings.MeleeSettings.meleeAttackRadius;
                meleeAtk.knockbackForce = settings.AttackSettings.MeleeSettings.knockbackForce;
                enemyAttackManager.attackTypes.Add(meleeAtk);
            }

            if(settings.AttackSettings.RangedSettings != null)
            {
                var rangedAtk = gameObject.AddComponent<EnemyAttackRanged>();
                rangedAtk.projectilePrefabs = settings.AttackSettings.RangedSettings.projectilePrefabs;
                rangedAtk.attackTriggers = settings.AttackSettings.RangedSettings.rangedTriggers;
                rangedAtk.cooldowns = settings.AttackSettings.RangedSettings.rangedCooldowns;
                rangedAtk.effectiveRange = settings.AttackSettings.RangedSettings.rangedEffectiveRange;
                enemyAttackManager.attackTypes.Add(rangedAtk);
            }
        }
    }

void InitializeHealth()
{
    if (settings != null && settings.HealthSettings != null)
    {
        health = gameObject.AddComponent<EnemyHealth>();
        health.floatingDamage = settings.HealthSettings.floatingDamagePrefab;
        health.MaxHealth = settings.HealthSettings.maxHealth;
        health.CurrentHealth = settings.HealthSettings.maxHealth;

        health.TakeDamageEvent.AddListener(animController.TriggerHit);
        health.TakeDamageEvent.AddListener(state.EnableHitChase);

        health.DieEvent.AddListener(animController.TriggerDie);
        health.DieEvent.AddListener(state.DisableIsAlive);
    }
}

void InitializeDrop()
{
    if (settings != null && settings.DropSettings != null)
    {
        drop = gameObject.AddComponent<EnemyDrop>();
        drop.dropTable = settings.DropSettings.dropTable;
        drop.dropItemPrefab = settings.DropSettings.dropItem;
    }
}

void InitializePatrol()
{
    if (settings != null && settings.PatrolSettings != null)
    {
        if (settings.PatrolSettings.doesPatrol)
        {
            patrol = transform.parent.AddComponent<Patrol>();
            patrol.DoesPatrol = settings.PatrolSettings.doesPatrol;
            patrol.patrolRoutePrefab = settings.PatrolSettings.patrolRoute;
            patrol.patrolSpeed = settings.PatrolSettings.patrolSpeed;
        }
    }
}
}

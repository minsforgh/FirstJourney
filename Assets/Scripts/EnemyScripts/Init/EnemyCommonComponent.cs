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
        if (settings != null && settings.basicSettings != null)
        {
            InitializeRenderer();
            InitializeRigidbody();
            InitializeCollider();
            InitializeAnimator();
        }
    }

    void InitializeRenderer()
    {
        if (settings.basicSettings.rendererSettings != null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = settings.basicSettings.rendererSettings.sprite;
            spriteRenderer.flipX = settings.basicSettings.rendererSettings.flipX;
            spriteRenderer.sortingLayerName = settings.basicSettings.rendererSettings.sortingLayerName;
        }
    }

    void InitializeRigidbody()
    {
        if (settings.basicSettings.rigidbodySettings != null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = settings.basicSettings.rigidbodySettings.bodyType;
            rb.useFullKinematicContacts = settings.basicSettings.rigidbodySettings.UseFullKinematiccontacts;
            rb.freezeRotation = settings.basicSettings.rigidbodySettings.freezeRotation;
            rb.drag = settings.basicSettings.rigidbodySettings.linearDrag;
            rb.gravityScale = settings.basicSettings.rigidbodySettings.gravityScale;
            rb.collisionDetectionMode = settings.basicSettings.rigidbodySettings.collisionDetectionMode;
        }   
    }

    void InitializeCollider()
    {
        if (settings.basicSettings.colliderSettings != null)
        {
            switch (settings.basicSettings.colliderSettings.colliderType)
            {
                case ColliderSettings.ColliderType.Box:
                    var boxCollider = gameObject.AddComponent<BoxCollider2D>();
                    boxCollider.offset = settings.basicSettings.colliderSettings.offset;
                    boxCollider.size = settings.basicSettings.colliderSettings.size;
                    break;

                case ColliderSettings.ColliderType.Capsule:
                    var capsuleCollider = gameObject.AddComponent<CapsuleCollider2D>();
                    capsuleCollider.offset = settings.basicSettings.colliderSettings.offset;
                    capsuleCollider.size = settings.basicSettings.colliderSettings.size;
                    break;
            }
        }
    }

    void InitializeAnimator()
    {
        if (settings.basicSettings.animatorSettings != null)
        {
            animator = gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = settings.basicSettings.animatorSettings.controller;
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
        if (settings != null && settings.chaseSettings != null)
        {
            switch (settings.chaseSettings.chaseType)
            {
                case ChaseSettings.ChaseType.Normal:
                    chase = gameObject.AddComponent<EnemyChase>();
                    chase.chaseSpeed = settings.chaseSettings.chaseSpeed;
                    chase.chaseRange = settings.chaseSettings.chaseRange;
                    chase.obstacleLayer = settings.chaseSettings.obstacleLayer;
                    break;

                case ChaseSettings.ChaseType.Enhanced:
                    chase = gameObject.AddComponent<EnhancedChase>();
                    chase.chaseSpeed = settings.chaseSettings.chaseSpeed;
                    chase.chaseRange = settings.chaseSettings.chaseRange;
                    chase.obstacleLayer = settings.chaseSettings.obstacleLayer;
                    break;
            }
        }
    }

    void InitializeAttack()
    {
        if (settings != null && settings.attackSettings != null)
        {
            var enemyAttackManager = gameObject.AddComponent<EnemyAttackManager>();
            enemyAttackManager.beforeAttackDelay = settings.attackSettings.beforeAttackDelay;
            enemyAttackManager.contactDamage = settings.attackSettings.contactDamage;
            enemyAttackManager.contactKnockbackForce = settings.attackSettings.contactKnockbackForce;

            if(settings.attackSettings.meleeSettings != null)
            {
                var meleeAtk = gameObject.AddComponent<EnemyMeleeAttack>();
                meleeAtk.cooldowns = settings.attackSettings.meleeSettings.meleeCooldowns;
                meleeAtk.attackTriggers = settings.attackSettings.meleeSettings.meleeTriggers;
                meleeAtk.damages = settings.attackSettings.meleeSettings.meleeDamages;
                meleeAtk.effectiveRange = settings.attackSettings.meleeSettings.meleeEffectiveRange;
                meleeAtk.attackRadius = settings.attackSettings.meleeSettings.meleeAttackRadius;
                meleeAtk.knockbackForce = settings.attackSettings.meleeSettings.knockbackForce;
                enemyAttackManager.attackTypes.Add(meleeAtk);
            }

            if(settings.attackSettings.rangedSettings != null)
            {
                var rangedAtk = gameObject.AddComponent<EnemyAttackRanged>();
                rangedAtk.projectilePrefabs = settings.attackSettings.rangedSettings.projectilePrefabs;
                rangedAtk.attackTriggers = settings.attackSettings.rangedSettings.rangedTriggers;
                rangedAtk.cooldowns = settings.attackSettings.rangedSettings.rangedCooldowns;
                rangedAtk.effectiveRange = settings.attackSettings.rangedSettings.rangedEffectiveRange;
                enemyAttackManager.attackTypes.Add(rangedAtk);
            }
        }
    }

void InitializeHealth()
{
    if (settings != null && settings.healthSettings != null)
    {
        health = gameObject.AddComponent<EnemyHealth>();
        health.floatingDamage = settings.healthSettings.floatingDamagePrefab;
        health.MaxHealth = settings.healthSettings.maxHealth;
        health.CurrentHealth = settings.healthSettings.maxHealth;

        health.TakeDamageEvent.AddListener(animController.TriggerHit);
        health.TakeDamageEvent.AddListener(state.EnableHitChase);

        health.DieEvent.AddListener(animController.TriggerDie);
        health.DieEvent.AddListener(state.DisableIsAlive);
    }
}

void InitializeDrop()
{
    if (settings != null && settings.dropSettings != null)
    {
        drop = gameObject.AddComponent<EnemyDrop>();
        drop.dropTable = settings.dropSettings.dropTable;
        drop.dropItemPrefab = settings.dropSettings.dropItem;
    }
}

void InitializePatrol()
{
    if (settings != null && settings.patrolSettings != null)
    {
        if (settings.patrolSettings.doesPatrol)
        {
            patrol = transform.parent.AddComponent<Patrol>();
            patrol.DoesPatrol = settings.patrolSettings.doesPatrol;
            patrol.patrolRoutePrefab = settings.patrolSettings.patrolRoute;
            patrol.patrolSpeed = settings.patrolSettings.patrolSpeed;
        }
    }
}
}

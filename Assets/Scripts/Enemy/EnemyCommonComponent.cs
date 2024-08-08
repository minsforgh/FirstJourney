using Unity.VisualScripting;
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
    private EnemyAttackBase attack;
    private EnemyHealth health;
    private EnemyDrop drop;
    private Patrol patrol;

    void Awake()
    {
        InitializeRenderer();
        InitializeRigidbody();
        InitializeCollider();
        InitializeAnimator();

        InitializeAnimController();
        InitializeState();
        InitializeMovement();
        InitializeAttack();
        InitializeHealth();
        InitializeDrop();

        InitializePatrol();
    }

    void InitializeRenderer()
    {
        if (settings != null && settings.rendererSettings != null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = settings.rendererSettings.sprite;
            spriteRenderer.flipX = settings.rendererSettings.flipX;
            spriteRenderer.sortingLayerName = settings.rendererSettings.sortingLayerName;
        }
    }

    void InitializeRigidbody()
    {
        if (settings != null && settings.rigidbodySettings != null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = settings.rigidbodySettings.bodyType;
            rb.useFullKinematicContacts = settings.rigidbodySettings.UseFullKinematic;
        }
    }

    void InitializeCollider()
    {
        if (settings != null && settings.colliderSettings != null)
        {
            switch (settings.colliderSettings.colliderType)
            {
                case ColliderSettings.ColliderType.Box:
                    var boxCollider = gameObject.AddComponent<BoxCollider2D>();
                    boxCollider.offset = settings.colliderSettings.offset;
                    boxCollider.size = settings.colliderSettings.size;
                    break;

                case ColliderSettings.ColliderType.Capsule:
                    var capsuleCollider = gameObject.AddComponent<CapsuleCollider2D>();
                    capsuleCollider.offset = settings.colliderSettings.offset;
                    capsuleCollider.size = settings.colliderSettings.size;
                    break;
            }
        }
    }

    void InitializeAnimator()
    {
        if (settings != null && settings.animatorSettings != null)
        {
            animator = gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = settings.animatorSettings.controller;
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

    void InitializeMovement()
    {
        if (settings != null && settings.movementSettings != null)
        {
            chase = gameObject.AddComponent<EnemyChase>();
            chase.chaseSpeed = settings.movementSettings.chaseSpeed;
            chase.chaseRange = settings.movementSettings.chaseRange;
        }
    }

    void InitializeAttack()
    {
        if (settings != null && settings.attackSettings != null)
        {   
            var enemyAttackManager = gameObject.AddComponent<EnemyAttackManager>();
            enemyAttackManager.beforeAttackDelay = settings.attackSettings.beforeAttackDelay;
            enemyAttackManager.contactDamage = settings.attackSettings.contactDamage;
            
            for(var i = 0; i < settings.attackSettings.attackTypes.Count; i++)
            {
                var attackType = settings.attackSettings.attackTypes[i];
                switch(attackType)
                {
                    case AttackSettings.AttackType.Melee:
                        var meleeAtk = gameObject.AddComponent<EnemyMeleeAttack>();
                        meleeAtk.cooldowns = settings.attackSettings.meleeCooldowns;
                        meleeAtk.attackTriggers = settings.attackSettings.meleeTriggers;
                        meleeAtk.damages = settings.attackSettings.meleeDamages;
                        meleeAtk.effectiveRange = settings.attackSettings.meleeEffectiveRange;
                        meleeAtk.attackRadius = settings.attackSettings.meleeAttackRadius;
                        meleeAtk.knockbackForce = settings.attackSettings.knockbackForce;
                        enemyAttackManager.attackTypes.Add(meleeAtk);
                        break;

                    case AttackSettings.AttackType.Ranged:
                        var rangedAtk = gameObject.AddComponent<EnemyAttackRanged>();
                        rangedAtk.projectilePrefabs = settings.attackSettings.projectilePrefabs;
                        rangedAtk.attackTriggers = settings.attackSettings.rangedTriggers;
                        rangedAtk.cooldowns = settings.attackSettings.rangedCooldowns;
                        rangedAtk.effectiveRange = settings.attackSettings.rangedEffectiveRange;
                        enemyAttackManager.attackTypes.Add(rangedAtk);
                        break;
                }
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

            health.TakeDamageEvent.AddListener(animController.TriggerHit);
            health.TakeDamageEvent.AddListener(state.EnableHitChase);

            health.DieEvent.AddListener(animController.TriggerDie);
            health.DieEvent.AddListener(state.DisableIsAlive);
        }
    }

    void InitializeDrop()
    {
        drop = gameObject.AddComponent<EnemyDrop>();
        drop.dropTable = settings.dropSettings.dropTable;
        drop.dropItemPrefab = settings.dropSettings.dropItem;
    }

    void InitializePatrol()
    {
        patrol = transform.parent.AddComponent<Patrol>();
        if (settings.patrolSettings.doesPatrol)
        {
            patrol.DoesPatrol = settings.patrolSettings.doesPatrol;
            patrol.patrolRoutePrefab = settings.patrolSettings.patrolRoute;
            patrol.patrolSpeed = settings.patrolSettings.patrolSpeed;
        }
    }
}

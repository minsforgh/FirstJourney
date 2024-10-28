using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicInitializer", menuName = "Initializers/BasicInitializer", order = 1)]
public class BasicInitializer : IComponentInitializer
{
    public override void Initialize(GameObject enemy, IEnemySettings settings)
    {
        if (settings != null && settings.BasicSettings != null)
        {
            InitializeRenderer(enemy, settings.BasicSettings.RendererSettings);
            InitializeRigidbody(enemy, settings.BasicSettings.RigidbodySettings);
            InitializeCollider(enemy, settings.BasicSettings.ColliderSettings);
            InitializeAnimator(enemy, settings.BasicSettings.AnimatorSettings);
        }
    }

    void InitializeRenderer(GameObject enemy, RendererSettings rendererSettings)
    {
        if (rendererSettings != null)
        {
            var spriteRenderer = enemy.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = rendererSettings.sprite;
            spriteRenderer.flipX = rendererSettings.flipX;
            spriteRenderer.sortingLayerName = rendererSettings.sortingLayerName;
        }
    }

    void InitializeRigidbody(GameObject enemy, RigidbodySettings rigidbodySettings)
    {
        if (rigidbodySettings != null)
        {
            var rb = enemy.GetComponent<Rigidbody2D>();
            rb.bodyType = rigidbodySettings.bodyType;
            rb.freezeRotation = rigidbodySettings.freezeRotation;
            rb.mass = rigidbodySettings.mass;
            rb.drag = rigidbodySettings.linearDrag;
            rb.gravityScale = rigidbodySettings.gravityScale;
            rb.collisionDetectionMode = rigidbodySettings.collisionDetectionMode;
        }
    }

    void InitializeCollider(GameObject enemy, ColliderSettings colliderSettings)
    {
        if (colliderSettings != null)
        {
            switch (colliderSettings.colliderType)
            {
                case ColliderSettings.ColliderType.Box:
                    var boxCollider = enemy.AddComponent<BoxCollider2D>();
                    boxCollider.offset = colliderSettings.offset;
                    boxCollider.size = colliderSettings.size;
                    break;

                case ColliderSettings.ColliderType.Capsule:
                    var capsuleCollider = enemy.AddComponent<CapsuleCollider2D>();
                    capsuleCollider.offset = colliderSettings.offset;
                    capsuleCollider.size = colliderSettings.size;
                    break;
            }
        }
    }

    void InitializeAnimator(GameObject enemy, AnimatorSettings animatorSettings)
    {
        if (animatorSettings != null)
        {
            var animator = enemy.GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorSettings.controller;
        }
    }
}

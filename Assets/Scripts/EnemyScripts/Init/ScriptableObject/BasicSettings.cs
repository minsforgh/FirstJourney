using UnityEngine;

[CreateAssetMenu(fileName = "BasicSettings", menuName = "Settings/BasicSettings")]
public class BasicSettings : ScriptableObject
{
    [Header("Renderer Settings")]
    public RendererSettings rendererSettings;

    [Header("Rigidbody Settings")]
    public RigidbodySettings rigidbodySettings;

    [Header("Collider Settings")]
    public ColliderSettings colliderSettings;

    [Header("Animator Settings")]
    public AnimatorSettings animatorSettings;
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
    public RigidbodyType2D bodyType = RigidbodyType2D.Dynamic;
    public bool UseFullKinematiccontacts = true;
    public bool freezeRotation = true;
    public float mass = 10f;
    public float linearDrag = 10f;
    public float gravityScale = 0f;
    public CollisionDetectionMode2D collisionDetectionMode = CollisionDetectionMode2D.Continuous;
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
    public RuntimeAnimatorController controller;
}

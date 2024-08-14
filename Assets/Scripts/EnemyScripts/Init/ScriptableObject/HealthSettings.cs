using UnityEngine;

[CreateAssetMenu(fileName = "HealthSettings", menuName = "Settings/HealthSettings")]
public class HealthSettings : ScriptableObject
{
    public float maxHealth;
    public GameObject floatingDamagePrefab;
}

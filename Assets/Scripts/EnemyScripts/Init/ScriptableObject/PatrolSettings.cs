using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatrolSettings", menuName = "Settings/PatrolSettings")]
public class PatrolSettings : ScriptableObject
{
    public bool doesPatrol;
    public GameObject patrolRoute;
    public float patrolSpeed;
}


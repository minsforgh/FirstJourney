using System;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    [SerializeField] private Transform body;
    [SerializeField] private Transform patrolRoute;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private EnemyAnimController enemyAnimController;
    [SerializeField] private EnemyState enemyState;

    int current;
    List<Transform> pointList;

    private void Start()
    {
        current = 0;

        pointList = new List<Transform>();
        if (patrolRoute != null)
        {
            foreach (Transform child in patrolRoute)
            {
                pointList.Add(child);
            }
        }
    }

    private void Update()
    {
        if (enemyState.DoPatrol)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (body.position != pointList[current].position)
        {
            enemyAnimController.SetIsmoving(true);
            body.position = Vector2.MoveTowards(body.position, pointList[current].position, patrolSpeed * Time.deltaTime);
        }
        else
        {
            enemyAnimController.SetIsmoving(false);
            current = (current + 1) % pointList.Count;
        }

        enemyAnimController.FlipSprite((pointList[current].position - body.position).normalized);

    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public bool DoesPatrol;
    public GameObject patrolRoutePrefab;
    public float patrolSpeed;

    private Transform body;
    private EnemyAnimController enemyAnimController;
    private EnemyState enemyState;

    private int current;
    private List<Transform> pointList;
    private GameObject patrolRoute;

    private float tolerance = 0.1f;

    private void Awake()
    {
        body = transform.GetChild(0);
        enemyAnimController = body.GetComponent<EnemyAnimController>();
        enemyState = body.GetComponent<EnemyState>();
    }

    private void Start()
    {
        patrolRoute = Instantiate(patrolRoutePrefab, transform);
        current = 0;

        pointList = new List<Transform>();
        if (patrolRoute != null)
        {
            foreach (Transform child in patrolRoute.transform)
            {
                pointList.Add(child);
            }
        }
    }

    private void Update()
    {
        if (enemyState.IsPatrolling && DoesPatrol)
        {
            ContinuePatrol();
        }
    }

    private void ContinuePatrol()
    {
        if (Vector2.Distance(body.position, pointList[current].position) > tolerance)
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

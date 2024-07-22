using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    [SerializeField] Transform body;
    [SerializeField] Transform patrolRoute;
    [SerializeField] float patrolSpeed;
    public Animator myAnimator;
    bool isPatrolling;
    
    int current;
    List<Transform> pointList;

    void Start()
    {
        current = 0;
        
        pointList = new List<Transform>();
        foreach (Transform child in patrolRoute)
        {
            pointList.Add(child);
        }

        isPatrolling = true;
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (isPatrolling)
        {
            if (body.position != pointList[current].position)
            {
                myAnimator.SetBool("isMoving", true);
                body.position = Vector2.MoveTowards(body.position, pointList[current].position, patrolSpeed * Time.deltaTime);
            }
            else
            {
                myAnimator.SetBool("isMoving", false);
                current = (current + 1) % pointList.Count;
            }

            FlipSprite();
        }
    }

    void FlipSprite()
    {
        float horizontalDirection = pointList[current].position.x - body.position.x;
        bool enemyHasHorizontalSpeed = Mathf.Abs(horizontalDirection) > Mathf.Epsilon;
        if (enemyHasHorizontalSpeed)
        {
            body.localScale = new Vector2(Math.Sign(horizontalDirection), 1);
        }
    }

    public void StopPatrol()
    {
        isPatrolling = false;
    }

    public void ContinuePatrol()
    {
        isPatrolling = true;
    }
}

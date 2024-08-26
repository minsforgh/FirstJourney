using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    private Vector2 targetPosition;
    private float moveSpeed;

    public void Initialize(Vector2 targetPosition, float moveSpeed)
    {
        this.targetPosition = targetPosition;
        this.moveSpeed = moveSpeed;
    }

    private void Update()
    {
        // 스테이지 중앙을 향해 이동
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // 일정 거리 이하로 접근하면 파괴하거나 다른 행동
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject); // 예시로 FireWall이 목표 위치에 도달하면 파괴
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<HealthInterface>().TakeDamage(100);
            Destroy(gameObject);
        }
        else if(collider.CompareTag("Pilar"))
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }
}

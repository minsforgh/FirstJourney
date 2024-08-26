using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [Header("Chase Spec")]
    public float chaseSpeed;
    public float chaseRange;
    public LayerMask obstacleLayer;  // 장애물 레이어

    protected Transform playerTransform;
    protected EnemyAnimController enemyAnimController;
    protected EnemyState enemyState;
    protected Rigidbody2D rigidBody;



    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        enemyAnimController = GetComponent<EnemyAnimController>();
        enemyState = GetComponent<EnemyState>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        if (playerTransform != null && enemyState.IsAlive)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;

            if (distanceToPlayer <= chaseRange)
            {
                float boxWidth = 0.4f;  // 두께 (X)
                float boxHeight = 1f;  // 높이 (Y)

                // box는 0.5f 떨어진 거리에 (Enemy와 장애물 사이의 여유 공간)
                RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(boxWidth, boxHeight), 0f, directionToPlayer, 0.6f, obstacleLayer);

                if (hit.collider == null)
                {
                    enemyState.DisableHitChase();
                    enemyState.SetIsPatrolling(false);
                    Chase();
                }
                else
                {
                    AvoidObstacle(directionToPlayer);
                }

            }
        }
    }

    protected void Chase()
    {   
        // 공격 후의 Delay를 그대로 이용하여, 공격 직후 바로 추격하지 못하게 구현
        if (enemyState.CanAttack)
        {
            enemyAnimController.SetIsmoving(true);
            Vector2 direction = ((Vector2)playerTransform.position - (Vector2)transform.position).normalized;
            rigidBody.MovePosition(rigidBody.position + direction * chaseSpeed * Time.deltaTime);
            enemyAnimController.FlipSprite(direction);
        }
        else
        {
            enemyAnimController.SetIsmoving(false);
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    protected void AvoidObstacle(Vector2 direction)
    {
        // 장애물 회피, Player를 향한 방향의 수직 방향으로 이동
        enemyAnimController.FlipSprite(direction);
        Vector3 avoidanceDirection = Vector2.Perpendicular(direction).normalized;
        transform.position += avoidanceDirection * (0.75f * chaseSpeed) * Time.deltaTime;
    }

}
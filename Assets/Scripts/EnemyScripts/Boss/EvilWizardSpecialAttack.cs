using System.Collections;
using UnityEngine;

public class EvilWizardSpecialAttack : BossSpecialAttack
{
    private bool hasPassedHalfHealth = false;
    private bool hasPassedFivePercentHealth = false;

    public GameObject fireWallPrefab; // FireWall 프리팹
    public float spawnRadius = 10f; // 스테이지 중심으로부터의 거리
    public float moveSpeed = 2f; // FireWall의 이동 속도
    public int numberOfFireWalls = 8; // 스폰할 FireWall 개수
    public float waitTime;
    float angleStep;
    float angle = 0f;

    private void Update()
    {
        if (health.CurrentHealth <= health.MaxHealth / 2 && !hasPassedHalfHealth)
        {
            // 체력이 절반 이하가 되는 순간
            hasPassedHalfHealth = true;
            Debug.Log("체력이 절반 이하입니다!");
            StartCoroutine(SpecialAttack());
        }

        if (health.CurrentHealth <= health.MaxHealth / 20 && !hasPassedFivePercentHealth)   
        {
            // 체력이 5% 이하가 되는 순간
            hasPassedFivePercentHealth = true;
            Debug.Log("체력이 5% 이하입니다!");
            StartCoroutine(SpecialAttack());
        }
    }

    public override IEnumerator SpecialAttack()
    {
        enemyState.SetIsSpecialAttack(true);
        enemyState.SetCanAttack(false);
        enemyState.SetIsInvincible(true);
        FreezePosition();

        transform.position = bossManager.transform.position;

        enemyAnimController.SetSpecialAttack(true);

        yield return StartCoroutine(MoveFireWall());

        enemyAnimController.SetSpecialAttack(false);
        
        enemyState.SetIsInvincible(false);
        enemyState.SetIsSpecialAttack(false);
        enemyState.SetCanAttack(true);
        UnfreezePosition();
    }

    public IEnumerator MoveFireWall()   
    {
        angleStep = 360f / (float)numberOfFireWalls;  // 각 방벽 사이의 각도

        for (int i = 0; i < numberOfFireWalls; i++)
        {
            SpawnFireWall();    
        }

        yield return new WaitForSeconds(waitTime);
    }

    public void SpawnFireWall()
    {
        float spawnX = bossManager.transform.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * spawnRadius;
        float spawnY = bossManager.transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * spawnRadius;
        Vector2 spawnPosition = new Vector2(spawnX, spawnY);

        float rotationAngle = angle + 90f;
        // 방벽 생성 및 회전 적용
        GameObject fireWall = Instantiate(fireWallPrefab, spawnPosition, Quaternion.Euler(0, 0, rotationAngle));

        // FireWall을 스테이지 중앙을 향해 이동
        fireWall.GetComponent<FlameWall>().Initialize(bossManager.transform.position, moveSpeed);

        angle += angleStep;

        if (angle > 360f)
        {
            angle -= 360f;
        }
    }

    private void FreezePosition()
    {
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    private void UnfreezePosition()
    {
        rb.constraints = RigidbodyConstraints2D.None;
    }
}
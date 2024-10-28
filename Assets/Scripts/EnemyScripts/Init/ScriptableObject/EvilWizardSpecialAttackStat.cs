using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "EvilWizardSpecialAttackStat", menuName = "Settings/Stats/EvilWizardSpecialAttackStat", order = 1)]
public class EvilWizardSpecialAttackStat : BossSpecialAttackStat
{
    public GameObject fireWallPrefab; // FireWall 프리팹
    public float spawnRadius = 10f; // 스테이지 중심으로부터의 거리
    public float moveSpeed = 2f; // FireWall의 이동 속도
    public int numberOfFireWalls = 8; // 스폰할 FireWall 개수
    public float waitTime;

    public override void InitSpecialAttack(BossSpecialAttack bossSpecialAttack)
    {
        EvilWizardSpecialAttack evilWizardSpecialAttack = bossSpecialAttack as EvilWizardSpecialAttack;
        evilWizardSpecialAttack.fireWallPrefab = fireWallPrefab;
        evilWizardSpecialAttack.spawnRadius = spawnRadius;  
        evilWizardSpecialAttack.moveSpeed = moveSpeed;
        evilWizardSpecialAttack.numberOfFireWalls = numberOfFireWalls;
        evilWizardSpecialAttack.waitTime = waitTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossInstance;

    public BossCircle bossCircle; // 보스 소환진 - interactable
    public GameObject bossInfoUIPrefab;
    public BossUI bossUI;
    public GameObject stageBlockPrefab;
    public GameObject stageBlockInstance;
    public Transform stageEntrance;
    
    [TextArea]
    public string bossName;

    private HealthInterface bossHealth;

    void Update()
    {
        if (bossUI != null)
        {
            bossUI.UpdateBossHp(bossHealth.CurrentHealth);
        }
    }
    
    public IEnumerator SpawnBoss()
    {   
        yield return new WaitForSeconds(0.5f);
        bossInstance = Instantiate(boss, bossCircle.transform.position + new Vector3(0, 5, 0), Quaternion.identity);  
        bossHealth = bossInstance.GetComponentInChildren<HealthInterface>();
        bossUI = Instantiate(bossInfoUIPrefab).GetComponentInChildren<BossUI>();
        SetBossInfoUI();
    }

     public void BossDefeated()
    {
        Destroy(bossInstance);
        Destroy(bossUI.gameObject);
        Destroy(stageBlockInstance);
    }

    public void SetBossInfoUI()
    {
        bossUI.SetBossName(bossName);
        bossUI.InitBossHp(bossHealth.MaxHealth);
        bossUI.UpdateBossHp(bossHealth.CurrentHealth);
    }

    public void SetStageBlock()
    {
        stageBlockInstance = Instantiate(stageBlockPrefab, stageEntrance.position, Quaternion.identity);
    }


}

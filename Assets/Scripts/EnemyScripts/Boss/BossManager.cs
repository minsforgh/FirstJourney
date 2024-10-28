using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;
    public GameObject bossInstance;

    [SerializeField] private BossCircle bossCircle; // 보스 소환진 - interactable
    [SerializeField] private GameObject bossInfoUIPrefab;
    public BossUI bossUI;
    [SerializeField] private AudioClip bossBGM;

    [SerializeField] private GameObject stageBlockPrefab;
    public GameObject stageBlockInstance;
    [SerializeField] private Transform stageEntrance;

    [SerializeField] private Portal portal;

    [TextArea]
    public string bossName;

    private HealthInterface bossHealth;

    public EnemyFactory enemyFactory;
    public IEnemySettings bossSettings;

    void Update()
    {
        if (bossUI != null)
        {
            bossUI.UpdateBossHp(bossHealth.CurrentHealth);
        }
        if(bossHealth != null && bossHealth.CurrentHealth <= 0 && bossInstance != null)
        {
            BossDefeated();
        }
    }
    
    public IEnumerator SpawnBoss()
    {   
        AudioManager.Instance.PlayBackgroundMusicByClip(bossBGM);
        yield return new WaitForSeconds(0.5f);
        // bossInstance = Instantiate(bossPrefab, bossCircle.transform.position + new Vector3(0, 5, 0), Quaternion.identity);
        bossInstance = enemyFactory.CreateEnemy(bossSettings, bossCircle.transform);
        bossHealth = bossInstance.GetComponentInChildren<HealthInterface>();
        bossUI = Instantiate(bossInfoUIPrefab).GetComponentInChildren<BossUI>();
        
        bossInstance.GetComponentInChildren<BossSpecialAttack>().bossManager = this;
        SetBossInfoUI();
    }

     public void BossDefeated()
    {   
        AudioManager.Instance.PlayBackgroundMusic(AudioClipType.InGame01);
        Destroy(bossInstance);
        Destroy(bossUI.gameObject);
        Destroy(stageBlockInstance);
        EnablePortal();
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

    private void EnablePortal()
    {
        portal.gameObject.SetActive(true);
    }


}

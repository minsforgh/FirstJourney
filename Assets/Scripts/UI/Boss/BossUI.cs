using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bossName;
    [SerializeField] private Slider hpBar;
    [SerializeField] private TextMeshProUGUI hpBarText;
    
    public void SetBossNmae(string name)
    {
        bossName.text = name;
    }

    public void UpdateBossHp(float currentHp)
    {
        hpBar.value = currentHp;
        hpBarText.text = $"{currentHp}/{hpBar.maxValue}";
    }

    public void InitBossHp(float maxHp)
    {   
        hpBar.maxValue = maxHp;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance { get; private set; }
    [SerializeField] PlayerHealth player;
    public TextMeshProUGUI currentMoneyText;

    Slider hpBar;
    TextMeshProUGUI hpText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {   
        player = FindAnyObjectByType<PlayerHealth>();
        hpBar = GetComponentInChildren<Slider>();
        hpText = GetComponentInChildren<TextMeshProUGUI>();
        hpBar.value = hpBar.maxValue;
        hpText.text = player.CurrentHealth.ToString() + " / " + player.MaxHealth.ToString();

        UpdateCurrentMoney(0);
    }

    public void UpdatePlayerHp()
    {
        hpBar.value = player.CurrentHealth / player.MaxHealth;
        hpText.text = player.CurrentHealth.ToString() + " / " + player.MaxHealth.ToString();
    }

    public void UpdateCurrentMoney(int money)
    {
        currentMoneyText.text = money.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance { get; private set; }

    private PlayerController playerController;
    private PlayerHealth playerHealth;

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
        Init();
        UpdatePlayerHp();
        UpdateCurrentMoney(0);
    }

    private void Init()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        playerHealth = playerController.GetPlayerHealth();

        hpBar = GetComponentInChildren<Slider>();
        hpText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdatePlayerHp()
    {
        hpBar.value = playerHealth.CurrentHealth / playerHealth.MaxHealth;
        hpText.text = playerHealth.CurrentHealth.ToString() + " / " + playerHealth.MaxHealth.ToString();
    }

    public void UpdateCurrentMoney(int money)
    {
        currentMoneyText.text = money.ToString();
    }
}

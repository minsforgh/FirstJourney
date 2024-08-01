using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class TradeSystem : MonoBehaviour
{   
    [SerializeField] private Button returnButton;
    public MerchantData merchant;
    public TextMeshProUGUI moneyText;

    private void OnEnable()
    {
        returnButton.onClick.AddListener(HideTradeSystem);
    }

    private void OnDisable()
    {
        returnButton.onClick.RemoveListener(HideTradeSystem);
    }

    public void HideTradeSystem()
    {   
        Destroy(gameObject);
    }

    public void UpdateMoneyText()
    {
        moneyText.text = merchant.currentMoney.ToString();
    }

    public void SetMerchant(MerchantData merchantData)
    {
        merchant = merchantData;
    }
}

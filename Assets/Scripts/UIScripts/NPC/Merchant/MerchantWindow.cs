using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MerchantWindow : NPCWindow
{
    [SerializeField] private Button buyButton;
    [SerializeField] private Button sellButton;

    [SerializeField] private GameObject buyWindowUIPrefab;
    [SerializeField] private GameObject sellWindowUIPrefab;

    public GameObject UIInstance;

    private void Start()
    {
        BasicSet();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        buyButton.onClick.AddListener(() => ShowUI(buyWindowUIPrefab, typeof(BuySystem)));
        sellButton.onClick.AddListener(() => ShowUI(sellWindowUIPrefab, typeof(SellSystem)));
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        buyButton.onClick.RemoveListener(() => ShowUI(buyWindowUIPrefab, typeof(BuySystem)));
        sellButton.onClick.RemoveListener(() => ShowUI(sellWindowUIPrefab, typeof(SellSystem)));
    }


    private void ShowUI(GameObject prefab, System.Type systemType)
    {
        AudioManager.Instance.PlayAudio(AudioClipType.Confirm);
        UIInstance = UIManager.Instance.CreateUI(prefab);
        var window = UIInstance.GetComponentInChildren(systemType) as TradeSystem;
        window?.SetMerchant(nPCData as MerchantData);
    }
}

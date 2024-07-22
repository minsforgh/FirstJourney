using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantWindow : NPCWindow
{
    [SerializeField] private Button buyButton;
    [SerializeField] private Button sellButton;

    [SerializeField] private GameObject buyWindowUIPrefab;
    public GameObject UIInstance;

    [SerializeField] private GameObject sellWindowUIPrefab;

    private void Start()
    {   
        BasicSet();
        buyButton.onClick.AddListener(ShowBuyUI);
        sellButton.onClick.AddListener(ShowSellUI);
    }

    private void ShowBuyUI()
    {
        UIInstance = Instantiate(buyWindowUIPrefab);
    }
    
    private void ShowSellUI()
    {
        UIInstance = Instantiate(sellWindowUIPrefab);
    }

    public void HideInteractionUI()
    {
        Destroy(UIInstance);
    }

}

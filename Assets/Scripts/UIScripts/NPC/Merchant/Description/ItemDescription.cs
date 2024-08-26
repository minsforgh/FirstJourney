using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    [SerializeField] private GameObject itemInfoUIPrefab;

    private GameObject infoUIInstance;

    public void ShowUI(ItemData item)
    {
        if (infoUIInstance != null)
        {
            infoUIInstance.SetActive(true);
        }
        else
        {
            infoUIInstance = Instantiate(itemInfoUIPrefab, transform);
        }

        infoUIInstance.GetComponent<ItemInfoUI>().SetInfo(item);
    }

}


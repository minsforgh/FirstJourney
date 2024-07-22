using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    public enum ItemType
    {
        Weapon,
        Usable
    }

    [SerializeField] private GameObject usableItemInfoUI;
    [SerializeField] private GameObject weaponItemInfoUI;
    private Dictionary<ItemType, GameObject> uiInstances = new Dictionary<ItemType, GameObject>();

    private ItemType currentItemType;

    public void ShowUI(ItemData item)
    {
        foreach (var ui in uiInstances.Values)
        {
            ui.SetActive(false);
        }

        if (item is WeaponData)
        {
            currentItemType = ItemType.Weapon;
        }
        else if (item is UsableItemData)
        {   
            currentItemType = ItemType.Usable;
        }

        if (uiInstances.ContainsKey(currentItemType)) 
        {
            uiInstances[currentItemType].SetActive(true);
        }
        else
        {
            GameObject newUI = null;

            switch (currentItemType)
            {
                case ItemType.Weapon:
                    newUI = Instantiate(weaponItemInfoUI, transform);
                    break;
                case ItemType.Usable:
                    newUI = Instantiate(usableItemInfoUI, transform);
                    break;
            }

            if (newUI != null)
            {
                uiInstances[currentItemType] = newUI;
                newUI.SetActive(true);
            }
        }
        ItemInfoBase itemInfoUI = uiInstances[currentItemType].GetComponent<ItemInfoBase>();
        itemInfoUI.SetInfo(item);
    }

}


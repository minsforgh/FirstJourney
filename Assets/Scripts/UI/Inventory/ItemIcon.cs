using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    public ItemData Item => itemData;
    private ItemData itemData;
    [SerializeField] private Image image;
    [SerializeField] private CanvasGroup canvasGroup;

    public void SetItem(ItemData item)
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        itemData = item;
        image.sprite = item.Icon;
    }

    public void ClearIcon()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        itemData = null;
        image.sprite = null;
    }
}

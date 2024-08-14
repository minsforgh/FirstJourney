using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToSellItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image image;
    public ItemData item;
    [SerializeField] private ItemDescription itemDescription;
    [SerializeField] private SellSystem sellSystem;

    private const float doubleClickTime = 0.3f;
    private float lastClickTime;
    private int clickCount;

    public void SetItem(ItemData itemData)
    {   
        item = itemData;
        image.sprite = item.Icon;

        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = false;
    }

    public void RemoveItem()
    {
        image.sprite = null;
        item = null;

        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clickCount++;
        Debug.Log(clickCount);
        if (Time.time - lastClickTime < doubleClickTime)
        {
            if (clickCount == 2)
            {
                OnDoubleClick();
                clickCount = 0;
            }
        }
        else
        {   
            StartCoroutine(SingleClickDetection());
        }

        lastClickTime = Time.time;
    }
    
    private IEnumerator SingleClickDetection()
    {
        yield return new WaitForSeconds(doubleClickTime);   

        if (clickCount == 1)
        {
            OnSingleClick();
        }

        clickCount = 0;
    }

    private void OnSingleClick()
    {
        Debug.Log("Single Click Detected");
        itemDescription.ShowUI(item);
    }

    private void OnDoubleClick()
    {   
        Debug.Log("Double Click");
        if(sellSystem.SellItem(item))
        {
            Destroy(gameObject);
        }
    }


}

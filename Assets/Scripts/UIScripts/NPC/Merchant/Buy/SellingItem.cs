using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SellingItem : MonoBehaviour, IPointerClickHandler
{   
    public ItemData Item => item;
    [SerializeField] private ItemData item;
    public Image itemIcon;
    public TextMeshProUGUI valueText;
    private ItemDescription itemDescription;
    private BuySystem buySystem;

    private const float doubleClickTime = 0.3f;
    private float lastClickTime;
    private int clickCount;

    void Start()
    {
        //itemDescription = FindObjectOfType<ItemDescription>();
        itemDescription = transform.parent.parent.GetChild(1).GetComponent<ItemDescription>();
    }

    public void SetItem(ItemData itemData)
    {
        item = itemData;
        itemIcon.sprite = item.Icon;
        valueText.text = ' ' + item.Value.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        clickCount++;

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
            // 첫 클릭 이후로 doubleClickTime 간은 다음 클릭을 대비해야함
            StartCoroutine(SingleClickDetection());
        }

        lastClickTime = Time.time;
    }

    // 코루틴 이용, doubleClickTime 동안 Wait
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
        if(buySystem.BuyItem(item))
        {
            Destroy(gameObject);
        }
    }

    public void SetBuySystem(BuySystem buy)
    {
        buySystem = buy;
    }
}   

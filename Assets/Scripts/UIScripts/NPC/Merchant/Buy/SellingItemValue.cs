using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellingItemValue : MonoBehaviour, IPointerClickHandler
{
    private SellingItem sellingItem;

    private void Start()
    {   
        sellingItem = transform.parent.GetComponent<SellingItem>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {   
        Debug.Log("Value Clicked");
        sellingItem.OnPointerClick(eventData);
    }
}

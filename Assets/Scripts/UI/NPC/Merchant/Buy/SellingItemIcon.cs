using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellingItemIcon : MonoBehaviour, IPointerClickHandler
{
    private SellingItem sellingItem;

    private void Start()
    {
        sellingItem = transform.parent.GetComponent<SellingItem>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        sellingItem.OnPointerClick(eventData);
    }

}

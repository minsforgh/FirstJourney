using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToSellItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ToSellItem toSellItem;

    private void Start()
    {
        toSellItem = GetComponentInChildren<ToSellItem>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        toSellItem.OnPointerClick(eventData);
    }


}

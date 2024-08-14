using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{   
    // OnDrop은 해당 오브젝트에 Drop이 떨어졌을 경우 호출 (드래그 중인 오브젝트에서 호출되는 것이 아님)
    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedItem = eventData.pointerDrag;
        if (draggedItem != null)
        {
            ItemIcon sourceIcon = draggedItem.GetComponentInParent<ItemIcon>();
            ItemIcon targetIcon = GetComponent<ItemIcon>();

            if (sourceIcon != null && targetIcon != null && sourceIcon != targetIcon)
            {
                if (targetIcon.Item != null)
                {   
                    // 각 itemSlot 간의 교환
                    ItemData tempItem = sourceIcon.Item;
                    sourceIcon.SetItem(targetIcon.Item);
                    targetIcon.SetItem(tempItem);

                    AudioManager.Instance.PlayAudio(AudioClipType.Basic);
                }
            }
        }
    }
}

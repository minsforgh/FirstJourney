using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemInfoBase : MonoBehaviour
{
   public Image itemIcon;

   public abstract void SetInfo(ItemData item);
}

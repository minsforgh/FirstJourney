using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    public DropTable dropTable;
    public GameObject dropItemPrefab;

    private bool canDrop;

    void Start()
    {
        canDrop = true;
    }

    public void DropItems()
    {
        if (canDrop)
        {   
            canDrop = false;
            dropTable.coin.Value = dropTable.CalCoinValue();
            SpawnItem(dropTable.coin);

            for (int i = 0; i < dropTable.items.Length; i++)
            {
                if (Random.value < dropTable.dropRates[i])
                {
                    SpawnItem(dropTable.items[i]);
                }
            }
        }
    }

    private void SpawnItem(ItemData itemData)
    {
        GameObject instance = Instantiate(dropItemPrefab);
        DropItem dropItem = instance.GetComponent<DropItem>();
        SpriteRenderer spriteRenderer = instance.GetComponent<SpriteRenderer>();
        dropItem.itemData = itemData;
        spriteRenderer.sprite = itemData.Sprite;

        instance.transform.position = transform.position;
    }

}

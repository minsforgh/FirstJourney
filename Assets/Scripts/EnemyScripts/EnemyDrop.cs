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
            if(dropTable.chest != null)
            {
                Instantiate(dropTable.chest, transform.position, Quaternion.identity);
                return;
            }
            
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

        Vector2 randomOffset = Random.insideUnitCircle * 0.5f; // 0.5f는 범위를 조절하는 값
        instance.transform.position = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
    }

}

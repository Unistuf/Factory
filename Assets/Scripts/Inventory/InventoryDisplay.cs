using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    public float inventoryRefreshRate;
    public Inventory inventory;

    public GameObject inventoryBoxPrefab;
    public Transform inventoryBoxSpawnPos;
    public List<InventoryBox> spawnedBoxes;

    void Start()
    {
        StartCoroutine(RefreshInventory());
    }

    IEnumerator RefreshInventory()
    {
        for (int i = spawnedBoxes.Count; i > 0; i--)
        {
            Destroy(spawnedBoxes[0].gameObject);
            spawnedBoxes.RemoveAt(0);
        }

        spawnedBoxes = new List<InventoryBox>();

        int x = 0;
        int y = 0;
        int amountInRow = 8;

        for (int i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i].amount > 0)
            {
                InventoryBox temp = Instantiate(inventoryBoxPrefab, new Vector3(inventoryBoxSpawnPos.position.x + (135 * x), inventoryBoxSpawnPos.position.y + (-175 * y), 0), transform.rotation).GetComponent<InventoryBox>();
                temp.gameObject.transform.parent = inventoryBoxSpawnPos;
                spawnedBoxes.Add(temp);
                temp.item = inventory.items[i];

                x += 1;
                if (x >= amountInRow)
                {
                    x = 0;
                    y += 1;
                }
            }
        }

        yield return new WaitForSeconds(inventoryRefreshRate);
        StartCoroutine(RefreshInventory());
    }
}

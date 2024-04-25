using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryBox : MonoBehaviour
{
    public Item item;

    public RawImage itemImage;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemCountText;

    void Update()
    {
        itemImage.texture = item.itemImage;
        itemNameText.text = "" + item.name;
        itemCountText.text = "" + item.amount;
    }
}

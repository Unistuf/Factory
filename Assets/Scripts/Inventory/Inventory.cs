using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    public Texture itemImage;
    public string name;
    public int amount;
}

[Serializable]
public class Recipe
{
    public Item[] inputs;
    public Item[] outputs;

    public float productionTime;
}

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public Item[] items;


    public void AddItem(string name, int amount)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (name == items[i].name)
            {
                items[i].amount += amount;
            }
        }
    }

    public void RemoveItem(string name, int amount)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (name == items[i].name)
            {
                items[i].amount -= amount;
            }
        }
    }

    public int GetItem(string name)
    {
        int returnValue = 0;

        for (int i = 0; i < items.Length; i++)
        {
            if (name == items[i].name)
            {
                returnValue = items[i].amount;
            }
        } 

        return returnValue;       
    }
}

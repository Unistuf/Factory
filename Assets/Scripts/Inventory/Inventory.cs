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
    public string displayName;
    public Texture recipeImage;
    public Item[] inputs;
    public Item[] outputs;

    public float productionTime;
}

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public Item[] items;

    public bool showProductionSpeed;


    public void AddItem(string name, int amount) //Add an amount of items to the inventory given a name (string) and an amount (int)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (name == items[i].name)
            {
                items[i].amount += amount;
            }
        }
    }

    public void RemoveItem(string name, int amount) //Remove an amount of items to the inventory given a name (string) and an amount (int)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (name == items[i].name)
            {
                items[i].amount -= amount;
            }
        }
    }

    public int GetItem(string name) //Get a int of the amount of items the player has given a name (string)
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
    
    public Texture GetItemImage(string name)
    {
        Texture returnValue = null;

        for (int i = 0; i < items.Length; i++)
        {
            if (name == items[i].name)
            {
                returnValue = items[i].itemImage;
            }
        } 

        return returnValue;         
    }
}

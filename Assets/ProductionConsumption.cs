using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Generator
{
    public string[] item;
    public int[] production;
}

[System.Serializable]
public class Resource
{
    public string name;
    public int productionRate;
}

[System.Serializable]
public class ConsumptionOrder
{
    public string name;
    public int amount;

    public ConsumptionOrder(string n_name, int n_amount)
    {
        name = n_name;
        amount = n_amount;
    }
}


public class ProductionConsumption : MonoBehaviour
{
    public List<Generator> generators;
    public List<ConsumptionOrder> consumptionOrders;

    public Resource[] resources;

    void Start()
    {
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        consumptionOrders.Add(new ConsumptionOrder("Metal", 2));
        consumptionOrders.Add(new ConsumptionOrder("Metal", 3));
        consumptionOrders.Add(new ConsumptionOrder("Water", 1));

        ProductionLoop();
        ConsumptionLoop();

        yield return new WaitForSeconds(1f);
        StartCoroutine(Loop());
    }

    public void AddResource(string itemName, int amount)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            if (resources[i].name == itemName)
            {
                resources[i].productionRate += amount;
            }
        }
    }

    public void RemoveResource(string itemName, int amount)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            if (resources[i].name == itemName)
            {
                resources[i].productionRate -= amount;
            }
        }
    }

    public int GetResourceRate(string name)
    {
        int returnAmount = 0;
        for (int i = 0; i < resources.Length; i++)
        {
            if (resources[i].name == name)
            {
                returnAmount = resources[i].productionRate;
            }
        }

        return returnAmount;
    }

    void ProductionLoop()
    {
        for (int i = 0; i < resources.Length; i++)
        {
            resources[i].productionRate = 0;
        }

        for (int i = 0; i < generators.Count; i++)
        {
            for (int u = 0; u < generators[i].item.Length; u++)
            {
                AddResource(generators[i].item[u], generators[i].production[u]);
            }
        }
    }

    void ConsumptionLoop()
    {
        for (int i = 0; i < consumptionOrders.Count; i++)
        {
            if (GetResourceRate(consumptionOrders[i].name) >= consumptionOrders[i].amount)
            {
                RemoveResource(consumptionOrders[i].name, consumptionOrders[i].amount);

                //inform script attached to consumption order that task was successful

                Debug.Log("Order Fufilled: " + consumptionOrders[i].amount + " " + consumptionOrders[i].name);
            }
            else{
                //inform script attached to consumption order that task was successful

                Debug.Log("Order Faild: " + consumptionOrders[i].amount + " " + consumptionOrders[i].name);
            }

            consumptionOrders.Remove(consumptionOrders[i]);
        }
    }


    public void OrderConsume(string name, int amount)//script also goes here for the consuming object
    {
        consumptionOrders.Add(new ConsumptionOrder(name, amount));
    }
}

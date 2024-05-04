using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinerScript : MonoBehaviour
{
    [Header("Config")]
    public string[] allowedResources;
    public float productionSpeed;
    public int productionAmount;

    [Header("Runtime")]
    public Inventory inventory;
    public string currentResource;

    [Header("ProductionText")]
    public GameObject textObject;
    public TextMeshProUGUI itemText;
    public RawImage itemImage;    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Resource")
        {
            currentResource = other.gameObject.name;
        }
    }

    void Start()
    {
        inventory = GameObject.Find("Manager").GetComponent<Inventory>();
        StartCoroutine(ProductionLoop());
    }

    void Update()
    {
        if (currentResource != "" && inventory.showProductionSpeed)
        {
            textObject.SetActive(true);

            itemImage.texture = inventory.GetItemImage(currentResource);
            itemText.text = "" + (productionAmount / productionSpeed).ToString("#.#") + "/s";
        }
        else{
            textObject.SetActive(false);
        }
    }

    IEnumerator ProductionLoop()
    {
        if (IsResourceAllowed(currentResource))
        {
            inventory.AddItem(currentResource, productionAmount);
        }

        yield return new WaitForSeconds(productionSpeed);
        StartCoroutine(ProductionLoop());
    }

    bool IsResourceAllowed(string name)
    {
        bool result = false;

        for (int i = 0; i < allowedResources.Length; i++)
        {
            if (currentResource == allowedResources[i])
            {
                result = true;
            }
        }

        return result;
    }
}

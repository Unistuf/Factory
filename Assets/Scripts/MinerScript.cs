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
        if (other.gameObject.tag == "Resource") //Gets the resource tile below it and set it as the current mining resource
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
        if (IsResourceAllowed(currentResource) && inventory.showProductionSpeed) //If production display is turned on and the current resource is one that this machine can produce
        {
            textObject.SetActive(true); //turn on the mining display object

            itemImage.texture = inventory.GetItemImage(currentResource); //Set the image to the current resource
            itemText.text = "" + (productionAmount / productionSpeed).ToString("#.##") + "/s"; //Show the amount per second
        }
        else{
            textObject.SetActive(false);
        }
    }

    IEnumerator ProductionLoop()
    {
        if (IsResourceAllowed(currentResource)) //if the resource is in the allowed list
        {
            inventory.AddItem(currentResource, productionAmount); //Add item to inventory
        }

        yield return new WaitForSeconds(productionSpeed); //Wait out the production speed
        StartCoroutine(ProductionLoop());
    }

    bool IsResourceAllowed(string name) //Checks if the resource is in the list of minable resources for this machine
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

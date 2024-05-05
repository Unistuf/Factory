using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CrafterScript : MonoBehaviour
{
    [Header("PreRequisets")]
    public Inventory inventoryScript;
    public CrafterUI crafterUI;
    public BuildScript buildScript;

    [Header("Settings")]
    public Recipe[] recipeOptions;

    [Header("Runtime")]
    public Recipe currentRecipe;

    [Header("ProductionText")]
    public GameObject textObject;
    public TextMeshProUGUI itemText;
    public RawImage itemImage;


    void Start()
    {
        inventoryScript = GameObject.Find("Manager").GetComponent<Inventory>();
        buildScript = GameObject.Find("Manager").GetComponent<BuildScript>();

        currentRecipe = new Recipe();
        currentRecipe.displayName = "";

        StartCoroutine(CraftLoop());
    }

    void Update()
    {
        if (currentRecipe.displayName != "" && inventoryScript.showProductionSpeed)
        {
            textObject.SetActive(true);

            itemImage.texture = currentRecipe.recipeImage;
            itemText.text = "" + (currentRecipe.inputs[0].amount / currentRecipe.productionTime).ToString("#.#") + "/s";
        }
        else{
            textObject.SetActive(false);
        }
    }

    void OnMouseDown() //Opens the crafting Ui when the crafting building is clicked
    {
        if (buildScript.selectedBuilding != null)
        {
            crafterUI.gameObject.SetActive(true);
            crafterUI.OpenUI(this);
            crafterUI.currentCrafter = this;
            crafterUI.recipes = recipeOptions;
            crafterUI.selectedRecipe = null;

            crafterUI.CreateIcons();
        }
    }

    IEnumerator CraftLoop() //Creates items based on its craft time and resources
    {
        if (currentRecipe.displayName != "")
        {
            bool CheckReqs = CheckRecipeRequirements(); 

            if (CheckReqs) //Check if the player has enough resources
            {
                CompleteRecipe(); //Craft Item

                yield return new WaitForSeconds(currentRecipe.productionTime); //Wait the production time
            }   
        }
        else{
            yield return new WaitForSeconds(1f); //If no recipe is selected wait a second
        }

        yield return new WaitForSeconds(0.1f);
        StartCoroutine(CraftLoop()); //Restart Loop
    }

    public bool CheckRecipeRequirements() //Check if the player has enough items to meet all the recipe requirements
    {
        bool result = true;

        for (int i = 0; i < currentRecipe.inputs.Length; i++)
        {
            if (inventoryScript.GetItem(currentRecipe.inputs[i].name) < currentRecipe.inputs[i].amount)
            {
                result = false;
            }
        }

        return result;
    }

    public void CompleteRecipe() //Give and remove items from the player based on the recipe
    {
        for (int i = 0; i < currentRecipe.inputs.Length; i++)
        {
            inventoryScript.RemoveItem(currentRecipe.inputs[i].name, currentRecipe.inputs[i].amount);
        }

        for (int i = 0; i < currentRecipe.outputs.Length; i++)
        {
            inventoryScript.AddItem(currentRecipe.outputs[i].name, currentRecipe.outputs[i].amount);
        }
    }
}

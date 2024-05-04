using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    void Start()
    {
        inventoryScript = GameObject.Find("Manager").GetComponent<Inventory>();
        buildScript = GameObject.Find("Manager").GetComponent<BuildScript>();

        currentRecipe = new Recipe();
        currentRecipe.displayName = "";

        StartCoroutine(CraftLoop());
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
        Debug.Log("a");
        if (currentRecipe.displayName != "")
        {
            Debug.Log("B");
            bool CheckReqs = CheckRecipeRequirements(); 

            if (CheckReqs) //Check if the player has enough resources
            {
                Debug.Log("C");
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

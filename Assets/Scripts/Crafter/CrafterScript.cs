using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrafterScript : MonoBehaviour
{
    public Inventory inventoryScript;
    public CrafterUI crafterUI;

    public Recipe[] recipeOptions;

    public Recipe currentRecipe;


    void Start()
    {
        inventoryScript = GameObject.Find("Manager").GetComponent<Inventory>();

        StartCoroutine(CraftLoop());
    }

    void OnMouseDown()
    {
        crafterUI.gameObject.SetActive(true);
        crafterUI.currentCrafter = this;
        crafterUI.recipes = recipeOptions;
        crafterUI.selectedRecipe = null;

        crafterUI.CreateIcons();
    }

    IEnumerator CraftLoop()
    {
        if (currentRecipe != null)
        {
            bool CheckReqs = CheckRecipeRequirements();

            if (CheckReqs)
            {
                CompleteRecipe();
                yield return new WaitForSeconds(currentRecipe.productionTime);
            }
            else{
                yield return new WaitForSeconds(1f);
            }         
        }
        else{
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(0.1f);
        StartCoroutine(CraftLoop());
    }

    public bool CheckRecipeRequirements()
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

    public void CompleteRecipe()
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

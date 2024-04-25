using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrafterUI : MonoBehaviour
{
    [Header("Recipe")]
    public Recipe[] recipes;
    public Recipe selectedRecipe;

    [Header("Runtime")]
    public List<CrafterButton> crafterButtons;
    public CrafterScript currentCrafter;

    [Header("Button Stuff")]
    public Transform buttonsStartPos;
    public GameObject crafterButtonPrefab;

    [Header("Ui Elements")]
    public GameObject infoMenu;
    public TextMeshProUGUI recipeName;
    public TextMeshProUGUI[] inputText;
    public TextMeshProUGUI[] outputText;
    public TextMeshProUGUI processTimeText;
    public RawImage recipeImage;

    void Update()
    {
        if (selectedRecipe != null)
        {
            infoMenu.SetActive(true);

            recipeName.text = "" + selectedRecipe.displayName;
            processTimeText.text = "" + selectedRecipe.productionTime + "s Production time";
            recipeImage.texture = selectedRecipe.recipeImage;

            for (int i = 0; i < selectedRecipe.inputs.Length; i++)
            {
                inputText[i].text = "" + selectedRecipe.inputs[i].amount + " " + selectedRecipe.inputs[i].name;
            }
            for (int i = 0; i < selectedRecipe.outputs.Length; i++)
            {
                outputText[i].text = "" + selectedRecipe.outputs[i].amount + " " + selectedRecipe.outputs[i].name;
            }            
        }
        else{
            infoMenu.SetActive(false);
        }
    }

    public void CreateIcons()
    {
        for (int i = crafterButtons.Count; i > 0; i--)
        {
            Destroy(crafterButtons[i].gameObject);
        }

        crafterButtons = new List<CrafterButton>();

        int x = 0;
        int y = 0;

        for (int i = 0; i < recipes.Length; i++)
        {
            CrafterButton temp = Instantiate(crafterButtonPrefab, new Vector3(buttonsStartPos.position.x + (x * 120), buttonsStartPos.position.y + (y * 120), 0), transform.rotation).GetComponent<CrafterButton>();
            crafterButtons.Add(temp);

            x += 1;
             
            if (x > 8){x = 0; y -= 1;}

            temp.recipe = recipes[i];
            temp.parent = this;
            temp.id = i;

            temp.gameObject.transform.parent = buttonsStartPos;
            temp.UpdateUI();
        }
    }

    public void SelectButton(int number)
    {
        selectedRecipe = recipes[number];

        for (int i = 0; i < inputText.Length; i++)
        {
            inputText[i].text = "";
        }
        for (int i = 0; i < outputText.Length; i++)
        {
            outputText[i].text = "";
        } 
    }

    public void ConfirmSelection()
    {
        currentCrafter.currentRecipe = selectedRecipe;
        this.gameObject.SetActive(false);
    }
}

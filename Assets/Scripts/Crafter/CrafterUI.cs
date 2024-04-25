using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrafterUI : MonoBehaviour
{
    public Recipe[] recipes;
    public Recipe selectedRecipe;

    public List<CrafterButton> crafterButtons;

    public CrafterScript currentCrafter;

    public Transform buttonsStartPos;
    public GameObject crafterButtonPrefab;

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
        }
    }

    public void SelectButton(int number)
    {
        selectedRecipe = recipes[number];
    }

    public void ConfirmSelection()
    {
        currentCrafter.currentRecipe = selectedRecipe;
        this.gameObject.SetActive(false);
    }
}

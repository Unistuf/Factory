using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildBox : MonoBehaviour
{
    public string buildingName;
    public BuildScript buildScript;
    public Inventory inventory;

    Buildings thisBuilding;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI ingredient1Text;
    public TextMeshProUGUI ingredient2Text;

    public RawImage ingredient1Image;
    public RawImage ingredient2Image;

    void Start()
    {
        thisBuilding = buildScript.GetBuilding(buildingName);

        nameText.text = "" + thisBuilding.name;

        ingredient1Image.texture = thisBuilding.ingredient1Image;
        ingredient2Image.texture = thisBuilding.ingredient2Image;
    }

    void Update()
    {
        ingredient1Text.text = "" + thisBuilding.ingredient1Name + " " + inventory.GetItem(thisBuilding.ingredient1Name) + "/" + thisBuilding.ingredient1Cost;
        ingredient2Text.text = "" + thisBuilding.ingredient2Name + " " + inventory.GetItem(thisBuilding.ingredient2Name) + "/" + thisBuilding.ingredient2Cost;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Buildings
{
    public GameObject prefab;
    public string name;
    public string buildingType;

    public string ingredient1Name;
    public string ingredient2Name;

    public int ingredient1Cost;
    public int ingredient2Cost;

}

public class BuildScript : MonoBehaviour
{
    [Header("Prerequisets")]
    public Inventory inventory;
    public SnappingCursor cursor;
    public CrafterUI crafterUI;
    public GameObject buildingsParent;

    [Header("Runtime")]
    [SerializeField]
    public Buildings[] buildings;
    public List<GameObject> SpawnedBuildings;
    public Buildings selectedBuilding;


    void Start()
    {
        selectedBuilding.name = "";
    }

    public void SelectToBuild(string buildingName) //Checks if the building exists given the name, then selects it
    {
        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i].name == buildingName)
            {
                selectedBuilding = buildings[i];
            }
        }
    }

    public void OnCursorDown() //Gets when the mouse is clicked from SnappingCursor.cs
    {
        if (selectedBuilding != null)
        {
            Build(); //Attempt to instantiate the building
        }
    }

    void Update()
    {
        
    }

    bool canBuildReqs;
    bool canBuildPos;

    public void Build()
    {
        canBuildReqs = CheckBuildRequirements(selectedBuilding); //Check if the player has enough items to build

        canBuildPos = !CheckPositionForBuilding(new Vector2(cursor.snappedMousePosInWorld.x, cursor.snappedMousePosInWorld.y)); //Check if theres already a building in the attempted place

        if (canBuildReqs && canBuildPos && selectedBuilding != null) //If all of the above are okay, proceed
        {   
            UseResources(); //Remove the resources used from the inventory

            GameObject temp = Instantiate(selectedBuilding.prefab, cursor.snappedMousePosInWorld, transform.rotation); //Spawn prefab
            temp.transform.parent = buildingsParent.transform; //Set the parent to keep the hierarchy tidy
            temp.name = selectedBuilding.name;

            if (selectedBuilding.buildingType == "Crafter")
            {
                temp.GetComponent<CrafterScript>().crafterUI = crafterUI; //Set the crafterUI variable of the crafter so it can function
            }

            SpawnedBuildings.Add(temp); //Add the spawned building to the list of buildings that has been spawned
        }
    }

    void UseResources() //Removes items from inventory
    {
        inventory.RemoveItem(selectedBuilding.ingredient1Name, selectedBuilding.ingredient1Cost);
        inventory.RemoveItem(selectedBuilding.ingredient2Name, selectedBuilding.ingredient2Cost);
    }

    public void DisableBuildMode() //Used when closing the build menu to deactive building
    {
        selectedBuilding = new Buildings();
        selectedBuilding.name = "";
    }

    public void OnBuildMenuButtonPressed(string buildingName) //Selects the building to be placed when the player clicks it in the build menu
    {
        SelectToBuild(buildingName);
    }

    Buildings returnBuilding;
    public Buildings GetBuilding(string buildingName) //Used to get info about a building given a name
    {
        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i].name == buildingName)
            {
                    returnBuilding = buildings[i];
            }
        }

        return returnBuilding;
    }

    //                          PLACEMENT CHECKS
    //-----------------------------------------------------------------------------------------------
    bool result;
    public bool CheckPositionForBuilding(Vector2 position) //Check if the position has a building in it already
    {
        result = false;

        for (int i = 0; i < SpawnedBuildings.Count; i++)
        {
            if (SpawnedBuildings[i] != null)
            {
                if (position.x == SpawnedBuildings[i].transform.position.x && position.y == SpawnedBuildings[i].transform.position.y)
                {
                    result = true;
                }
            }
        }

        return result;
    }

    public bool CheckBuildRequirements(Buildings building) //Check if the player has enough items to build with
    {
        bool requirementsMet = true;

        if (inventory.GetItem(building.ingredient1Name) < building.ingredient1Cost)
        {
            requirementsMet = false;
        }
        if (inventory.GetItem(building.ingredient2Name) < building.ingredient2Cost)
        {
            requirementsMet = false;
        }

        return requirementsMet;
    }

    //                          Destroy Building
    //-----------------------------------------------------------------------------------------------

    public void DestroyBuilding(GameObject buildingObject)
    {
        Buildings building = GetBuilding(buildingObject.name);

        //Refund the building Costs
        inventory.AddItem(building.ingredient1Name, building.ingredient1Cost);
        inventory.AddItem(building.ingredient2Name, building.ingredient2Cost);

        //Delete Building from scene
        Destroy(buildingObject);  
    }
}

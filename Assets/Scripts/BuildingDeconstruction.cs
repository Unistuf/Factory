using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDeconstruction : MonoBehaviour
{
    public BuildScript buildScript;

    public SpriteRenderer buildingSpriteRenderer;

    [Header("Settings")]
    public float holdTimeRequired;
    float heldTimeLeft = 1f;

    void Start()
    {
        buildScript = FindObjectOfType<BuildScript>().GetComponent<BuildScript>();
    }

    void OnMouseOver()
    {
        HoldCheck(); 
    }

    void Deconstruct()
    {
        buildScript.DestroyBuilding(this.gameObject); //Asks the build manager to destroy this building
    }


    void HoldCheck()
    {
        //Starts the hold timer
        if (Input.GetMouseButtonDown(1))
        {
            heldTimeLeft = holdTimeRequired;
        }

        //Checks if the right mouse button is held
        if (Input.GetMouseButton(1))
        {
            heldTimeLeft -= Time.deltaTime;

            buildingSpriteRenderer.color = new Color(1, 0 + (heldTimeLeft / holdTimeRequired), 0 + (heldTimeLeft / holdTimeRequired)); //Makes the building more red the longer its been held

            //Checks if the right mouse button has been held long enough
            if (heldTimeLeft <= 0)
            {
                Deconstruct();
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            buildingSpriteRenderer.color = Color.white; //Resets to white if the player releases the right mouse button
        }
    }

    void OnMouseExit()
    {
        buildingSpriteRenderer.color = Color.white;//Resets to white if the player move the cursor away
    }
}

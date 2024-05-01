using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingCursor : MonoBehaviour
{
    public Vector3 mousePosInWorld;
    public Vector3 snappedMousePosInWorld;

    public BuildScript buildScript;

    public Collider2D collider;
    public SpriteRenderer sprite;

    public Color cannotPlaceColor;
    public Color canPlaceColor;

    void Update()
    {
        UpdateMouse();
        UpdateSnappedPosition();
        UpdateGraphic();
    }

    void UpdateGraphic()
    {
        if (buildScript.selectedBuilding != null) //If the build mode is active
        {
            collider.enabled = true;            //Set the collider and sprite to active
            sprite.gameObject.SetActive(true);

            if (buildScript.CheckBuildRequirements(buildScript.selectedBuilding) && !buildScript.CheckPositionForBuilding(new Vector2(snappedMousePosInWorld.x, snappedMousePosInWorld.y))) //Check if the building can be place
            {
                sprite.color = canPlaceColor; //Set the color to a blue if it can
            }
            else
            {
                sprite.color = cannotPlaceColor; //Set the color to a red if it can
            }
        }
        else{ //If the build mode is not active
            collider.enabled = false;   //Set the collider and sprite to deactive
            sprite.gameObject.SetActive(false);           
        }
    }

    void UpdateMouse()
    {
        mousePosInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Get the position of the mouse in the world
        mousePosInWorld.z = 0f; // zero z
    }

    void UpdateSnappedPosition()
    {
        snappedMousePosInWorld = new Vector3(Mathf.Round(mousePosInWorld.x), Mathf.Round(mousePosInWorld.y), 0); //Round the mouse positions to snap them to increments of 1

        transform.position = snappedMousePosInWorld;
    }
    
    void OnMouseDown()
    {
        if (buildScript.selectedBuilding != null)
        {
            buildScript.OnCursorDown(); //Active function on the build script when the player clicks
        }
    }
}

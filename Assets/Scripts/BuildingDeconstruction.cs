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

    void Update()
    {

    }

    void OnMouseOver()
    {
        HoldCheck();
    }

    void Deconstruct()
    {
        buildScript.DestroyBuilding(this.gameObject);
    }


    void HoldCheck()
    {
        if (Input.GetMouseButtonDown(1))
        {
            heldTimeLeft = holdTimeRequired;
        }

   
        if (Input.GetMouseButton(1))
        {
            heldTimeLeft -= Time.deltaTime;

            buildingSpriteRenderer.color = new Color(1, 0 + (heldTimeLeft / holdTimeRequired), 0 + (heldTimeLeft / holdTimeRequired));

            if (heldTimeLeft <= 0)
            {
                Deconstruct();
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            buildingSpriteRenderer.color = Color.white;
        }
    }

    void OnMouseExit()
    {
        buildingSpriteRenderer.color = Color.white;
    }
}

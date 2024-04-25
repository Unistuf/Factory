using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrafterButton : MonoBehaviour
{
    [Header("DONT TOUCH, CHANGED ON RUNTIME")]
    public Recipe recipe;
    public int id;
    public CrafterUI parent;

    public void ButtonPressed()
    {
        parent.SelectButton(id);
    }

    public void UpdateUI()
    {

    }
}

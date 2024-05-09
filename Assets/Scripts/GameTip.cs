using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTip : MonoBehaviour
{
    public string message;

    private void OnMouseEnter()
    {
        GameTipManager._instance.DisplayToolTip(message);
    }

    private void OnMouseExit()
    {
        GameTipManager._instance.HideToolTip();
    }
}

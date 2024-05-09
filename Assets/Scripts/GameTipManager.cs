using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTipManager : MonoBehaviour
{

    public static GameTipManager _instance;

    public TextMeshProUGUI txtComponent;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void DisplayToolTip(string message)
    {
        gameObject.SetActive(true);
        txtComponent.text = message;
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
        txtComponent.text = string.Empty;

    }

}

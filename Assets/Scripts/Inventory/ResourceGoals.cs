using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class ResourceGoals : MonoBehaviour
{
    [SerializeField] int currentTier;
    [SerializeField] Item[] tier0Goals;
    [SerializeField] Item[] tier1Goals;
    [SerializeField] Item[] tier2Goals;
    [SerializeField] Item[] tier3Goals;
    [SerializeField] Item[] tier4Goals;
    [SerializeField] UnityEngine.UI.Image resourceGoalImagePrefab;

    [SerializeField] Inventory inventory;
    [SerializeField] GameObject UIParent;


    // Start is called before the first frame update
    void Start()
    {
        LoadItemGoals(currentTier);

        StartCoroutine(CheckGoalsLoop());
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void LoadItemGoals(int tier)
    {
        // Clear previous goals
        foreach (Transform child in UIParent.transform)
        {
            Destroy(child.gameObject);
        }

        switch (tier)
        {
            case 0:
                for (int i = 0; i <= tier0Goals.Length - 1; i++)
                {
                    UnityEngine.UI.Image newGoalImage = Instantiate(resourceGoalImagePrefab, UIParent.transform);
                    //resourceGoalImageList[i].sprite = tier0Goals[i].itemImage;
                    newGoalImage.GetComponentInChildren<TextMeshProUGUI>().SetText($"{tier0Goals[i].name}\n{inventory.GetItem(tier0Goals[i].name)} / {tier0Goals[i].amount}");
                }
                break;        
            case 1:
                for (int i = 0; i <= tier1Goals.Length - 1; i++)
                {
                    UnityEngine.UI.Image newGoalImage = Instantiate(resourceGoalImagePrefab, UIParent.transform);
                    //resourceGoalImageList[i].sprite = tier1Goals[i].itemImage;
                    newGoalImage.GetComponentInChildren<TextMeshProUGUI>().SetText($"{tier1Goals[i].name}\n{inventory.GetItem(tier1Goals[i].name)} / {tier1Goals[i].amount}");
                }
                break;
            case 2:
                for (int i = 0; i <= tier2Goals.Length - 1; i++)
                {
                    UnityEngine.UI.Image newGoalImage = Instantiate(resourceGoalImagePrefab, UIParent.transform);
                    //resourceGoalImageList[i].sprite = tier2Goals[i].itemImage;
                    newGoalImage.GetComponentInChildren<TextMeshProUGUI>().SetText($"{tier2Goals[i].name}\n{inventory.GetItem(tier2Goals[i].name)} / {tier2Goals[i].amount}");
                }
                break;
            case 3:
                for (int i = 0; i <= tier3Goals.Length - 1; i++)
                {
                    UnityEngine.UI.Image newGoalImage = Instantiate(resourceGoalImagePrefab, UIParent.transform);
                    //resourceGoalImageList[i].sprite = tier3Goals[i].itemImage;
                    newGoalImage.GetComponentInChildren<TextMeshProUGUI>().SetText($"{tier3Goals[i].name}\n{inventory.GetItem(tier3Goals[i].name)} / {tier3Goals[i].amount}");
                }
                break;
            case 4:
                for (int i = 0; i <= tier4Goals.Length - 1; i++)
                {
                    UnityEngine.UI.Image newGoalImage = Instantiate(resourceGoalImagePrefab, UIParent.transform);
                    //resourceGoalImageList[i].sprite = tier4Goals[i].itemImage;
                    newGoalImage.GetComponentInChildren<TextMeshProUGUI>().SetText($"{tier4Goals[i].name}\n{inventory.GetItem(tier4Goals[i].name)} / {tier4Goals[i].amount}");
                }
                break;
        }
    }

    public IEnumerator CheckGoalsLoop()
    {
        yield return new WaitForSeconds(5);

        LoadItemGoals(currentTier);

        switch (currentTier)
        {
            case 0:
                foreach (Item item in tier0Goals)
                {
                    if (item.amount < inventory.GetItem(item.name))
                    {
                        Debug.Log($"{item.name} fulfilled");

                        // Code to unlock this tier goes here
                    }
                }
                break;            
            case 1:
                foreach (Item item in tier1Goals)
                {
                    if (item.amount < inventory.GetItem(item.name))
                    {
                        Debug.Log($"{item.name} fulfilled");

                        // Code to unlock this tier goes here
                    }
                }
                break;            
            case 2:
                foreach (Item item in tier2Goals)
                {
                    if (item.amount < inventory.GetItem(item.name))
                    {
                        Debug.Log($"{item.name} fulfilled");

                        // Code to unlock this tier goes here
                    }
                }
                break;            
            case 3:
                foreach (Item item in tier3Goals)
                {
                    if (item.amount < inventory.GetItem(item.name))
                    {
                        Debug.Log($"{item.name} fulfilled");

                        // Code to unlock this tier goes here
                    }
                }
                break;            
            case 4:
                foreach (Item item in tier4Goals)
                {
                    if (item.amount < inventory.GetItem(item.name))
                    {
                        Debug.Log($"{item.name} fulfilled");

                        // Code to unlock this tier goes here
                    }
                }
            break;
        }

        StartCoroutine(CheckGoalsLoop());
    }
}

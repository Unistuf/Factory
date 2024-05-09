using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEditor.Build.Content;
using UnityEngine;

[System.Serializable]
public class TierGoals
{
    public Item[] requirements;
    public GameObject areaUnlocked;
}

public class ResourceGoals : MonoBehaviour
{
    [SerializeField] int currentTier;
    [SerializeField] UnityEngine.UI.RawImage resourceGoalImagePrefab;

    [SerializeField] TierGoals[] tierGoals;

    [SerializeField] Inventory inventory;
    [SerializeField] GameObject UIParent;

    // Start is called before the first frame update
    void Start()
    {
        LoadItemGoals(currentTier);

        StartCoroutine(CheckGoalsLoop());
    }


    public void LoadItemGoals(int tier)
    {
        // Clear previous goals
        foreach (Transform child in UIParent.transform)
        {
            Destroy(child.gameObject);
        }

        // Then gather the goals for the current tier, looping through the items for that tier
        for (int i = 0; i <= tierGoals[currentTier].requirements.Length - 1; i++)
        {
            UnityEngine.UI.RawImage newGoalImage = Instantiate(resourceGoalImagePrefab, UIParent.transform);
            newGoalImage.texture = inventory.GetItemImage(tierGoals[currentTier].requirements[i].name);
            newGoalImage.GetComponentInChildren<TextMeshProUGUI>().SetText($"{tierGoals[currentTier].requirements[i].name}\n{inventory.GetItem(tierGoals[currentTier].requirements[i].name)} / {tierGoals[currentTier].requirements[i].amount}");
        }
    }

    public IEnumerator CheckGoalsLoop()
    {
        yield return new WaitForSeconds(2);
        
        // Start a counter to check how many resources we've fulfilled
        int fulfilledItems = 0;

        LoadItemGoals(currentTier);

        // Loop through our requirements and check them against our actual inventory amounts
        foreach (Item item in tierGoals[currentTier].requirements)
        {
            // And if we meet it, tick the counter up and debug log that we have fulfilled the goal
            if (item.amount <= inventory.GetItem(item.name))
            {
                fulfilledItems++;
                Debug.Log($"{item.name} fulfilled, {fulfilledItems} total, {tierGoals[currentTier].requirements.Length - fulfilledItems} remaining");
            }

            // And if we fulfil all goals, unlock the next tier
            if (fulfilledItems == tierGoals[currentTier].requirements.Length && currentTier <= 4)
            {
                    Instantiate(tierGoals[currentTier].areaUnlocked);

                    currentTier++;
                    Debug.Log($"Tier {currentTier} unlocked!");
            }
        }
        
        // Then loop and check every few seconds
        StartCoroutine(CheckGoalsLoop());
    }
}

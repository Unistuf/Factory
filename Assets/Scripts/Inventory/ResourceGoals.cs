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

        int fulfilledItems = 0;

        LoadItemGoals(currentTier);

        foreach (Item item in tierGoals[currentTier].requirements)
        {
            if (item.amount < inventory.GetItem(item.name))
            {
                fulfilledItems++;
                Debug.Log($"{item.name} fulfilled, {fulfilledItems} total, {tierGoals[currentTier].requirements.Length - fulfilledItems} remaining");
            }

            if (fulfilledItems == tierGoals[currentTier].requirements.Length)
            {
                currentTier++;

                Debug.Log(currentTier);

                Debug.Log($"Tier {currentTier} unlocked!");
                //Instantiate(tierGoals[currentTier].areaUnlocked);
            }
        }
        
        StartCoroutine(CheckGoalsLoop());
    }
}

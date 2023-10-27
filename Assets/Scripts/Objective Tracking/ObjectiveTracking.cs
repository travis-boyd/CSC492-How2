using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public List<Objective> objectives = new List<Objective>();
    private int UIObjectiveCount = 0;

    public GameObject objectivePanel; // Reference to the Panel containing the TMP Textboxes
    private TMP_Text[] titleTexts; // Array to store the Title Text TMP Text components
    private TMP_Text[] descriptionTexts; // Array to store the Description Text TMP Text components


    void Start()
    {
        // Initialize the objectives that will be active when the level starts
        // examples:
        UIAddObjective(new Objective("key", "Find and collect the special key."));
        UIAddObjective(new Objective("boss", "Conquer the level boss."));
        UIInitialize();
    }

    void UIAddObjective(Objective objective)
    {
        // If there are already 4 objectives in the UI, it is too full
        if (UIObjectiveCount > 3)
        {
            Debug.LogError("Error: objectives panel full");
            return;
        }

        // Otherwise, add objective to the last slot using the UIObjectiveCount
        int index = UIObjectiveCount;
        UIObjectiveCount += 1;

        titleTexts[index] = objective.title;
        descriptionTexts[index] = objective.description;
        if (objective.isComplete)
        {
            UICompleteObjective(objective);
        }

    }

    void UIRemoveObjective(Objective objective)
    {
        // implement this method
        UIObjectiveCount -= 1;
    }

    void UICompleteObjective(Objective objective)
    {
        // doesn't remove it from the UI, but greys it out/crosses it out
        // implement this method
    }


    void UIInitialize()
    {
        if (objectivePanel != null)
        {
            // Initialize arrays to store TMP Text components
            titleTexts = new TMP_Text[4];
            descriptionTexts = new TMP_Text[4];

            Transform[] objectiveObjects = objectivePanel.GetComponentsInChildren<Transform>();

            for (int i = 0; i < 4; i++)
            {
                foreach (Transform child in objectiveObjects)
                {
                    if (child.name == "Objective " + (i + 1))
                    {
                        titleTexts[i] = child.Find("Objective Title Text").GetComponent<TMP_Text>();
                        descriptionTexts[i] = child.Find("Objective Description Text").GetComponent<TMP_Text>();
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Panel reference is not set!");
        }

        // You can use it like this to manipulate the HUD:
            // titleTexts[2].text = "Changing Objective 3's Title";
            // descriptionTexts[2].text = "and its description.";
            // titleTexts[3].enabled = false;
            // descriptionTexts[3].enabled = false;

    }
}
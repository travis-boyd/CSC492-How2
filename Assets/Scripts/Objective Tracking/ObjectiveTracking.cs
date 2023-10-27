using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public List<Objective> objectives = new List<Objective>();
    private int UIObjectiveCount = 0;

    // testing
    public Button testButton;


    public GameObject objectivePanel; // Reference to the Panel containing the TMP Textboxes
    private TMP_Text[] titleTexts; // Array to store the Title Text TMP Text components
    private TMP_Text[] descriptionTexts; // Array to store the Description Text TMP Text components


    void Start()
    {
        UIInitialize();

        //testing
        testButton.onClick.AddListener(Test);
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

        titleTexts[index].text = objective.title;
        descriptionTexts[index].text = objective.description;
        if (objective.isComplete)
        {
            UICompleteObjective(objective);
        }

    }

    void UIRemoveObjective(Objective objective)
    {
        // Find the index of the objective to remove
        int indexToRemove = getObjectiveIndex(objective);

        // If there are active objectives after the one we're removing,
        // shift them all forward, overwriting the previous
        for (int i = indexToRemove; i < UIObjectiveCount - 1; i++)
        {
            titleTexts[i] = titleTexts[i + 1];
            descriptionTexts[i] = descriptionTexts[i + 1];
        }
        // If it's the final active objective, simply deactivate it
       
        // remove final objective
        titleTexts[UIObjectiveCount].enabled = false;
        descriptionTexts[UIObjectiveCount].enabled = false;
        UIObjectiveCount--;
    }

    void UICompleteObjective(Objective objective)
    {
        int indexToComplete = getObjectiveIndex(objective);
        titleTexts[indexToComplete].color = Color.gray;
        titleTexts[indexToComplete].fontStyle |= FontStyles.Strikethrough;
        descriptionTexts[indexToComplete].color = Color.gray;
        descriptionTexts[indexToComplete].fontStyle |= FontStyles.Strikethrough;


        // doesn't remove it from the UI, but greys it out/crosses it out
        // implement this method
    }


    void UIInitialize()
    {
        if (objectivePanel != null)
        {
            // Initialize arrays to store TMP Textboxes 
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

    private int getObjectiveIndex(Objective objective)
    {
        // Find the index of the objective in the two lists of textboxes
        for (int i = 0; i < UIObjectiveCount; i++)
        {
            if (titleTexts[i].text == objective.title && descriptionTexts[i].text == objective.description)
            {
                return i;
            }
        }
        return -1;
    }

    private void Test()
    {
        Debug.Log("Deleting entry #1");
        Debug.Log("Deleting " + titleTexts[0].text);

        UIRemoveObjective(objectives[0]);
    }
}
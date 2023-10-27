using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Objective
{
    public string title;
    public string description;
    public bool isComplete;

    public Objective(string title, string description)
    {
        this.title = title;
        this.description = description;
        this.isComplete = false;
    }
}

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
    }

    void UIAddObjective(Objective objective)
    {
        objectives.Add(objective);
        UIObjectiveCount += 1;

    }

    void UIRemoveObjective(Objective objective)
    {
        // implement this method
        UIObjectiveCount -= 1;
    }

    void CompleteObjective(Objective objective)
    {
        // doesn't remove it from the UI, but greys it out/crosses it out
        // implement this method
    }

    void UpdateObjectiveUI()
    {
        // implement this method
    }

    void UIInitialize()
    {
        if (objectivePanel != null)
        {
            // Initialize arrays to store TMP Text components
            titleTexts = new TMP_Text[4];
            descriptionTexts = new TMP_Text[4];

            Transform[] objectiveObjects = panel.GetComponentsInChildren<Transform>();

            for (int i = 0; i < 4; i++)
            {
                foreach (Transform child in objectiveObjects)
                {
                    if (child.name == "Objective " + (i + 1))
                    {
                        titleTexts[i] = child.Find("Title Text").GetComponent<TMP_Text>();
                        descriptionTexts[i] = child.Find("Description Text").GetComponent<TMP_Text>();
                        // You can access titleTexts[i] and descriptionTexts[i] as needed.

                        //test
                        titleTexts[i].text = "test poop";
                        descriptionTexts[i].text = "teesstt pooooopp";
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Panel reference is not set!");
        }

        Debug.Log("yay");
    }
}
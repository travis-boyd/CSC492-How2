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

    void Start()
    {
        // Initialize the objectives that will be active when the level starts
        // examples:
        AddObjective(new Objective("key", "Find and collect the special key."));
        AddObjective(new Objective("boss", "Conquer the level boss."));
    }

    void AddObjective(Objective objective)
    {
        objectives.Add(objective);
        UpdateObjectiveUI();
    }

    void RemoveObjective(Objective objective)
    {
        // implement this method
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
}
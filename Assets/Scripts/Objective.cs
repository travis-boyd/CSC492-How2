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

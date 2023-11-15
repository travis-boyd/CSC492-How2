using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    // Define tags for object identification
    public string PLAYER = "Player";
    public string OBJECTIVE_1 = "Objective_1";
    ObjectiveManager objectiveManager = ObjectiveManager.Instance;

    private void OnCollisionEnter(Collision collision)
    {
        ObjectiveManager.Instance.UIAddObjective(new Objective("title1","description1"));

        GameObject object_1 = collision.gameObject;
        GameObject object_2 = gameObject;

        /*
        Debug.Log(object_1.tag);
        Debug.Log(object_2.tag);
        */

        if (object_1.CompareTag(PLAYER))
        {
            if (object_2.CompareTag(OBJECTIVE_1))
            {
            }
        }

    }
}
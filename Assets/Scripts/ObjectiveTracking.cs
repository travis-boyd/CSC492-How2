using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ObjectiveManager : MonoBehaviour
{
    public List<Objective> objectives = new List<Objective>();
    public int UIObjectiveCount = 0;
    public int CompletedObjectiveCount = 0;
    public static ObjectiveManager Instance;



    public GameObject objectivePanel; // Reference to the Panel containing the TMP Textboxes
    public TMP_Text[] titleTexts; // Array to store the Title Text TMP Text components
    public TMP_Text[] descriptionTexts; // Array to store the Description Text TMP Text components


    void Start()
    {
        Instance = this;
        UIInitialize();

        // testing
        /*
        UIAddObjective(new Objective("title1","description1"));
        UIAddObjective(new Objective("title2","description2"));
        UIAddObjective(new Objective("title3","description3"));
        UIAddObjective(new Objective("title4","description4"));
        */

        AddObjective(new Objective("Move!", "Reach the SPHERE"));
    }

    public void Win()
    {
        Debug.Log("Win!");
        StartCoroutine(RestartSceneAfterDelayCoroutine(5f));
    }

    public void AddObjective(Objective objective)
    {
        UIAddObjective(objective);
    }
    private void UIAddObjective(Objective objective)
    {
        // If there are already 4 objectives in the UI, it is too full
        if (UIObjectiveCount > 3)
        {
            Debug.LogError("Error: objectives panel full (" + UIObjectiveCount + ")");
            
            return;
        }

        // Otherwise, add objective to the last slot using the UIObjectiveCount
        int index = UIObjectiveCount;
        UIObjectiveCount += 1;

        titleTexts[index].text = objective.title;
        titleTexts[index].enabled = true;
        descriptionTexts[index].enabled = true;
        descriptionTexts[index].text = objective.description;

        // increase the size of the panel
        RectTransform panelRectTransform = objectivePanel.GetComponent<RectTransform>();
        panelRectTransform.sizeDelta = new Vector2(panelRectTransform.sizeDelta.x, panelRectTransform.sizeDelta.y + 10f);

        if (objective.isComplete)
        {
            UICompleteObjective(objective);
        }

        // make sure panel isn't hidden
        objectivePanel.SetActive(true);

    }

    public void RemoveObjective(int positionToRemove)
    {
        int indexToRemove = positionToRemove - 1;
        UIRemoveObjective(indexToRemove);
    }
    private void UIRemoveObjective(int indexToRemove)
    {
        // If there are active objectives after the one we're removing,
        // shift them all forward, overwriting the previous
        for (int i = indexToRemove; i < 3; i++)
        {
            titleTexts[i].text = titleTexts[i + 1].text;
            descriptionTexts[i].text = descriptionTexts[i + 1].text;
        }

        // remove final objective
        titleTexts[UIObjectiveCount - 1].enabled = false;
        descriptionTexts[UIObjectiveCount - 1].enabled = false;
        UIObjectiveCount--;

        RectTransform panelRectTransform = objectivePanel.GetComponent<RectTransform>();
        panelRectTransform.sizeDelta = new Vector2(panelRectTransform.sizeDelta.x, panelRectTransform.sizeDelta.y - 10f);
        if (UIObjectiveCount == 0)
        {
            objectivePanel.SetActive(false);
        }
    }
    public void RemoveObjective(Objective objective)
    {
        // Find the index of the objective to remove
        int indexToRemove = getObjectiveIndex(objective);
        if (indexToRemove == -1)
        {
            Debug.LogError("Error: objective not found");
            return;
        }

        UIRemoveObjective(indexToRemove);

    }

    public void CompleteObjective(int positionToComplete)
    {
        int indexToComplete = positionToComplete - 1;
        UICompleteObjective(indexToComplete);
        CompletedObjectiveCount++;
        if (CompletedObjectiveCount == UIObjectiveCount)
        {
            Win();
        }

    }
    public void UICompleteObjective(int indexToComplete)
    {
        titleTexts[indexToComplete].color = Color.gray;
        titleTexts[indexToComplete].fontStyle |= FontStyles.Strikethrough;
        descriptionTexts[indexToComplete].color = Color.gray;
        descriptionTexts[indexToComplete].fontStyle |= FontStyles.Strikethrough;
        Flash(Color.green);
    }
    public void UICompleteObjective(Objective objective)
    {
        int indexToComplete = getObjectiveIndex(objective);
        UICompleteObjective(indexToComplete);
    }


    void UIInitialize()
    {
        // set the panel height to 5 (empty)
        RectTransform panelRectTransform = objectivePanel.GetComponent<RectTransform>();
        panelRectTransform.sizeDelta = new Vector2(panelRectTransform.sizeDelta.x, 5f);
        objectivePanel.SetActive(false);

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

                        // make sure they're not visible
                        titleTexts[i].enabled = false;
                        descriptionTexts[i].enabled = false;
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

    public void Flash(Color color)
    {
        StartCoroutine(FlashCoroutine(color));
    }

    private void ReloadScene()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the current scene again to restart
        SceneManager.LoadScene(currentSceneIndex);
    }

    IEnumerator FlashCoroutine(Color color)
    {
        Image panelImage = objectivePanel.GetComponent<Image>();
        Color originalColor = panelImage.color;

        // Flashing effect loop
        float elapsedTime = 0f;
        float flashDuration = 1f;
        Color flashColor = color;

        while (elapsedTime < flashDuration)
        {
            // Alternate between the original color and the flash color
            panelImage.color = (Mathf.FloorToInt(elapsedTime * 10) % 2 == 0) ? flashColor : originalColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset the color to the original color when the flashing is done
        panelImage.color = originalColor;
    }

    private IEnumerator RestartSceneAfterDelayCoroutine(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Restart the scene
        ReloadScene();
    }
}
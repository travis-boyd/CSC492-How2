using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelect : MonoBehaviour
{
    // public int difficulty;
    

    // For now take player to game scene
    public void SelectStage()
    {
        SceneManager.LoadScene("Movement101");
    }

}

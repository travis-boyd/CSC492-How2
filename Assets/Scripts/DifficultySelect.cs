using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelect : MonoBehaviour
{
    // public int difficulty;
    

    // For now take player to game scene
    // every button loads movement 101
    public void SelectStage()
    {
        SceneManager.LoadScene("Movement 101 Maps");
    }

    public void Select_1_1()
    {
        //SceneManager.LoadScene("Camera and Movement Testing");
        SceneManager.LoadScene("Movement 101 Maps");
        
    }
    public void Select_1_2()
    {
        SceneManager.LoadScene("Movement 101 Maps");
    }
    public void Select_1_3()
    {
        SceneManager.LoadScene("Movement 101 Maps");
    }
    public void Select_2_1()
    {
        SceneManager.LoadScene("Movement 101 Maps");
    }
    public void Select_2_2()
    {
        SceneManager.LoadScene("Movement 101 Maps");
    }
    public void Select_2_3()
    {
        SceneManager.LoadScene("Movement 101 Maps");
    }
    public void Select_3_1()
    {
        SceneManager.LoadScene("Movement 101 Maps");
    }
    public void Select_3_2()
    {
        SceneManager.LoadScene("Movement 101 Maps");
    }
    public void Select_3_3()
    {
        SceneManager.LoadScene("Movement 101 Maps");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public string mainMenu = "Main Menu";

    public void retry()
    {
        SceneManager.LoadScene(3);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    
    
}

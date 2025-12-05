using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCommands : MonoBehaviour
{
    public void StartGame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("AI_Interface");
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }


}

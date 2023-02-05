using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenSwitch : MonoBehaviour
{
    public void ToCredits()
    {
        SceneManager.LoadScene("CreditsScreen");
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    public void ToTutorial()
    {
        SceneManager.LoadScene("TutorialScreen");
    }

    public void ToGame()
    {
        SceneManager.LoadScene("Game"); //change to actual game
    }
    
   

}

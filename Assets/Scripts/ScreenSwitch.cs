using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ScreenChange()
    {
       if (this.gameObject.tag == "play")
        {
            SceneManager.LoadScene("Game"); //edit to actual scene name
        }

        if (this.gameObject.tag == "credits")
        {
            SceneManager.LoadScene("CreditsScreen");
        }

        if (this.gameObject.tag == "quit")
        {
            SceneManager.LoadScene("TitleScreen");
        }

        if (this.gameObject.tag == "continue")
        {
            SceneManager.LoadScene("something"); //what does this go to
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyKeyDelay : MonoBehaviour
{
    private float Timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;

        if (Timer >= 2){
            if (Input.anyKey)
            {
                if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("Victory") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("GameOver") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("CreditsScreen"))
                {
                    SceneManager.LoadScene("TitleScreen");
                }
                else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("Tutorial")){
                    SceneManager.LoadScene("Game");
                }
            }
        }
    }
}

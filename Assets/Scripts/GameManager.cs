using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*
    // audio player
    public AudioSource aS;
    public AudioClip[] audioClipArray;
    
    [SerializeField] private AudioSource _musicSource1, _musicSource2, _sfxClick, sfxCapture, _sfxDanger, _sfxOvertaken, _sfxVictory, _sfxDefeat;

    public int currentSound;
    public bool sfxTrigger = false;
    */

    // sound
    public AudioSource tsMusic;

    private static GameManager managerInstance;

    //private float Timer = 0;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        // No duplicate managers
        if (managerInstance == null) {
            managerInstance = this;
        } 
        else {
            Object.Destroy(gameObject);
        }
    }
    /*
    // Start is called before the first frame update
    void Update()
    {
        if (sfxTrigger == true)
        {
            PlaySound();
        }
    }
    */
    // Update is called once per frame
    /*
    public void PlaySound(AudioClip clip)
    {
        //_sfxClick.PlayOneShot(clip);

        aS.clip = audioClipArray[currentSound];
        aS.PlayOneShot(aS.clip);
        sfxTrigger = false;
    }
    */
    void Start()
    {
        // get tsm
        tsMusic = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("TitleScreen") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("CreditsScreen"))
        {
            tsMusic.volume = .4f;
        }
        else {
            tsMusic.volume = 0.0f;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("TitleScreen") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("Tutorial") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("Victory") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("GameOver"))
        {
            
            if (Input.anyKey) {
                if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("Victory") || SceneManager.GetActiveScene() != SceneManager.GetSceneByName ("GameOver"))
                {
                    //SceneManager.LoadScene("TitleScreen");
                }
                else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("Tutorial")){
                    //SceneManager.LoadScene("Game");
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}

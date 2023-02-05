using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // audio player
    public AudioSource aS;
    public AudioClip[] audioClipArray;

    private static GameManager managerInstance;
    
    [SerializeField] private AudioSource _musicSource1, _musicSource2, _sfxClick, sfxCapture, _sfxDanger, _sfxOvertaken, _sfxVictory, _sfxDefeat;

    public int currentSound;
    public bool sfxTrigger = false;

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

    // Start is called before the first frame update
    void Update()
    {
        if (sfxTrigger == true)
        {
            PlaySound();
        }
    }

    // Update is called once per frame

    public void PlaySound(AudioClip clip)
    {
        //_sfxClick.PlayOneShot(clip);

        aS.clip = audioClipArray[currentSound];
        aS.PlayOneShot(aS.clip);
        sfxTrigger = false;
    }
}

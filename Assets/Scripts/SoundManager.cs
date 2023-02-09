using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    //public NodeController nodeTriggers;

    [SerializeField] private AudioSource _sfxClick, _sfxCapture, _sfxDanger, _sfxOvertaken;

    public void PlayClick()
    {
        
    }

    public void PlaySound(AudioClip clip) {
        _sfxClick.PlayOneShot(clip);
    }
}

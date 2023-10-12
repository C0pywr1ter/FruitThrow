using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  public static SoundManager instance;

    float musicLevel;
    [SerializeField] private AudioSource musicSource, effectsSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
   public void PlaySound(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

   // musicLevel = MainMenu.musicLevelValue;
    //    audioSource.volume = musicLevel;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySoundOnClick : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonPressed);
    }
    public void ButtonPressed()
    {
       SoundManager.instance.PlaySound(clip);
    }
}


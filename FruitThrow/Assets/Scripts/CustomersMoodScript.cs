using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomersMoodScript : MonoBehaviour
{
    private Image customerMoodImage;
    private int customerMoodValue;
    private Text customersMoodText;
    private int moodNumber;
    [SerializeField] private Text timer;
    private void Start()
    {
        customerMoodImage = GetComponent<Image>();
    }
    private void Update()
    {
        
    }
    public IEnumerator StartCalculatingMood(int time)
    {
        yield return null;
    }
}

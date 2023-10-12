using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SunController : MonoBehaviour
{
    public float speed = 1;

    public float startRotation = -40 ;
    public float endRotation = 200f;
    public float rotationDuration = 60f; // 60 seconds
  

    private float elapsedTime = 0f;
    private float rotationSpeed;

    float seconds = 0f;
    int hours = 7;
    int minutes = 00;

    public Text clockText;

    public static bool endDay = false;
    public int dayCount = 0;
    [SerializeField] private GameObject endDayMenu;
    [SerializeField] private Text customersAmountTxt;
    [SerializeField] private Text dayTxt;
    [SerializeField] private Text ratingTxt;
   // [SerializeField] private GameObject roomLight;

    float currentRotation = 0;
    
    private UIManager manager;
    private SaveData saveData;
    [SerializeField] private AdsScript adsScript;
    void Start()
    {
        rotationSpeed = (endRotation - startRotation) / rotationDuration;
        transform.rotation = Quaternion.Euler(startRotation, 0f, 0f);

        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<UIManager>();
        saveData = GameObject.FindGameObjectWithTag("Data").GetComponent<SaveData>();

    }

    void Update()
    {
        //if(Time<=)
        elapsedTime += Time.deltaTime;
        seconds += Time.deltaTime * 500f / 2 * speed;        //    
        if(seconds >= 60) { minutes++; seconds = 0; }

        int remainder = minutes % 30;
        int roundedMinutes = remainder >= 15 ? minutes + (30 - remainder) : minutes - remainder;
        if(roundedMinutes == 60)
        {
            roundedMinutes = 0;
          
                minutes = 0;
                hours++;

            
         
           
        }
        if(roundedMinutes == 0)
        {
            clockText.text = $"{hours}:{roundedMinutes}0";

        }
        else
        {
            clockText.text = $"{hours}:{roundedMinutes}";
        }
       
             //
       // if (hours == 12)
       // {
        //    currentRotation = 90 - 80;//
        //    startRotation = 125 - 80;//
       // }
        if(hours == 24)
        {
            // currentRotation = 80;
            //startRotation = 120;
            //hours = 0;
            endDay = true;
            EndDay();
        }
        
        currentRotation = startRotation + elapsedTime * rotationSpeed ;// / 2 ;
        transform.rotation = Quaternion.Euler(currentRotation, 0f, 0f);

        
       
        
       
    }
    void EndDay()
    {
        elapsedTime = 0f;
        seconds = 0f;
        hours = 7;
        Time.timeScale = 0;
        endDayMenu.gameObject.SetActive(true);

        customersAmountTxt.text = manager.customersAmount.ToString();
        dayCount++;
        dayTxt.text = dayCount.ToString();
        ratingTxt.text = manager.rating.ToString();
       
        manager.EndDay();

        /* yield return new WaitForSeconds(2);
         elapsedTime = 0f;
         seconds = 0f;
         hours = 7;
         endDayMenu.SetActive(false);*/
        adsScript.LoadAd();
    }
    void StartDay()
    {

        adsScript.ShowAd();
        endDay = false;
        saveData.Save();
       
        endDayMenu.gameObject.SetActive(false);
    }



}











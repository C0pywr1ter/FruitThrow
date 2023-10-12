using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PotScr : MonoBehaviour
{
    [SerializeField] private Text[] timerSecondsMassiv;
    //private GameObject[] taggedObjects;
    private Text thisTimer;
    public static bool Apple { get; set; }
    public static bool Garlic { get; set; }
    public static bool Ham { get; set; }
    public static bool Onion { get; set; }
    public static bool Pumpkin { get; set; }
    public static bool Tomato { get; set; }
    public static bool Lemon { get; set; }

    public int level;
    public int price;
    public int cookingTime;


    private UIManager manager;
    public int time;
    [SerializeField] private Vector3 waterPosition;
    [SerializeField] private GameObject WaterEffect;

    public int skips = 0;
    private void Start()
    {
        
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<UIManager>();

        timerSecondsMassiv = manager.timerSecondsMassiv;
    }
    
    public int CalculateCookingTime()
    {

     
        return cookingTime;

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "apple(Clone)")
        {
            Apple = true;
            manager.ScoreFruite("apple");
         Instantiate(WaterEffect, gameObject.transform.position, Quaternion.identity);
        
        }
        if (other.name == "garlic(Clone)")
        {
            Garlic = true;
            manager.ScoreFruite("garlic");
            Instantiate(WaterEffect, gameObject.transform.position, Quaternion.identity);
          
        }
        if (other.name == "ham(Clone)")
        {
            Ham = true;
            manager.ScoreFruite("ham");
            Instantiate(WaterEffect, gameObject.transform.position, Quaternion.identity);
          
        }
        if (other.name == "onion(Clone)")
        {
            Onion = true;
            manager.ScoreFruite("onion");
            Instantiate(WaterEffect, gameObject.transform.position, Quaternion.identity);
          
        }
        if (other.name == "pumpkin(Clone)")
        {

            Pumpkin = true;
            manager.ScoreFruite("pumpkin");
            Instantiate(WaterEffect, gameObject.transform.position, Quaternion.identity);
           
        }
        if (other.name == "tomato(Clone)")
        {
            Tomato = true;
            manager.ScoreFruite("tomato");
            Instantiate(WaterEffect, gameObject.transform.position, Quaternion.identity);

        }
        if (other.name == "lemon(Clone)")
        {
            Lemon = true;
            manager.ScoreFruite("lemon");
            Instantiate(WaterEffect, gameObject.transform.position, Quaternion.identity);
        
        }
       
        Destroy(other.gameObject);
        
    }
    private void Update()
    {
        //taggedObjects = GameObject.FindGameObjectsWithTag("Timer");
       // for (int i = 0; i < taggedObjects.Length; i++)
       // {
        //    timerSecondsMassiv[i] = taggedObjects[i].GetComponent<Text>();
        //}
       // if (timerSecondsMassiv[0] != null)
       // {
            foreach (var timer in timerSecondsMassiv)
            {

                if (timer.text == "0")
                {
                    thisTimer = timer;
                    break;
                }
            }
       // }
        
    }
    public IEnumerator CookingCoroutine()
    {
       
        if(skips > 0)
        {
            time = 1;
            skips--;
        }
        else
        {
            time = cookingTime;
        }
        while(time > 0)
        {
           // Debug.Log(time);
       
           
            if(thisTimer != null)
            {
                thisTimer.text = time.ToString();
            }
            time-=2;
            yield return new WaitForSeconds(2);
        }
        manager.FinishOrder();
        time = 0;
        if(thisTimer != null)
        {
            thisTimer.text = "0";
        }
       
    }
}

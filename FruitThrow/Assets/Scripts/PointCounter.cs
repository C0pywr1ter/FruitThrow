using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PointCounter : MonoBehaviour
{
    public static int points = 0;
      bool isBallinHoop = false;
    
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hoop")
        {
         //   isBallinHoop = SpawnBall.canPointCount;
            if (isBallinHoop == false)
            {
                
                points++;
                isBallinHoop = true;



            }
        }
       
    }
    private void Update()
    {
   
       

    }
}

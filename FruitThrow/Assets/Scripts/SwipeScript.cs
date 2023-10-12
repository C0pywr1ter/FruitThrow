using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeScript : MonoBehaviour {

  


    Vector2 startPos, endPos, direction;
	float touchTimeStart, touchTimeFinish;

	[Range(0.9f, Mathf.Infinity)]
	float timeInterval; 

	[SerializeField]
	float throwForceInXandY = 1f; 

	[SerializeField]
	float throwForceInZ = 50f; 

	Rigidbody rb;

    public static int BallsMissed = 0;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();

       
        
    }

  
    void Update()
	{


        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                touchTimeStart = Time.time;
                startPos = Input.GetTouch(0).position;

            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                touchTimeStart = Time.time;
                startPos = Input.mousePosition;
            }

        }

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
		{

			if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                touchTimeFinish = Time.time;
                timeInterval = touchTimeFinish - touchTimeStart;


                endPos = Input.GetTouch(0).position;


                direction = startPos - endPos;


                if (timeInterval > 0)
                {
                    rb.isKinematic = false;
                    rb.AddForce(-direction.x * throwForceInXandY, -direction.y * throwForceInXandY, throwForceInZ / timeInterval);
                }


                Destroy(gameObject, 10f);
            }
           

		}
		else if (Input.GetMouseButtonUp(0))
		{
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                touchTimeFinish = Time.time;
                timeInterval = touchTimeFinish - touchTimeStart;



                endPos = Input.mousePosition;


                direction = startPos - endPos;

                if (timeInterval > 0.02f)
                {
                    rb.isKinematic = false;
                    rb.AddForce(-direction.x * throwForceInXandY, -direction.y * throwForceInXandY, throwForceInZ / timeInterval);
                }
                BallsMissed++;
                Destroy(gameObject, 10f);
                
            }

            
		}
	}
}

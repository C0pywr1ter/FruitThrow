using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour {

	[SerializeField]
	GameObject ball;
	GameObject ballOnScene;

	private List<GameObject> ingridients;
	public GameObject[] food;
	private UIManager uiManager;
    private void Start()
    {
		uiManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<UIManager>();
		ingridients = uiManager.ingridientsList;

	//	Instantiate(food[Random.Range(0,6)], new Vector3(-11f, 5.25f, -6.78f), Quaternion.identity);
	}
    private void Update()
    {
		
		ballOnScene = GameObject.FindGameObjectWithTag("Sphere");
    }
    public void Spawn()
	{
	if(ingridients.Count > 0)
		{
            Destroy(ballOnScene);
            Instantiate(ingridients[0], new Vector3(-11f, 5.25f, -6.78f), Quaternion.identity);
            uiManager.DeleteIngridientFromList();
        }
	
		
	}
}

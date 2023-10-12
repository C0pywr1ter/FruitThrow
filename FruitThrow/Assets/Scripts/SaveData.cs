using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Unity.VisualScripting;

public class SaveData : MonoBehaviour
{
    public static string path;
    public GameData gameData;
    private UIManager manager;
    private SunController sunController;
    public GameObject sun;
    
    void Awake()
    {

        path = Application.dataPath + "/SaveData.json"; 
        gameData = new GameData();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<UIManager>();
        sunController = sun.GetComponent<SunController>();

       
    }

   
    public void Load()
    {
        if (File.Exists(path))
        {
           

            string data = File.ReadAllText(path);
           
            gameData = JsonUtility.FromJson<GameData>(data);
            for (int i = 1; i < gameData.playerPotObjects.Count - 1; i++)
            {
                GameObject prefab = Resources.Load<GameObject>(gameData.playerPotObjects[i].objectId);
                GameObject obj = Instantiate(prefab, gameData.playerPotObjects[i].position, gameData.playerPotObjects[i].rotation);
                obj.name = prefab.name;
                obj.gameObject.SetActive(false);
                obj.transform.position = gameData.playerPotObjects[i].position;
                obj.transform.rotation = gameData.playerPotObjects[i].rotation;

                manager.playersPots.Add(obj);

            }
            /*    foreach (var objectData in gameData.playerPotObjects)
            {

                // GameObject obj = GameObject.Instantiate(objectData.playerPotObj);
                GameObject prefab = Resources.Load<GameObject>(objectData.objectId);
                GameObject obj = Instantiate(prefab, objectData.position, objectData.rotation);
                obj.name = prefab.name;
                obj.gameObject.SetActive(false);
                obj.transform.position = objectData.position;
                obj.transform.rotation = objectData.rotation;

                manager.playersPots.Add(obj);
            }
*/
            manager.money = gameData.playerMoney;
            manager.rating = gameData.playerRating;
           

            


            sunController.dayCount = gameData.playerDayCount;

            Debug.Log("Data Loaded");
        }
        else Debug.Log("Can't read saved data");
    }
    public void Save()
    {
         
       //удалять объекты со сцены //////////////////////////////////////////////////////////////////////////////////

        gameData.playerMoney = manager.money; 
        gameData.playerRating = manager.rating;
        gameData.playerPots = manager.playersPots;
        gameData.cookingPots = manager.cookingPots;
        gameData.playerDayCount = sunController.dayCount;
      /*  foreach (var pot in gameData.cookingPots)   ///////////////////
        {
            gameData.playerPots.Add(pot);
            gameData.cookingPots.Remove(pot);

        }

        */
        gameData.playerPotObjects.Clear();

       



            foreach (var pot in gameData.playerPots)
            {
                PlayerPotObject playerPotObject = new PlayerPotObject(pot.transform.position, pot.transform.rotation, pot.gameObject, pot.name);


                gameData.playerPotObjects.Add(playerPotObject);
            }
        
       /* else
        {
            foreach (var pot in gameData.playerPots)
            {
                PlayerPotObject playerPotObject = new PlayerPotObject(pot.transform.position, pot.transform.rotation, pot.gameObject);

                for (int i = 0; i < gameData.playerPots.Count - 1; i++)
                {
                    if(gameData.playerPots.Count > gameData.playerPotObjects.Count)
                    {

                    }
                    gameData.playerPotObjects[i] = playerPotObject;
                }
                gameData.playerPotObjects.Add(playerPotObject);
            }
        }
        */
        string jsonData = JsonUtility.ToJson(gameData, true);

        File.WriteAllText(path, jsonData);  
        Debug.Log("Data Saved");
    }

    
}

[System.Serializable]

public class GameData
{
    public List<GameObject> playerPots;
    public List<GameObject> cookingPots;
    public int playerMoney;
    public float playerRating;
    public int playerDayCount;
    public List<PlayerPotObject> playerPotObjects = new List<PlayerPotObject>();
}
[System.Serializable]

public class PlayerPotObject
{
    public Vector3 position;
    public Quaternion rotation;
  
    public GameObject playerPotObj;
    public string objectId;

    public PlayerPotObject(Vector3 position, Quaternion rotation, GameObject playerPotObject, string objectId)
    {
        this.position = position;
        this.rotation = rotation;
    this.playerPotObj = playerPotObject;
        this.objectId = objectId;
       
    }
    
}
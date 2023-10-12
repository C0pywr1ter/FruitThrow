using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Color = UnityEngine.Color;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Camera camera;



   /* [SerializeField] private Image customerMood;
    [SerializeField] private Text customerMoodText;
    private int customerMoodIndex;

    */
    private int missedBalls = 0;

    public Text[] timerSecondsMassiv;
   
    private int time;
    

    [SerializeField] private GameObject[] pots;
    public List<GameObject> playersPots = new List<GameObject>();
    public List<GameObject> cookingPots = new List<GameObject>();
    [SerializeField] private GameObject potOnScreen;
    [SerializeField] private GameObject potPosition;

    private string mistakeString;
    private bool isMistakeMade = false;
    [SerializeField] private GameObject mistakeScreen;
    [SerializeField] private Text mistakeText;



    [SerializeField] private GameObject KitchenMenu;
    bool isKitchenMenuActive = false;


    [SerializeField] private GameObject IngridientsMenu;
    [SerializeField] private GameObject IngridientsMenu2;
    bool isIngridientsMenuActive = false;
    public List<GameObject> ingridientsList;
    private bool canCameraTurn = false;


    public int money = 0;
    private int expectedMoney = 0;
    [SerializeField] private Text moneyTxt;
    [SerializeField] private Text moneyTxt2;
    [SerializeField] private Text expectedMoneyText;

    private bool apple;
    private bool garlic;
    private bool ham;
    private bool onion;
    private bool pumpkin;
    private bool tomato;
    private bool lemon;


    [SerializeField] private Sprite[] spritesCustomers;
    [SerializeField] private Sprite[] spritesDishes;
    [SerializeField] private GameObject[] gameObjectIngridients;
    [SerializeField] private Image imageCustomer;
    [SerializeField] private Image imageDish;
    [SerializeField] private GameObject[] ingridientPlacing = new GameObject[4];
    [SerializeField] private List<GameObject> ingridients;


    int points;
    [SerializeField] public Text pointsText;




    private PotScr potscr;
    private SaveData saveData;


    bool isImagesEnabled = false;
    bool isCookingStarted = false;


    public AudioSource audioSource;

    public int customersAmount;
    public float rating;
    private float expectedRating;
    private bool endDay = false;

    [SerializeField] private GameObject[] PotsOnScreen;
    [SerializeField] private GameObject WaterEffect;
    bool isEffectActive = false;

  

    int f = 0;
    int g = 0;
    private void Awake()
    {
        saveData = GameObject.FindGameObjectWithTag("Data").GetComponent<SaveData>();
        saveData.Load();
    }
    void Start()
    {

        
        if (cookingPots != null)
        {
            foreach (var pot in cookingPots)   
            {
              playersPots.Add(pot);
               cookingPots.Remove(pot);

            }
        }

        playersPots[0].SetActive(true);
       

        ChangeOrder();


       

        potOnScreen = GameObject.FindGameObjectWithTag("Hoop");



        potscr = GameObject.FindGameObjectWithTag("Hoop").GetComponent<PotScr>();


    }
    void ChangeOrder()
    {

        expectedMoney = 0;
        imageCustomer.sprite = spritesCustomers[Random.Range(0, spritesCustomers.Length)];
        imageDish.sprite = spritesDishes[Random.Range(0, spritesDishes.Length)];
        int randCount = Random.Range(0, 5);
    

        Transform parentTransform = ingridientPlacing[0].transform.parent;
        Quaternion ingridientRotation = ingridientPlacing[0].transform.rotation;

        int i = 0;
        do
        {

            int rand = Random.Range(0, gameObjectIngridients.Length);
            ingridients.Add(Instantiate(gameObjectIngridients[rand], ingridientPlacing[i].transform.position, ingridientRotation));


            ingridients[i].transform.SetParent(parentTransform);
            ingridients[i].layer = 5;
            i++;
        }
        while (i < randCount);
         
        if (randCount == 1)
        {
            expectedMoney = Random.Range(1, 2);
            expectedRating += 0.05f;
        }
        if (randCount == 2)
        {
            expectedMoney = Random.Range(2, 5);
            expectedRating += 0.1f;
        }
        if (randCount == 3)
        {
            expectedMoney = Random.Range(5, 7);
            expectedRating += 0.15f;
        }
        if (randCount == 4)
        {
            expectedMoney = Random.Range(7, 10);
            expectedRating += 0.2f;
        }

    }

    void Update()
    {
       




        potOnScreen = GameObject.FindGameObjectWithTag("Hoop");
        if (potOnScreen != null)
        {
            potscr = GameObject.FindGameObjectWithTag("Hoop").GetComponent<PotScr>();
        }

        moneyTxt2.text = money.ToString() + "$";


        endDay = SunController.endDay;



        if (!CheckImages() && isCookingStarted == true)
        {

            isCookingStarted = false;
            StartCoroutine(potscr.CookingCoroutine());
          

            ChangePots();

        }
        
        moneyTxt.text = money.ToString() + "$";
     
        points = PointCounter.points; pointsText.text = points.ToString();




        if (canCameraTurn)
        {
            StartCoroutine(TurnCamera());
        }
        if (!isIngridientsMenuActive)
        {
            StopCoroutine(TurnCamera());
            camera.transform.rotation = Quaternion.Euler(0, 0, 0);
        }



    }
    public void EndDay()
    {

        customersAmount = 0;

    }
    private bool CheckImages()
    {


        if(ingridients.Count == 0)
        {
         
            ChangeOrder();
            f++;
            Debug.Log(f);
            isImagesEnabled = false;
            isCookingStarted = true;

        }
        else
        {
            isImagesEnabled = true;
            isCookingStarted = false;
        }

        return isImagesEnabled;
    }
   
    private IEnumerator ShowMoneyProfit()
    {
        expectedMoneyText.gameObject.SetActive(true);

        if (expectedMoney > 0)
        {
            expectedMoneyText.text = "+" + expectedMoney.ToString();
            expectedMoneyText.color = Color.green;
        }
        else
        {
            expectedMoneyText.text = "-" + expectedMoney.ToString();
            expectedMoneyText.color = Color.red;
        }
        yield return new WaitForSeconds(2);
        expectedMoneyText.gameObject.SetActive(false);
    }
    void ChangePots()
    {
        cookingPots.Add(playersPots[0]);
        playersPots.Remove(playersPots[0]);


      
        potOnScreen.SetActive(false);

        potOnScreen = null;

        if (playersPots.Count > 0)
        {
           
            playersPots[0].SetActive(true);
           
        }

    }
    public void FinishOrder()
    {

        isCookingStarted = false;

        rating += expectedRating;
       
        money += expectedMoney + potscr.level * 2;
        moneyTxt.text = money.ToString() + "$";
        customersAmount++;


        StartCoroutine(ReturnCookingPots());
        StartCoroutine(ShowMoneyProfit());


        if (potOnScreen == null)
        {
         
            playersPots[0].SetActive(true);

        }



     



    }
    private IEnumerator ReturnCookingPots()
    {
        while(cookingPots.Count == 0)
        {
            yield return new WaitForSeconds(0.5f);
        }
        playersPots.Add(cookingPots[0]);
        cookingPots.Remove(cookingPots[0]);
    }
    public void ScoreFruite(string fruit)
    {

         for (int i = 0; i<ingridients.Count; i++)
          {
            if (ingridients[i] != null && ingridients[i].name.Contains(fruit))// == (fruit + " 1(Clone)"))
              {

                Destroy(ingridients[i]);
                ingridients.RemoveAt(i);

              }

          }

      }
     
        public void Restart()
        {
            SceneManager.LoadScene("Scene01");
            PointCounter.points = 0;


        }
        public void KitchenButton()
        {
            isKitchenMenuActive = !isKitchenMenuActive;
            KitchenMenu.SetActive(isKitchenMenuActive);

        }
        public void IngridientsButton()
        {


            isIngridientsMenuActive = !isIngridientsMenuActive;
            canCameraTurn = isIngridientsMenuActive;

            IngridientsMenu.SetActive(isIngridientsMenuActive);
            IngridientsMenu2.SetActive(isIngridientsMenuActive);
        }
        private IEnumerator TurnCamera()
        {
            canCameraTurn = false;
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * 0.5f;
                camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, Quaternion.Euler(0, -50, 0), t);
                yield return new WaitForEndOfFrame();


            }


        }
        public void TakeIngridient(GameObject ingridient)
        {
            ingridientsList.Add(ingridient);

        }
        public void DeleteIngridientFromList()
        {
            ingridientsList.Remove(ingridientsList[0]);
        }
        public void BuyPot(GameObject PotInStore)
        {
            int price = PotInStore.GetComponent<PotScr>().price;


            if (money >= price)
            {


                GameObject newPot;
                if (playersPots.Count <= 4)
                {

                newPot = Instantiate(PotInStore, potPosition.transform.position, PotInStore.transform.rotation);
                    newPot.name = PotInStore.name;





                    money -= price;
                    playersPots.Add(newPot);
                   
                    newPot.SetActive(false);
                }
                else
                {
                    money -= price;
                    playersPots[0] = PotInStore;

                }
            }
            else
            {
                CallForMistake(mistakeString);
                mistakeString = "You dont have enough money";
                mistakeText.text = mistakeString;
            }
        }
        public void CallForMistake(string mistake)
        {
            isMistakeMade = !isMistakeMade;
            mistakeScreen.SetActive(isMistakeMade);
            mistakeText.text = mistake;


        }
    }



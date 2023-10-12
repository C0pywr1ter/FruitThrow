using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject settingsMenu;
    public GameObject mainMenu;
    public GameObject loadingScreen;


    public static float musicLevelValue = 0.3f;
    public Slider musicSlider;


    private bool activeMenu = true;
    private int activeMenuInt = 1;

    private bool isGameMenuActive = false;
    [SerializeField] private GameObject GameMenu;
    [SerializeField] private GameObject PotsStoreObj;
    [SerializeField] private GameObject UI;
   // [SerializeField] private GameObject IngridientsUI;
    //  [SerializeField] private GameObject PotInStore;
    private bool isPotsStoreActive = false;

    private SaveData saveData;

    
    private void Start()
    {
        saveData = GameObject.FindGameObjectWithTag("Data").GetComponent<SaveData>();
        
    }
    
  /*  void Update()
    {
       
        UpdateMusic();
        

    }
    public void UpdateMusic()
    {
       
        musicLevelValue = musicSlider.value ;////////////////////
     
    }
  */
  
   

    public void MenuStartGame()
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(1);
        saveData.Load();
    }
    public void MenuSettings()
    {
        activeMenu = !activeMenu;
        startMenu.SetActive(activeMenu);
        settingsMenu.SetActive(!activeMenu);
       


    }
    public void MenuButton()
    {
        isGameMenuActive = !isGameMenuActive;
        GameMenu.SetActive(isGameMenuActive);
        if (isGameMenuActive)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

    }
    public void MenuExit()
    {
        Application.Quit();
    }
    public void PotsStore()
    {
        isPotsStoreActive = !isPotsStoreActive;
        PotsStoreObj.SetActive(isPotsStoreActive);
        UI.SetActive(!isPotsStoreActive);
       // IngridientsUI.SetActive(!isPotsStoreActive);
    }
   
}

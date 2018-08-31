using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapGen : MonoBehaviour {

    public string MapSizeString;
    public int mapsizeValue;


    public float WaveTimer;

    private OptionSettings GameSettings;

    public AudioSource backgroundMusic;
    public AudioClip menuMusic;
    public AudioClip gameMusic1;
    public AudioClip gameMusic2;


    private float timer;
    private float replayTime = 40;



    [Header("Loading Fade")]
    private GameObject loadingPanel;
    [SerializeField]
    private Image loadingImage;
    [SerializeField]
    private float fadeAmount = 0f;
    private bool fade = false;
    private bool LoadScreenLoaded= false;

    //temp obj array to check number of Gamecontrollers
    public GameObject[] gos;

    private void Start()
    {

        LoadScreenLoaded = false;
        fadeAmount = 0f;

        loadingPanel = GameObject.Find(Tags.FadeScreen);
        loadingImage = GameObject.Find(Tags.FadeScreen).GetComponent<Image>();
        loadingImage.color = new Color(0f,0f,0f,0f); 
        loadingPanel.SetActive(false);

        //GameObject tempObj = GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER_TAG);
        //if (tempObj !=null)
        //{
        //    Destroy(gameObject);
        //}
    



        AssignDefaults();
        backgroundMusic = Camera.main.GetComponent<AudioSource>();
        backgroundMusic.clip = menuMusic;

        GameSettings = GetComponent<OptionSettings>();

        backgroundMusic.volume = GameSettings.SoundMusic;
        backgroundMusic.Play();


    }


    private void Update()
    {
        if (backgroundMusic.isPlaying == false)
        {
            timer += Time.deltaTime;
            if (timer >=replayTime)
            {
                timer = 0;
                backgroundMusic.volume = GameSettings.SoundMusic;
                backgroundMusic.Play();

            }
        }

        //Fade Screen
        if (fade == true)
        {
            loadingImage.color += new Color(0f, 0f, 0f, 0.05f);
            if (loadingImage.color.a >1f)
            {
                LoadScreenLoaded = true;
                GameSettings.OptionsBoard.SetActive(true);
                 BeginGameSinglePlayer();
            }
        }



    }

    private void AssignDefaults()
    {
        MapSizeString = "Small";
        WaveTimer = 180;
    }

    public void DetermineDifficulty(int DifficultLevel)
    {
        switch (DifficultLevel)
        {

            //easy
            case 0:
                WaveTimer = 180;
                break;
                //Medium
            case 1:
                WaveTimer = 160;
                mapsizeValue = 1;
                break;

                //hard
            case 2:
                WaveTimer = 120;
                mapsizeValue = 2;
                break;


        }
        Debug.Log(MapSizeString);

    }

    public void DetermineSize(int DropValue)
    {

        switch (DropValue)
        {


            case 0:
                MapSizeString = "Small";
                mapsizeValue = 0;
                break;
            case 1:
                MapSizeString = "Medium";
                mapsizeValue = 1;
                break;

            case 2:
                MapSizeString = "Large";
                mapsizeValue = 2;
                break;


        }
        Debug.Log(MapSizeString);

    }

    public void BeginGameSinglePlayer()
    {
        fade = !fade;
        if (LoadScreenLoaded == false)
        {
            loadingPanel.SetActive(true);
        }
        else
        {
            GameSettings.ToggleOptions();
            StopCoroutine("Fade");
            Debug.Log("Loading is true");

            SceneManager.LoadScene(ScenesTags.SCENE_SINGLEPLAYER, LoadSceneMode.Single);
            SceneManager.sceneLoaded += OnSceneLoad;
        }
        Debug.Log("begin game called");

    }

    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        //Assign audioSource
        backgroundMusic = Camera.main.GetComponent<AudioSource>();



        if (scene.name ==  ScenesTags.SCENE_SINGLEPLAYER)
        {
            int mapS = GameObject.FindGameObjectWithTag(Tags.SCENECONTROLER_TAG).GetComponent<MapScript>().mapIntValue = mapsizeValue;
            GameObject.FindGameObjectWithTag(Tags.ENEMY_CONTROLLER_TAG).GetComponent<WaveManager>().MaxTimer = WaveTimer;

            backgroundMusic.clip = gameMusic1;
            backgroundMusic.volume = GameSettings.SoundMusic;

            backgroundMusic.Play();

        }
        else if (scene.name == ScenesTags.SCENE_MAINMENU)
        {

            loadingImage = GameObject.Find(Tags.FadeScreen).GetComponent<Image>();
            backgroundMusic.clip = menuMusic;
            backgroundMusic.Play();
            backgroundMusic.volume = GameSettings.SoundMusic;


            //Destroy(gameObject);

        }

        Debug.Log(scene.name + " was loaded");

        GameSettings.AssignObjects();
    }

    public void ReturnToMainMenu()
    {

        SceneManager.LoadScene(ScenesTags.SCENE_MAINMENU, LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoad;

        Destroy(gameObject);
    }




}

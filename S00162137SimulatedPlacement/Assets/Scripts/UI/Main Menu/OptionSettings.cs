using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionSettings : MonoBehaviour {

    [Header("Main Menu")]
    //Main Menu
    #region

    public GameObject OptionsBoard;
    //Buttons
    public Button optionBtn;
    public Button optionBtnApply;
    public Button optionBtnCancel;
    public Button optionBtnMainMenu;


    public Button btnLoopMusic;
    public Button btnFullscreen;


    #endregion

    [Header("Game")]
    //InGame
    #region
    public GameObject OptionsBoardGame;
    //Buttons
    public Button optionBtnGame;
    public Button optionBtnApplyGame;
    public Button optionBtnCancelGame;
    public Button optionBtnMainMenuGame;


    public Button btnLoopMusicGame;
    public Button btnFullscreenGame;

    #endregion



    //Sliders
    public Slider SFXSlider;
    public Slider musicSlider;
    public Slider AmbiantSlider;


    private bool OpenOptions = true;
    private bool GamePaused = false;

    [Header("Images")]
    public Sprite OnImage;
    public Sprite OffImage;

    [Header("Sounds")]
    public float SoundEffects = 1f;
    public float SoundAmbiant = 1f;
    public float SoundMusic = 1f;
    public bool loopMusic = false;
    public bool Windowed = false;

    public float StoreAgeEffects;
    public float StoreAgeAmbiant;
    public float StoreAgeMusic;


    private void Start()
    {
        AssignObjects();


    }

    private void Update()
    {
        //If not in main menu, pause
        if (SceneManager.GetActiveScene().name != ScenesTags.SCENE_MAINMENU)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleOptions();
            }
        }
    }


    //Find Buttons and Objects
    public void AssignObjects()
    {
        OpenOptions = true;
        OptionsBoard = GameObject.Find(Tags.OptionsDirOptionPanel);

       
        if (SceneManager.GetActiveScene().name == ScenesTags.SCENE_MAINMENU)
        {

            optionBtn = GameObject.Find("/Canvas/Button Panels/OptionMenu").GetComponent<Button>();
            optionBtn.onClick.AddListener(delegate { ToggleOptions(); });

            //
            #region
            optionBtnApply = GameObject.Find(Tags.OptionsDirApplyBTN).GetComponent<Button>();
            optionBtnApply.onClick.AddListener(delegate { AssignValues(); });


            optionBtnCancel = GameObject.Find(Tags.OptionsDirCancelBTN).GetComponent<Button>();
            optionBtnCancel.onClick.AddListener(delegate { CloseOptionMenu(); });



            btnLoopMusic = GameObject.Find("/Canvas/OptionsMenu/Panel/OptionsPanel/LoopMusicBTN/LoopMBTN").GetComponent<Button>();

            btnLoopMusic.onClick.AddListener(delegate { LoopMusic(); });


            btnFullscreen = GameObject.Find("/Canvas/OptionsMenu/Panel/OptionsPanel/toggleFullScreenBTN/FullScreenBTN").GetComponent<Button>();

            btnFullscreen.onClick.AddListener(delegate { ToggleFullScreen(); });


            //Sliders
            SFXSlider = GameObject.Find(Tags.OptionsDirSliderSFX).GetComponent<Slider>();
            SFXSlider.value = SoundEffects;
            SFXSlider.onValueChanged.AddListener(delegate { AdjustSound(); });

            musicSlider = GameObject.Find(Tags.OptionsDirSliderMusic).GetComponent<Slider>();
            musicSlider.value = SoundMusic;
            musicSlider.onValueChanged.AddListener(delegate { AdjustSound(); });

            AmbiantSlider = GameObject.Find(Tags.OptionsDirSliderAmbiant).GetComponent<Slider>();
            AmbiantSlider.value = SoundAmbiant;
            AmbiantSlider.onValueChanged.AddListener(delegate { AdjustSound(); });




            #endregion
        }

        else if (SceneManager.GetActiveScene().name == ScenesTags.SCENE_SINGLEPLAYER)
        {
            MapGen tempMapGen = GetComponent<MapGen>();

            //
            #region
            //Apply Options
            optionBtnApplyGame = GameObject.Find(Tags.OptionsDirApplyBTN).GetComponent<Button>();
            optionBtnApplyGame.onClick.AddListener(delegate { AssignValues(); });

            //Cancel Options
            optionBtnCancelGame = GameObject.Find(Tags.OptionsDirCancelBTN).GetComponent<Button>();
            optionBtnCancelGame.onClick.AddListener(delegate { CloseOptionMenu(); });


            //loopmusic
            btnLoopMusicGame = GameObject.Find("/Canvas/OptionsMenu/Panel/OptionsPanel/LoopMusicBTN/LoopMBTN").GetComponent<Button>();
            btnLoopMusicGame.onClick.AddListener(delegate { LoopMusic(); });

            //btnFullScreen
            btnFullscreenGame = GameObject.Find("/Canvas/OptionsMenu/Panel/OptionsPanel/toggleFullScreenBTN/FullScreenBTN").GetComponent<Button>();
            btnFullscreenGame.onClick.AddListener(delegate { ToggleFullScreen(); });

            optionBtnMainMenu = GameObject.Find(Tags.OptionsDirMainMenuBTN).GetComponent<Button>();
            optionBtnMainMenu.onClick.AddListener(delegate { GetComponent<MapGen>().ReturnToMainMenu(); });

            //Sliders
            SFXSlider = GameObject.Find(Tags.OptionsDirSliderSFX).GetComponent<Slider>();
            SFXSlider.value = SoundEffects;
            SFXSlider.onValueChanged.AddListener(delegate { AdjustSound(); });

            musicSlider = GameObject.Find(Tags.OptionsDirSliderMusic).GetComponent<Slider>();
            musicSlider.value = SoundMusic;
            musicSlider.onValueChanged.AddListener(delegate { AdjustSound(); });

            AmbiantSlider = GameObject.Find(Tags.OptionsDirSliderAmbiant).GetComponent<Slider>();
            AmbiantSlider.value = SoundAmbiant;
            AmbiantSlider.onValueChanged.AddListener(delegate { AdjustSound(); });


            #endregion

        }
        //Assign Buttons and they're call methods

        //optionBtnApply = GameObject.Find(Tags.OptionsDirApplyBTN).GetComponent<Button>();
        //optionBtnApply.onClick.AddListener(delegate { AssignValues(); });

        //optionBtnCancel = GameObject.Find(Tags.OptionsDirCancelBTN).GetComponent<Button>();
        //optionBtnCancel.onClick.AddListener(delegate { CloseOptionMenu(); });

        //btnLoopMusic = GameObject.Find("/Canvas/OptionsMenu/Panel/OptionsPanel/LoopMusicBTN/LoopMBTN").GetComponent<Button>();
        //btnLoopMusic.onClick.AddListener(delegate { LoopMusic(); });

        //btnFullscreen = GameObject.Find("/Canvas/OptionsMenu/Panel/OptionsPanel/toggleFullScreenBTN/FullScreenBTN").GetComponent<Button>();
        //btnFullscreen.onClick.AddListener(delegate { ToggleFullScreen(); });

        ////Sliders
        //SFXSlider = GameObject.Find(Tags.OptionsDirSliderSFX).GetComponent<Slider>();
        //SFXSlider.value = SoundEffects;
        //SFXSlider.onValueChanged.AddListener(delegate { AdjustSound(); });

        //musicSlider = GameObject.Find(Tags.OptionsDirSliderMusic).GetComponent<Slider>();
        //musicSlider.value = SoundMusic;
        //musicSlider.onValueChanged.AddListener(delegate { AdjustSound(); });

        //AmbiantSlider = GameObject.Find(Tags.OptionsDirSliderAmbiant).GetComponent<Slider>();
        //AmbiantSlider.value = SoundAmbiant;
        //AmbiantSlider.onValueChanged.AddListener(delegate { AdjustSound(); });



        //int tempi = 0;
        //foreach (GameObject goController in GameObject.FindGameObjectsWithTag(Tags.GAMECONTROLLER_TAG))
        //{
        //    tempi++;
        //    GameObject controller = goController;
        //    if (tempi >1)
        //    {
        //        Destroy(goController);
        //    }
        //}
        btnLoopMusic = GameObject.Find("/Canvas/OptionsMenu/Panel/OptionsPanel/LoopMusicBTN/LoopMBTN").GetComponent<Button>();

        btnLoopMusic.onClick.AddListener(delegate { LoopMusic(); });


        btnFullscreen = GameObject.Find("/Canvas/OptionsMenu/Panel/OptionsPanel/toggleFullScreenBTN/FullScreenBTN").GetComponent<Button>();

        btnFullscreen.onClick.AddListener(delegate { ToggleFullScreen(); });



        DontDestroyOnLoad(gameObject);

        ToggleOptions();

    }

   

    //ShowOptionMenu
    public void ToggleOptions()
    {
        OpenOptions = !OpenOptions;
        if (OpenOptions == true)
        {
            OptionsBoard.SetActive(true);
        }
        else
        {
            OptionsBoard.SetActive(false);
        }

    }

    private void CloseOptionMenu()
    {

    }

    //For FullScreenOrNot
    private void ToggleFullScreen()
    {
        Windowed = !Windowed;
        //set fullscreen

        if (Windowed == false)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            btnFullscreen.GetComponent<Image>().sprite = OffImage;

        }
        //set windowed
        else
        {
            //set to windowed
            Screen.fullScreenMode = FullScreenMode.Windowed;
            btnFullscreen.GetComponent<Image>().sprite = OnImage;

        }
    }

    private void LoopMusic()
    {
        loopMusic = !loopMusic;
        AudioSource bgMusic = Camera.main.GetComponent<AudioSource>();
        bgMusic.loop = loopMusic;
        if (loopMusic == true)
        {
            btnLoopMusic.GetComponent<Image>().sprite = OnImage ;
        }
        else
        {
            btnLoopMusic.GetComponent<Image>().sprite = OffImage;
        }

    }

    public void AssignValues()
    {


        SoundEffects = StoreAgeEffects;
        SoundAmbiant = StoreAgeAmbiant;
        SoundMusic = StoreAgeMusic;
        GetComponent<MapGen>().backgroundMusic.volume = SoundMusic;

        ToggleOptions();
    }

    public void AdjustSound()
    {


        StoreAgeEffects = SFXSlider.value;
        StoreAgeAmbiant = AmbiantSlider.value;
        StoreAgeMusic = musicSlider.value;

    }



}

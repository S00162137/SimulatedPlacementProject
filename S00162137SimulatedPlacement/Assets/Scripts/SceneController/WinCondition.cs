using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class WinCondition : MonoBehaviour {

    public GameObject EndPanel;


    public HovelScript hovel1;
    public bool hovelAlive1 = false;


    public HovelScript hovel2;
    public bool hovelAlive2 = false;

    //Victor Text
    public Text ResultText;



    //Player Sprites
    public Sprite P1Sprite;
    public Sprite P2Sprite;

    //Used to show who won or loss
    private Vector3 WinRotation;
    private Vector3 LoseRotation;

    //UI Sprites
    public Image Player1Image;
    public Image Player2Image;

    public Button returnBtn;
    public Button exitBtn;

    private void Start()
    {
        exitBtn = GameObject.Find("/Canvas/EndScreen/ResultImage/ExitBTN").GetComponent<Button>();
        returnBtn = GameObject.Find("/Canvas/EndScreen/ResultImage/mainMenuBtn").GetComponent<Button>();

        EndPanel = GameObject.Find(Tags.DirEndScreen);

        //Gets the image from Child Objects
        ResultText = EndPanel.gameObject.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
        Player1Image = EndPanel.gameObject.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>();
        Player2Image = EndPanel.gameObject.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Image>();


        returnBtn.onClick.AddListener(delegate { GameObject.Find("GameController").GetComponent<MapGen>().ReturnToMainMenu(); });

        exitBtn.onClick.AddListener(delegate { QuitGame(); });





        WinRotation = new Vector3(0,0,0);
        LoseRotation = new Vector3(0,0,-90);

        EndPanel.SetActive(false);

    }

    public void DetermineWin(int townhallPlayer)
    {
        //If player 1 dies do:
        EndPanel.SetActive(true);


        Player1Image.sprite = P1Sprite;
        Player2Image.sprite = P2Sprite;



        if (townhallPlayer == 1)
        {
            //Winner is enemy
            ResultText.text = "Defeat!";
            Player1Image.transform.rotation = Quaternion.Euler(0,0,-90);
            Player2Image.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else
        {
            //Winner is enemy
            ResultText.text = "Victory!";
            Player1Image.transform.rotation = Quaternion.Euler(0, 0, 0);
            Player2Image.transform.rotation = Quaternion.Euler(0, 0, -90);

        }

        // else if player 2, do:

    }


    public void ContinueToMainMenu(bool Exit)
    {
        if (Exit == true)
        {

        }
        else
        {
            //return to main menu
        }

    }

    public void AssignHovel(GameObject hovel)
    {
        HovelScript tempHovelScript = hovel.GetComponent<HovelScript>();

        if (hovel1 ==  null)
        {
            hovel1 = tempHovelScript;
            hovelAlive1 = true;
        }
        else
        {
            hovel2 = tempHovelScript;
            hovelAlive2 = true;

        }


    }


    public void HovelDestroyed(GameObject hovel)
    {
        //1 Died
        if (hovel.GetComponent<HovelScript>() == hovel1)
        {
            hovelAlive1 = false;
        }

        //2Died
        else if (hovel.GetComponent<HovelScript>() == hovel2)
        {
            hovelAlive2 = false;

        }

        //Both destroyed
        if (hovelAlive1 == false && hovelAlive2 == false)
        {
            DetermineWin(2);
        }
    }

 

    public void QuitGame()
    {



        Application.Quit();
    }
}

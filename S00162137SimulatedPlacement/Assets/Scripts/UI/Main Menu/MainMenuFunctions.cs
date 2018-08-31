using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuFunctions : MonoBehaviour {

    public GameObject SinglePlayerPanel;
    public GameObject MultiplayerPanel;


    private void Start()
    {
        SinglePlayerPanel.SetActive(false);


    }

    public void OpenPanel(int PanelPage)
    {

        switch (PanelPage)
        {
            case 0:
                SinglePlayerPanel.SetActive(true);
                break;

            case 1:
                MultiplayerPanel.SetActive(true);
                break;


            default:
                break;

        }


    }

    private void SinglePlayer()
    {

    }



    public void ClosePanel()
    {
        SinglePlayerPanel.SetActive(false);



    }

    public void QuitGame()
    {

     //   UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIBUTTONSWAP : MonoBehaviour {

    public Button SelectedButton;

    private int counterTest;
    private bool press1 = true;

    void Start () {

        //Code to Call a method on button click
        SelectedButton.onClick.AddListener(delegate { Test(SelectedButton.name); });
    }
	
	void Update () {
   
    }

    public void Test(string ok)
    {
        counterTest++;
        Debug.Log(counterTest);
    }

    public void Test2(string ok)
    {
        SelectedButton.onClick.AddListener(delegate { Test(SelectedButton.name); });

        Debug.Log(press1);
    }


}

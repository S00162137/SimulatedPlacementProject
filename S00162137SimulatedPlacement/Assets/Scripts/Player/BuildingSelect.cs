using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelect : MonoBehaviour {


    private Camera cameraMain;
    private GameObject selectedBuilding;

	// Use this for initialization
	void Start () {
        cameraMain = Camera.main;


	}
	
	// Update is called once per frame
	void Update () {



        if (Input.GetMouseButtonDown(0))
        {

            //Get the mouse position on the screen and send a raycast into the game world from that position.
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            //If something was hit, the RaycastHit2D.collider will not be null.
            if (hit.collider != null)
            {
                if (hit.collider.tag == Tags.BUILDING_TAG || hit.collider.tag == Tags.TOWNHALL_TAG)
                {
                    Debug.Log(hit.collider.name);

                }

            }


        }




    }



}

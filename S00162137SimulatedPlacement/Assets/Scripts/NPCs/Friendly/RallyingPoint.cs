using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RallyingPoint : MonoBehaviour {


    private Vector3 mousePos;
    public float setY;
    public float setZ;

    public bool canMove = false;

	void Start () {


	}
	
	void Update () {
        if (canMove == true)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = new Vector3(mousePos.x, setY, setZ);
        }
	}

    private void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (canMove ==  true)
            {
                gameObject.transform.position = new Vector3(mousePos.x, setY, setZ);
            }
            canMove = !canMove;


        }


    }

  
}

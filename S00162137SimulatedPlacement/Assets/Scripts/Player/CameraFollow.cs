using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject playerObj;
    private PlayerStats pstats;
    public Vector3 offSet;

    [SerializeField]
    private SpriteRenderer sRenderer;
    public Sprite background;
    public Sprite undergroundbackground;
    private bool Underground = false;


	// Use this for initialization
	void Start () {
        playerObj = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        pstats = playerObj.GetComponent<PlayerStats>();
        sRenderer = GetComponentInChildren<SpriteRenderer>();

        
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = playerObj.transform.position + offSet; // Follow Player]


        if (pstats.IsAlive == true)
        {

            if (playerObj.transform.position.y > -2f)
            {
                transform.position = new Vector3(playerObj.transform.position.x, 1f, -20);

            }
            else
            {
                transform.position = new Vector3(playerObj.transform.position.x, -28f, -20f);

            }


        }

        //Free move camwera
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                gameObject.transform.Translate(new Vector3(-0.3f, 0, 0) * pstats.moveSpeed);

            }
            else if (Input.GetKey(KeyCode.D))
            {
                gameObject.transform.Translate(new Vector3(0.3f, 0, 0) * pstats.moveSpeed);

            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.position = new Vector3(transform.position.x, 1f, -20);
                sRenderer.sprite = background;

            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                transform.position = new Vector3(transform.position.x, -28f, -20);
                sRenderer.sprite = undergroundbackground;

            }

        }




    }

    public void ChangeBackground()
    {
        Underground = !Underground;
        if (Underground == true)
        {
            sRenderer.sprite = undergroundbackground;
        }
        else
        {
            sRenderer.sprite = background;
        }

        Debug.Log(Underground);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour {

    [SerializeField]
    private Button Moveleft;
    [SerializeField]
    private Button MoveRight;
    [SerializeField]
    private Button MoveLevel;

    public bool canMove = false;
    public float moveValue = 0f;

    // 0 , -28
    private float AboveGroundPosY = 3f;
    private float UnderGroundPosY = -26f;

    private float multiplier = 5.28f;
    private int TilesAmount;


    //To clamp camera into the area
    public float LeftSideBounds;
    public float RightSideBounds;
    public float scrollSpeed = 5f;

    public Vector2 AboveGroundPos;
    public Vector2 UnderGroundPos;
    public bool ShowAbove = false;

    private SpriteRenderer BGSpriteRender;
    public Sprite AboveBGSprite;
    public Sprite UnderBGSprite;

	// Use this for initialization
	void Start () {
        BGSpriteRender = GetComponentInChildren<SpriteRenderer>();

        Moveleft = GameObject.Find(Tags.DirMoveLeftBTN).GetComponent<Button>();
       // MoveRight = GameObject.Find(Tags.DirMoveRightBTN).GetComponent<Button>();
        MoveLevel = GameObject.Find(Tags.DirMoveLevelBTN).GetComponent<Button>();

        //Call method
        // Moveleft.onClick.AddListener(delegate { MoveXAxis(0); });
        // Moveleft.onClick.AddListener(delegate { MoveXAxis(0); });
     //   MoveRight.onClick.AddListener(delegate { MoveXAxis(scrollSpeed); });
        MoveLevel.onClick.AddListener(delegate { MoveMinimapCamera(); });


        TilesAmount = GameObject.FindGameObjectWithTag(Tags.SCENECONTROLER_TAG).GetComponent<MapScript>().numberOfTiles;
        LeftSideBounds = transform.position.x;
        RightSideBounds = (float)(TilesAmount * multiplier) - 10;

        MoveMinimapCamera();
    }

    private void Update()
    {
        if (canMove ==true)
        {
            //Move right
            if (moveValue > 0)
            {
                if (transform.position.x < RightSideBounds)
                {
                    transform.position = new Vector3(transform.position.x + scrollSpeed / 100, transform.position.y, -20f);

                }
            }
            //move left

            else if(moveValue <0)
            {
                if (transform.position.x > LeftSideBounds)
                {
                    transform.position = new Vector3(transform.position.x - scrollSpeed / 100, transform.position.y, -20f);

                }
            }

        }
    }



    public void EnableMove()
    {

    }


    public void MoveMinimapCamera()
    {

        ShowAbove = !ShowAbove;
        if (ShowAbove == true)
        {
            gameObject.transform.position = new Vector3(transform.position.x, AboveGroundPosY, -20f);
            BGSpriteRender.sprite = AboveBGSprite;
        }
        else
        {
            gameObject.transform.position = new Vector3(transform.position.x, UnderGroundPosY, -20f);
            BGSpriteRender.sprite = UnderBGSprite;

        }


    }

    public void MoveXAxis(float dirVal)
    {
        canMove = !canMove;

        moveValue = dirVal;
    }

    

}

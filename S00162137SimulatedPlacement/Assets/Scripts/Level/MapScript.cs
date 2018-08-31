using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour {

    #region MapSize Variables
    //Tile Position Variables
    private Vector3 tileStartPos;
    private float tilePosDist = 5.28f;
    private Vector3 tilePosition;
    private enum mapSize { Small, Medium, Large };
    public int mapIntValue;
    public int numberOfTiles;

    public Minimap minimap;

    [Header("Aboveground Variables")]

    //Boundary Variables
    [SerializeField]
    private GameObject AboveBorder;
    private Vector3 aboveBorderPos;
    private Vector3 aboveEndBorderPos;


    //Tile Prefabs
    public GameObject groundTile;
    public GameObject treeObj;
    public GameObject groundTileEntrance;

    private int TileChanceIncrease;

    [Header("Underground Variables")]

    //Underground tiles 
    public GameObject underGroundTile;
    public GameObject undergroundTileEntrance;

    [SerializeField]
    private GameObject UnderBorder;
    private Vector3 underBorderPos;
    private Vector3 underEndBorderPos;

    private Vector3 undertileStartPos;
    private float undertilePosDist = 5.28f;
    private Vector3 undertilePosition;


    [Header("Underground Entrane Points")]
    public int entrancesTilePoint1;
    public int entrancesTilePoint2;
    public int entrancesTilePoint3;
    public int entrancesTilePoint4;

    [Header("Neutral Camps")]
    public GameObject gibblerHovel;
    public GameObject wolfDen;
    public GameObject TribalHutt;


    [Header("Map size elements")]
    [SerializeField]
    private mapSize mapLength;
    private int endMapTile;
    private int centerMapTile;


    #endregion
    //For targeting
    public GameObject EndMapTile;
    public GameObject CenterMapTileAbove;
    public GameObject CenterMapTileUnder;



    [Header("MapSpecials")]
    private GameObject AboveSpecial;
    private GameObject UnderSpecial;

    [Header("Object Prefabs")]
    #region ObjectSetup
    public GameObject TownhallPrefab;
    public GameObject WoodCuttersPrefab;
    public GameObject FarmHousePrefab;

    public GameObject AboveFoodObj;
    public GameObject UnderFoodObj;
    public GameObject TreesPrefab;

    
    public GameObject smallrockVeinPrefab;
    public GameObject bigrockVeinPrefab;


    public GameObject PlayerObj;
    #endregion




    // Use this for initialization
    void Start () {


        tilePosition = new Vector3(tilePosition.x, -0.54f, tilePosition.z);
 
        aboveBorderPos = new Vector3(tilePosition.x - (groundTile.transform.localScale.x ),tilePosition.y, tilePosition.z -3f );


        undertilePosition = new Vector3(undertilePosition.x, -30f, undertilePosition.z);

        underBorderPos = new Vector3(undertilePosition.x - (underGroundTile.transform.localScale.x), undertilePosition.y, undertilePosition.z - 3f );




        //Applies MapSize based on Int value
        mapLength = (mapSize)mapIntValue;

        DetermineMapSize();



    }
	

    //Determine map size, points and special locations
    public void DetermineMapSize()
    {
        switch (mapLength)
        {
            case mapSize.Small:
                entrancesTilePoint1 = 5;
                entrancesTilePoint2 = 10;
                entrancesTilePoint3 = 15;
                entrancesTilePoint4 = 20;

                centerMapTile = 13;
                numberOfTiles = 25;

                GenerateMapSize(25);
                break;


            case mapSize.Medium:

                entrancesTilePoint1 = 5;
                entrancesTilePoint2 = 15;
                entrancesTilePoint3 = 35;
                entrancesTilePoint4 = 45;

                centerMapTile = 25;
                numberOfTiles = 50;

                GenerateMapSize(50);

                break;


            case mapSize.Large:

                entrancesTilePoint1 = 5;
                entrancesTilePoint2 = 20;
                entrancesTilePoint3 = 50;
                entrancesTilePoint4 = 70;

                centerMapTile = 35;
                numberOfTiles = 75;
                GenerateMapSize(75);

                break;


            
                
        }
    }

    //Set map size
    public void GenerateMapSize(int length)
    {
        endMapTile = length;
        //make map
        for (int i = 0; i <= length; i++)
        {
            ChooseTileType(i);
            tilePosition.x +=  tilePosDist;
            TileChanceIncrease += 5;
        }

        //Make underground
        for (int i = 0; i <= length; i++)
        {
            MakeUnderground(i);
            undertilePosition.x += tilePosDist;
        }

    }

    //AboveGround
    public void ChooseTileType(int Counter)
    {
        #region Players

        //Player1 Position
        if (Counter <= 4)
        {
            Instantiate(groundTile, tilePosition, gameObject.transform.rotation);
            //Spawn TownHall
            if (Counter ==0)
            {
                Instantiate(AboveBorder, new Vector2(tilePosition.x - 3.5f, tilePosition.y), AboveBorder.transform.rotation);
                Instantiate(FarmHousePrefab, new Vector3(tilePosition.x, tilePosition.y + FarmHousePrefab.transform.position.y + 0.5f, 1), FarmHousePrefab.transform.rotation);

            }
            if (Counter == 1) // Spawn TownHall
            {
                Instantiate(TownhallPrefab, tilePosition + TownhallPrefab.transform.position, TownhallPrefab.transform.rotation);

            }
            if (Counter == 3) //Spawn WoodCutters
            {
                Instantiate(WoodCuttersPrefab, tilePosition + WoodCuttersPrefab.transform.position, WoodCuttersPrefab.transform.rotation);

            }
            if (Counter == 4)
            {
              //  Instantiate(groundTileTree, tilePosition, gameObject.transform.rotation);

            }
        }

        //Player2 Position
        else if (Counter >= (endMapTile -3))
        {
            Instantiate(groundTile, tilePosition, gameObject.transform.rotation);

            //Spawn TownHall / Base
             if (Counter == (endMapTile - 1))
            {
                Instantiate(gibblerHovel, new Vector3(tilePosition.x, tilePosition.y + 0.28f, -1f), gameObject.transform.rotation);


            }

            //Border
            else if (Counter == endMapTile)
            {
                Instantiate(AboveBorder, new Vector2(tilePosition.x + 7.5f, tilePosition.y), AboveBorder.transform.rotation);
            }
        }


        #endregion

        //Spawn Tile Types
        else if (Counter > 4 && Counter < (endMapTile - 3))
        {

            //If counter is on entrance points
            if (Counter == entrancesTilePoint1 || Counter == entrancesTilePoint2 || Counter == entrancesTilePoint3 || Counter == entrancesTilePoint4 || Counter == centerMapTile)
            {
                if (Counter == centerMapTile)
                {

                    //Instansiate map special
                    Instantiate(groundTile, tilePosition, gameObject.transform.rotation);
                    CenterMapTileAbove = groundTile;
                    //minimap.AboveGroundPos = CenterMapTileAbove.transform.position;


                }
                else
                {
                    Instantiate(groundTileEntrance, tilePosition, gameObject.transform.rotation);

                }
            }
            else
            {
                Instantiate(groundTile, tilePosition, gameObject.transform.rotation);



                int TileObj = Random.Range(0,3);
                if (TileObj == 0)
                {
                    Instantiate(treeObj, new Vector3(tilePosition.x, tilePosition.y + 0.6f, 1f), gameObject.transform.rotation);
                }
                else if(TileObj == 1)
                {
                    Instantiate(AboveFoodObj, new Vector3(tilePosition.x, tilePosition.y - 0.25f, 1f), gameObject.transform.rotation);
                }
                else
                {

                }




                //if (TileChanceIncrease >= 15)
                //{
                //    //Spawn Trees

                //    //Instantiate(groundTileStone, tilePosition, gameObject.transform.rotation);
                //    Instantiate(treeObj, new Vector3(tilePosition.x, tilePosition.y +0.6f, 1f), gameObject.transform.rotation);


                //    //Spawn Stones

                //    //Reset Chance
                //    TileChanceIncrease = 0;
                //}
                //else
                //{
                //    Instantiate(groundTile, tilePosition, gameObject.transform.rotation);

                //}


                TileChanceIncrease += 5;

            }


        }


        //Find Above Tile Center, used for miniMap
        int TileCounter = 0;
        foreach (GameObject tileObject in GameObject.FindGameObjectsWithTag(Tags.GROUND_TAG))
        {
            TileCounter++;
            if (tileObject.transform.position.y > -5f)
            {
                if (TileCounter == centerMapTile)
                {
                    CenterMapTileAbove = tileObject;
                }
            }


        }
        //Player 2 / Enemy AI
        //else if (Counter >= endMapTile -4)
        //{
        //    if (Counter == endMapTile -1)
        //    {
        //    }
        //}


    }

    //Underground
    public void MakeUnderground(int counter)
    {

        if (counter > 4 && counter < (endMapTile - 4))
        {

            if (counter == entrancesTilePoint1 || counter == entrancesTilePoint2 || counter == entrancesTilePoint3 || counter == entrancesTilePoint4 || counter ==  centerMapTile)
            {
                //Instantiate(underGroundTile, undertilePosition, gameObject.transform.rotation);

                if (counter == centerMapTile)
                {
                    Instantiate(underGroundTile, undertilePosition, gameObject.transform.rotation);
                    CenterMapTileUnder = underGroundTile;
                    //minimap.UnderGroundPos = CenterMapTileUnder.transform.position;

                    //Assigns Positions
                    Instantiate(UnderBorder, new Vector3(undertilePosDist* centerMapTile, undertilePosition.y, 1), UnderBorder.transform.rotation); // makwe border wall
                    Debug.Log(counter);




                }

                else
                {
                    Instantiate(undergroundTileEntrance, undertilePosition, gameObject.transform.rotation);
                    if (counter == centerMapTile)
                    {
                    }
                }
            }

            else // spawn normal tile
            {
                Instantiate(underGroundTile, undertilePosition, gameObject.transform.rotation);
                int r = Random.Range(0, 30);
                if (r >= 28)
                {
                    Instantiate(bigrockVeinPrefab, new Vector2(undertilePosition.x, undertilePosition.y + (bigrockVeinPrefab.transform.localScale.y / 4)), bigrockVeinPrefab.transform.rotation);
                }
                else
                {
                    if (r > 15 && r < 28)
                    {
                        Instantiate(smallrockVeinPrefab,
                            new Vector2(undertilePosition.x, undertilePosition.y - 0.2f),
                                    smallrockVeinPrefab.transform.rotation);

                    }

                    else if (r > 8 && r <= 15)
                    {
                        Instantiate(UnderFoodObj,
                         new Vector2(undertilePosition.x, undertilePosition.y - 0.4f),
                        UnderFoodObj.transform.rotation);

                    }
                }
            }

        }


        else
        {
            Instantiate(underGroundTile, undertilePosition, gameObject.transform.rotation);
            //Spawn Garanteed Gibbler hovel
            if (counter == (endMapTile - 1))
            {
                Instantiate(gibblerHovel, new Vector3(undertilePosition.x, undertilePosition.y + 0.28f, -1f), gibblerHovel.transform.rotation);
            }
            //Make Borders
            else if (counter == 0 || counter ==endMapTile)
            {
                if (counter == 0)
                {
                    Instantiate(UnderBorder, new Vector2(undertilePosition.x - 3.5f, undertilePosition.y), UnderBorder.transform.rotation); // makwe border wall

                }
                else
                {
                    Instantiate(UnderBorder, new Vector2(undertilePosition.x + 7.5f, undertilePosition.y), UnderBorder.transform.rotation); // makwe border wall

                }
                Instantiate(bigrockVeinPrefab, new Vector2(undertilePosition.x, undertilePosition.y + 0.5f), bigrockVeinPrefab.transform.rotation);

            }



        }


        //Find Below Tile Center, used for miniMap
        int TileCounter = 0;
        foreach (GameObject tileObject in GameObject.FindGameObjectsWithTag(Tags.GROUND_TAG))
        {
            TileCounter++;
            if (tileObject.transform.position.y <-10f)
            {
                if (TileCounter == centerMapTile)
                {
                    CenterMapTileUnder = tileObject;
                    Instantiate(UnderBorder, new Vector2(CenterMapTileUnder.transform.position.x, undertilePosition.y), UnderBorder.transform.rotation); // makwe border wall

                }
            }


        }


    }


}

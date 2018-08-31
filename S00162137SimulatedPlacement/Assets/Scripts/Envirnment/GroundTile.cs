using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour {

    public GameObject TileObj;
    public Transform BuildPoint;

    public enum TileType { Above, Under, Both };
    public TileType tType;
    public bool hasItem;

    // Use this for initialization
    void Start () {

        if (TileObj != null)
        {
            hasItem = true;
        }

        if (tType == TileType.Above)
        {
           
        }
        else
        {

        }
	}
	

    //Add Code for when player clears the Tile IE + Wood / + Stone
    public void TileCleared()
    {
        Destroy(TileObj);
        hasItem = false;

    }


}

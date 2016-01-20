using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    public List<Tile> openList;
    List<Tile> closeList;

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        //calculate Manhattan Distance
        foreach(Tile tile in openList)
        {
            if(tile.targetPos.x != tile.transform.position.x)
            {
                tile.heuristicValue += Mathf.Abs(tile.targetPos.x - tile.transform.position.x);
            }
            if(tile.targetPos.y != tile.transform.position.y)
            {
                tile.heuristicValue += Mathf.Abs(tile.targetPos.y - tile.transform.position.y);
            }
            else
            {
                tile.heuristicValue = 0;
            }
            
        }
	}
}

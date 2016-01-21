using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

	Tile[] tiles;
	public GameObject tile;

	public List<Tile> gameBoard = new List<Tile>();
	List<Tile> closeList;

	int tileId;

	public Transform text;

	int puzzleType = 8;

	public float h;

	void Start() 
	{
		// intialize the board to the end position
		tile.GetComponent<Tile>().id = 0;
		text = tile.transform.FindChild("Text");
		for (int j = 2; j >= 0; --j)
		{
			for (int i = 0; i < 3; ++i)
			{
				// store the target location of each tile
				tile.GetComponent<Tile>().targetPos.x = i;
				tile.GetComponent<Tile>().targetPos.y = j;

				// set the text on the tiles and instantiate
				tile.GetComponent<Tile>().id++;
				tileId = tile.GetComponent<Tile>().id;
				text.GetComponent<Text>().text = tileId.ToString();
				if (tileId <= puzzleType)
				{
					Instantiate(tile, tile.GetComponent<Tile>().targetPos, Quaternion.identity);
					gameBoard.Add(tile.GetComponent<Tile>());
					
				}
				
			}
		}		
	}

	// Update is called once per frame
	void Update()
	{
		foreach (Tile tile in gameBoard)
		{
			// get the heuristic value for the individual tiles
			CalculateManhattanDistance(tile);

			// store the overall heuristic value for the current board
			h += tile.heuristicValue;
		}
	}

	void CalculateManhattanDistance(Tile tile)
	{
		// check how far away from the target location the tile is
		if (tile.currentPos.x != tile.targetPos.x)
		{
			// set the tiles heuristic value
			tile.heuristicValue += Mathf.Abs(tile.targetPos.x - tile.currentPos.x);
		}
		if (tile.currentPos.y != tile.targetPos.y)
		{
			tile.heuristicValue += Mathf.Abs(tile.targetPos.y - tile.currentPos.y);
		}
		else
		{
			tile.heuristicValue = 0;
		}
	}
}

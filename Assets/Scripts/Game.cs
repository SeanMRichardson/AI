﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    Tile[] tiles;
    public GameObject tile;

    public GameObject obj;

    public List<Tile> gameBoard = new List<Tile>();
    List<Tile> closeList;

    int tileId;

    public Transform text;

    int puzzleType = 8;

    public float h;

    bool randomizing = false;

    void Start()
    {
        // intialize the board to the end position
        tile.GetComponent<Tile>().id = 1;
        text = tile.transform.FindChild("Text");
        for (int j = 3; j >= 1; --j)
        {
            for (int i = 0; i < 3; ++i)
            {
                // store the target location of each tile
                tile.GetComponent<Tile>().targetPos.x = i;
                tile.GetComponent<Tile>().targetPos.y = j;

                tileId = tile.GetComponent<Tile>().id;
                // set the text on the tiles and instantiate
                if (tileId <= puzzleType)
                {
                    text.GetComponent<Text>().text = tileId.ToString();

                    // instantiate a gameobject to the list
                    obj = Instantiate(tile, tile.GetComponent<Tile>().targetPos, Quaternion.identity) as GameObject;
                    gameBoard.Add(obj.GetComponent<Tile>());
                    tile.GetComponent<Tile>().id++;
                }
            }
        }
        RandomizeBoard();
    }

    void Update()
    {

        foreach (Tile tile in gameBoard)
        {
            h = 0;
            // get the heuristic value for the individual tiles
            CalculateManhattanDistance(tile);

            // store the overall heuristic value for the current board
            h += tile.heuristicValue;
        }
    }

    void CalculateManhattanDistance(Tile tile)
    {
        float hValue = 0;
        // check how far away from the target location the tile is
        if (tile.currentPos.x != tile.targetPos.x)
        {
            // set the tiles heuristic value
            hValue += Mathf.Abs(tile.targetPos.x - tile.currentPos.x);
            tile.heuristicValue = hValue;
        }
        if (tile.currentPos.y != tile.targetPos.y)
        {
            hValue += Mathf.Abs(tile.targetPos.y - tile.currentPos.y);
            tile.heuristicValue = hValue;
        }
        if (tile.currentPos == tile.targetPos)
        {
            tile.heuristicValue = 0;
        }
    }

    void RandomizeBoard()
    {
        randomizing = true;
        Vector2[] randomPositions = new Vector2[8];
        List<Vector2> possiblePositions = new List<Vector2>(9);


        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                possiblePositions.Add(new Vector2(i, j));
            }
        }

        for (int i = 0; i < 9; i++)
        {
            int x = Random.Range(0, possiblePositions.Count);
            randomPositions[i] = possiblePositions[x];
            possiblePositions.RemoveAt(x);
            gameBoard[i].transform.position = randomPositions[i];
        }

        // populate array with random positions
        // check to see if any are the same
        // if they are change the second one

        //add a random position to an array
        //generate a new random position
        //check if the positions are the same as any in the list
        // if so generate a new position

        
    }
}
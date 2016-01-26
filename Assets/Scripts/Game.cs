using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject tile;
    public float h;
    public GameObject obj;
    public List<Tile> gameBoard = new List<Tile>();
    List<Tile> closeList;
    public Transform text;
    public Text hText;

    int tileId;
    int puzzleType = 8;

    void Start()
    {
        // intialize the board to the end position
        tile.GetComponent<Tile>().id = 1;
        text = tile.transform.FindChild("Text");
        for (int j = 2; j >= 0; --j)
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
        //RandomizeBoard();
    }

    void Update()
    {
        h = 0;
        foreach (Tile tile in gameBoard)
        {
            // get the heuristic value for the individual tiles
            CalculateManhattanDistance(tile);

            // store the overall heuristic value for the current board
            h += tile.heuristicValue;
            hText.text = "Manhattan Distance = " + h;
        }
    }

    /// <summary>
    /// calculates the manhattan distance heuristic
    /// </summary>
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
            // set the heuristic value
            hValue += Mathf.Abs(tile.targetPos.y - tile.currentPos.y);
            tile.heuristicValue = hValue;
        }

        // if the tiles do match reset the heuristic value
        if (tile.currentPos == tile.targetPos)
        {
            tile.heuristicValue = 0;
        }
    }

    /// <summary>
    /// A function which can randomize the position of a tile on a gameboard
    /// </summary>
    public void RandomizeBoard()
    {
        // store the possible positions on the board
        List<Vector2> possiblePositions = new List<Vector2>(9);

        // store the random positions on the board
        Vector2[] randomPositions = new Vector2[8];
        
        // add all the possible positions to a list
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                possiblePositions.Add(new Vector2(i, j));
            }
        }

        // loop through the possible positions
        for (int i = 0; i < 9; i++)
        {
            //pick a random index
            int x = Random.Range(0, possiblePositions.Count);

            // add the random position to the array
            randomPositions[i] = possiblePositions[x];

            // remove the possible position
            possiblePositions.RemoveAt(x);

            // change position of tile on the board
            gameBoard[i].transform.position = randomPositions[i];
        }       
    }
}
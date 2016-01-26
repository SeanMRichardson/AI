﻿using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour 
{
	public Vector2 targetPos;
	public Vector2 currentPos;

	public int id = 0;

	public float heuristicValue;

	public bool canMove;

	void Update()
	{
		currentPos = transform.position;
	}
}

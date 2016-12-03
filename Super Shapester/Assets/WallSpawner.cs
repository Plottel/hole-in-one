﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallSpawner : MonoBehaviour {
	public GameObject newWall;
	private List<GameObject> walls;
	public Transform WALL_SPAWN_POS;
	public const int SPAWN_INTERVAL = 3;
	private float lastSpawnTime;

	// Use this for initialization
	void Start () 
	{
		walls = new List<GameObject> ();
		StartCoroutine (Spawn ()); 
		lastSpawnTime = 0;
		WALL_SPAWN_POS.position = new Vector3 (0, 1, 150);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (ReadyToSpawn ()) 
		{
			lastSpawnTime = Time.realtimeSinceStartup;
			walls.Add((GameObject)Instantiate (newWall, WALL_SPAWN_POS.position, Quaternion.identity));
		}

		foreach (GameObject wall in walls) 
		{
			if (ReachedCamera (wall))
			{
				walls.Remove(wall);
				Destroy (wall);
			}
		}

	}

	private IEnumerator Spawn()
	{
		while (true) 
		{
			yield return new WaitForSeconds (SPAWN_INTERVAL);
		}
	}

	private bool ReachedCamera(GameObject wall)
	{
		return wall.gameObject.transform.position.z <= 0;
	}

	private bool ReadyToSpawn()
	{
		Debug.Log (Time.realtimeSinceStartup);
		return Time.realtimeSinceStartup - lastSpawnTime >= SPAWN_INTERVAL;
	}
}
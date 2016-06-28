﻿/*
Description: A chunk generates a multitude of game objects within it's bounds
in which the player moves through. All prefabs shoulds be generated here.
Created: 6/26/16
*/

using UnityEngine;
using System.Collections;

public class Chunk
{
    //Width of a chunk in Unity units. A larger chunk will load more objects at once.
    public const int chunkWidth = 28;
    //Center position of chunk.
    public Vector2 position;
    //Identification number of chunk.
    public int chunkNumber;

    /*
    gameObject ArrayLists. Any gameObjects of the same type should be 
    stored in their respective ArrayLists here so that they can
    be conveniently unloaded. Add any needed.
    */
    private ArrayList _groundObstacles = new ArrayList();
    //Chance of a ground obstacle spawning on a unit, (0f - 1f).
    public float groundObstacleSpawnChance = .10f;

    //Constructor.
    public Chunk(Vector2 position, int chunkNumber)
    {
        this.position = position;
        this.chunkNumber = chunkNumber;

        Load();

    }

    //Any objects in the chunk should be spawned in Load().
    private void Load()
    {

        /*
        A for loop with an index representing each unit within the chunk as
        iterator generates forward.
        */
        Vector2 indexPosition = new Vector2(position.x - chunkWidth / 2, 0);
        for (int i = 0; i < chunkWidth; i++)
        {
            indexPosition.x++;
           
            /*
            Any objects spawning should have there own copy of the if statement below
            configured to their needs.
            */
            if (Mathf.Round(Random.Range(0, chunkWidth)) <= (chunkWidth * groundObstacleSpawnChance))
            {
                //Creates a gameObject of a prefab from within the Resources folder at indexPosition.
                GameObject groundObstacle = Object.Instantiate(Resources.Load("Ground_Obstacle"), 
                    indexPosition, Quaternion.Euler(0, 0, 0)) as GameObject;

                //Just moves it up half the prefab's height.
                groundObstacle.transform.position = new Vector2(groundObstacle.transform.position.x, 
                    groundObstacle.transform.position.y + groundObstacle.GetComponent<Renderer>().bounds.size.y/2);
                _groundObstacles.Add(groundObstacle);
            }

            
        }

        Debug.Log("Chunk #" + chunkNumber + " loaded.");
    }

    //Any objects loaded in this chunk should be despawned in Unload().
    public void Unload()
    {
        //For each object in _groundObstacles.
        for (int i = 0; i < _groundObstacles.Count; i++)
        {
            //Remove it from scene.
            Object.Destroy((GameObject)_groundObstacles[i]);

            //Remove it from array.
            _groundObstacles.Remove(((GameObject)_groundObstacles[i]));
            //Remove from i so that we don't skip objects in the ArrayList.
            i--;
        }

        Debug.Log("Chunk #" + chunkNumber + " unloaded.");
    }

}

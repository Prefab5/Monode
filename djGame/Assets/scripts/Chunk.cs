/*
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
    be conveniently unloaded.
    */

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
        Debug.Log("Chunk #" + chunkNumber + " loaded.");
    }

    //Any objects loaded in this chunk should be despawned in Unload().
    public void Unload()
    {
        Debug.Log("Chunk #" + chunkNumber + " unloaded.");
    }

}

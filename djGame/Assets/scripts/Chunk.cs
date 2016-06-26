/*
Description: A chunk generates a multitude of game objects within it's bounds
in which the player moves through. All prefabs shoulds be generated here.
*/

using UnityEngine;
using System.Collections;

public class Chunk {
    public const int chunkWidth = 24;
    public Vector2 position;
    public int chunkNumber;

	public Chunk(Vector2 position, int chunkNumber)
    {
        this.position = position;
        this.chunkNumber = chunkNumber;
        Debug.Log("Chunk #" + chunkNumber + " spawned. Position:(" + position.x + ", " + position.y + ")");

    }

    public void Remove()
    {
        Debug.Log("Chunk #" + chunkNumber + " deleted.");
    }

}

/*
Description: Handles creating and deleting _chunks. _chunks are created and deleted when they 
are certain distances from the main camera. _chunks are controlled areas of game objects.
Created: 6/26/2016
*/

using UnityEngine;
using System.Collections;

public class ChunkController : MonoBehaviour
{

    private static Vector2 _initialCameraPosition;
    private Vector2 _currentCameraPosition;
    private static int _chunkNumber;
    private ArrayList _chunks = new ArrayList();
    public bool debug = false;


    void Start()
    {
        _initialCameraPosition = Camera.main.transform.position;
        _chunkNumber = 1;
        //Creates first chunk where the camera is.
        _chunks.Add(new Chunk(_initialCameraPosition + new Vector2(Chunk.chunkWidth / 4, 0), _chunkNumber));

    }


    void Update()
    {
        _currentCameraPosition = Camera.main.transform.position;

        //If there is only one chunk right now and we soon need another, make another.    
        if (_chunks.Count == 1)
        {
            if (_currentCameraPosition.x >= ((Chunk)_chunks[0]).position.x + Chunk.chunkWidth / 4)
            {
                _chunkNumber++;
                _chunks.Add(new Chunk(((Chunk)_chunks[0]).position + new Vector2(Chunk.chunkWidth, 0), _chunkNumber));
            }

        }

        /*
        If we have more than one chunk,
        check each chunk to see if its far enough left to be deleted.
        */
        else
        {
            for (int i = 0; i < _chunks.Count; i++)
            {
                {
                    if (((Chunk)_chunks[i]).position.x + Chunk.chunkWidth / 2 <= _currentCameraPosition.x - Chunk.chunkWidth / 4)
                    {
                        //Call Chunk method that removes previously loaded gameObjects.
                        ((Chunk)_chunks[i]).Unload();

                        //Remove that Chunk object from our ArrayList.
                        _chunks.Remove((Chunk)_chunks[i]);
                        break;
                    }
                }
            }
        }

        //Draws boundaries of _chunks.
        if (debug)
        {
            foreach (Chunk chunk in _chunks){
                Debug.DrawLine(new Vector2(chunk.position.x - Chunk.chunkWidth / 2, chunk.position.y + Chunk.chunkWidth / 2), new Vector2(chunk.position.x - Chunk.chunkWidth / 2, chunk.position.y - Chunk.chunkWidth / 2), new Color(1, 0, 0));
                Debug.DrawLine(new Vector2(chunk.position.x + Chunk.chunkWidth / 2, chunk.position.y + Chunk.chunkWidth / 2), new Vector2(chunk.position.x + Chunk.chunkWidth / 2, chunk.position.y - Chunk.chunkWidth / 2), new Color(1, 0, 0));
                Debug.DrawLine(new Vector2(chunk.position.x - Chunk.chunkWidth / 2, chunk.position.y + Chunk.chunkWidth / 2), new Vector2(chunk.position.x + Chunk.chunkWidth / 2, chunk.position.y + Chunk.chunkWidth / 2), new Color(1, 0, 0));
                Debug.DrawLine(new Vector2(chunk.position.x - Chunk.chunkWidth / 2, chunk.position.y - Chunk.chunkWidth / 2), new Vector2(chunk.position.x + Chunk.chunkWidth / 2, chunk.position.y - Chunk.chunkWidth / 2), new Color(1, 0, 0));
            }
        }

    }
}

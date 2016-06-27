/*
Description: Handles creating and deleting chunks. Chunks are created and deleted when they 
are certain distances from the main camera. Chunks are controlled areas of game objects.
Created: 6/26/2016
*/

using UnityEngine;
using System.Collections;

public class ChunkController : MonoBehaviour
{

    private static Vector2 _initialCameraPosition;
    private Vector2 _currentCameraPosition;
    private static int _currentChunk;
    private ArrayList Chunks = new ArrayList();


    void Start()
    {
        _initialCameraPosition = Camera.main.transform.position;
        _currentChunk = 0;
        _currentChunk++;
        Chunks.Add(new Chunk(_initialCameraPosition + new Vector2(Chunk.chunkWidth / 4, 0), _currentChunk));

    }


    void Update()
    {
        _currentCameraPosition = Camera.main.transform.position;

        //Create chunk.      
        if (Chunks.Count == 1)
        {
            if (_currentCameraPosition.x >= ((Chunk)Chunks[0]).position.x + Chunk.chunkWidth / 4)
            {
                _currentChunk++;
                Chunks.Add(new Chunk(((Chunk)Chunks[0]).position + new Vector2(Chunk.chunkWidth, 0), _currentChunk));
            }

        }
        else
        {
            for (int i = 0; i < Chunks.Count; i++)
            {
                {

                    if (((Chunk)Chunks[i]).position.x + Chunk.chunkWidth / 2 <= _currentCameraPosition.x - Chunk.chunkWidth / 4)
                    {
                        ((Chunk)Chunks[i]).Remove();
                        Chunks.Remove((Chunk)Chunks[i]);
                        break;
                    }
                }
            }
        }

        //Delete chunk.

    }
}

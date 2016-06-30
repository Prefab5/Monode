/*Description: Handles creating and deleting _chunks. _chunks are created and deleted when they 
are certain distances from the main camera. _chunks are controlled areas of game objects.
Created: 6/26/2016
*/

using UnityEngine;
using System.Collections;

public class ChunkController : MonoBehaviour
{

    private static Vector2 _initialCameraPosition;
    private Vector2 _currentCameraPosition;
    private static int _chunkCount;
    public bool debug = true;
    public float chunkMoveSpeed = 4f;

    void Start()
    {
        _initialCameraPosition = Camera.main.transform.position;
        _chunkCount = 1;

        //Creates first chunk where the camera is.
        GameObject chunk = Object.Instantiate(Resources.Load("Chunk"),
                    _initialCameraPosition + new Vector2(Chunk.chunkWidth / 4, 0), 
                    Quaternion.Euler(0, 0, 0)) as GameObject;

        //Set parent for hierachy orginizational purposes.
        chunk.transform.parent = transform;
        chunk.transform.name = "Chunk " + _chunkCount;
        
        _chunkCount++;


    }


    void Update()
    {
        ChunkSpawnManagement();
        ChunkMovement();
        ChunkDebugRect();
    }

    void ChunkSpawnManagement()
    {
        _currentCameraPosition = Camera.main.transform.position;

        //If there is only one chunk right now and we soon need another, make another.    
        if (gameObject.transform.childCount == 1)
        {
            if (_currentCameraPosition.x >= gameObject.transform.GetChild(0).position.x + Chunk.chunkWidth / 4)
            {
               
                
                GameObject chunk = Object.Instantiate(Resources.Load("Chunk"),
                    gameObject.transform.GetChild(0).position + new Vector3(Chunk.chunkWidth, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;

                chunk.transform.parent = transform;
                chunk.transform.name = "Chunk " + _chunkCount;
                _chunkCount++;

            }

        }

        /*If we have more than one chunk,check each chunk to see if its 
        far enough left to be deleted.*/
        else
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                {
                    if (gameObject.transform.GetChild(i).position.x + Chunk.chunkWidth / 2 <= _currentCameraPosition.x - Chunk.chunkWidth / 4)
                    {

                        GameObject.Destroy(gameObject.transform.GetChild(i).gameObject);
                        break;
                    }
                }
            }
        }
    }

    void ChunkDebugRect()
    {
        if (debug)
        {
            for(int i = 0; i < gameObject.transform.childCount; i++)
            {
                Debug.DrawLine(new Vector2(gameObject.transform.GetChild(i).position.x - Chunk.chunkWidth / 2, gameObject.transform.GetChild(i).position.y + Chunk.chunkWidth / 2), new Vector2(gameObject.transform.GetChild(i).position.x - Chunk.chunkWidth / 2, gameObject.transform.GetChild(i).position.y - Chunk.chunkWidth / 2), new Color(1, 0, 0));
                Debug.DrawLine(new Vector2(gameObject.transform.GetChild(i).position.x + Chunk.chunkWidth / 2, gameObject.transform.GetChild(i).position.y + Chunk.chunkWidth / 2), new Vector2(gameObject.transform.GetChild(i).position.x + Chunk.chunkWidth / 2, gameObject.transform.GetChild(i).position.y - Chunk.chunkWidth / 2), new Color(1, 0, 0));
                Debug.DrawLine(new Vector2(gameObject.transform.GetChild(i).position.x - Chunk.chunkWidth / 2, gameObject.transform.GetChild(i).position.y + Chunk.chunkWidth / 2), new Vector2(gameObject.transform.GetChild(i).position.x + Chunk.chunkWidth / 2, gameObject.transform.GetChild(i).position.y + Chunk.chunkWidth / 2), new Color(1, 0, 0));
                Debug.DrawLine(new Vector2(gameObject.transform.GetChild(i).position.x - Chunk.chunkWidth / 2, gameObject.transform.GetChild(i).position.y - Chunk.chunkWidth / 2), new Vector2(gameObject.transform.GetChild(i).position.x + Chunk.chunkWidth / 2, gameObject.transform.GetChild(i).position.y - Chunk.chunkWidth / 2), new Color(1, 0, 0));
            }
        }
    }

    void ChunkMovement()
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).transform.Translate(new Vector2(-1 * chunkMoveSpeed * Time.deltaTime, 0));
        }
    }

    public void PlayerCollision()
    {

    }

}

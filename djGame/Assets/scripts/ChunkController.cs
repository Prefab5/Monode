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
	private int chunkWidth = 10;
	private float difficulty = 0;

	public GameObject scoreObject;
	private ScoreController scoreController;

	public GameObject enemyControllerObject;
	private EnemyController enemyController;

    
    
    
    

    void Start()
    {
		InitializeDifficulty ();
		ConfigureChunkWidth ();

        _initialCameraPosition = Camera.main.transform.position;
        _chunkCount = 1;

        //Creates first chunk where the camera is.
        GameObject chunk = Object.Instantiate(Resources.Load("Chunk"),
                    _initialCameraPosition + new Vector2(chunkWidth / 4, 0), 
                    Quaternion.Euler(0, 0, 0)) as GameObject;
		chunk.GetComponent<Chunk> ().chunkWidth = chunkWidth;

        //Set parent for hierachy orginizational purposes.
        chunk.transform.parent = transform;
        chunk.GetComponent<Chunk>().chunkNumber = _chunkCount;
        chunk.transform.name = "Chunk " + _chunkCount;
        
        _chunkCount++;


    }

	void InitializeDifficulty (){
		scoreController = scoreObject.GetComponent<ScoreController> ();

		enemyController = enemyControllerObject.GetComponent<EnemyController> ();
		enemyController.difficulty = difficulty;

	}

	private void ConfigureChunkWidth (){


		float screenWidth = Camera.main.orthographicSize * Screen.width / Screen.height * 4;

		if (screenWidth > chunkWidth) {
			//+5 to give th 5 units of travel time to load the next chunk.
			chunkWidth = Mathf.CeilToInt (screenWidth+5);

			//Must be even to prevent gap between chunks.
			if (chunkWidth % 2 == 1) {
				chunkWidth++;
			}
		}

	}


    void Update()
    {
        ChunkSpawnManagement();
        ChunkMovement();
        ChunkDebugRect();
    }

	void AdjustGameDifficulty(){
		//A factor of 500 means that at 500 score the difficulty will be maxed at 1.0f 
		int factor = 500;

		difficulty = Mathf.Clamp01 (scoreController.GetScore () / factor);
		enemyController.difficulty = difficulty;
	}

    void ChunkSpawnManagement()
    {
        _currentCameraPosition = Camera.main.transform.position;

        //If there is only one chunk right now and we soon need another, make another.    
        if (gameObject.transform.childCount == 1)
        {
            if (_currentCameraPosition.x >= gameObject.transform.GetChild(0).position.x + chunkWidth / 4)
            {
				
				AdjustGameDifficulty ();
                
                GameObject chunk = Object.Instantiate(Resources.Load("Chunk"),
                    gameObject.transform.GetChild(0).position + new Vector3(chunkWidth, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
				chunk.GetComponent<Chunk> ().chunkWidth = chunkWidth;
				chunk.GetComponent<Chunk> ().difficulty = difficulty;

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
                    if (gameObject.transform.GetChild(i).position.x + chunkWidth / 2 <= _currentCameraPosition.x - chunkWidth / 4)
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
                Debug.DrawLine(new Vector2(gameObject.transform.GetChild(i).position.x - chunkWidth / 2, gameObject.transform.GetChild(i).position.y + chunkWidth / 2), new Vector2(gameObject.transform.GetChild(i).position.x - chunkWidth / 2, gameObject.transform.GetChild(i).position.y - chunkWidth / 2), new Color(1, 0, 0));
                Debug.DrawLine(new Vector2(gameObject.transform.GetChild(i).position.x + chunkWidth / 2, gameObject.transform.GetChild(i).position.y + chunkWidth / 2), new Vector2(gameObject.transform.GetChild(i).position.x + chunkWidth / 2, gameObject.transform.GetChild(i).position.y - chunkWidth / 2), new Color(1, 0, 0));
                Debug.DrawLine(new Vector2(gameObject.transform.GetChild(i).position.x - chunkWidth / 2, gameObject.transform.GetChild(i).position.y + chunkWidth / 2), new Vector2(gameObject.transform.GetChild(i).position.x + chunkWidth / 2, gameObject.transform.GetChild(i).position.y + chunkWidth / 2), new Color(1, 0, 0));
                Debug.DrawLine(new Vector2(gameObject.transform.GetChild(i).position.x - chunkWidth / 2, gameObject.transform.GetChild(i).position.y - chunkWidth / 2), new Vector2(gameObject.transform.GetChild(i).position.x + chunkWidth / 2, gameObject.transform.GetChild(i).position.y - chunkWidth / 2), new Color(1, 0, 0));
            }
        }
    }

    void ChunkMovement()
    {
        
        LeftMovement();
        
    }
		

    private void LeftMovement()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).transform.Translate(new Vector2(-1 * chunkMoveSpeed * Time.deltaTime, 0));
        }
    }

}

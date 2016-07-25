/*Description: A chunk generates a multitude of game objects within it's bounds
in which the player moves through. All prefabs shoulds be generated here.
Created: 6/26/16
*/

using UnityEngine;
using System.Collections;

public class Chunk : MonoBehaviour
{
	//Width of a chunk in Unity units. A larger chunk will load more objects at once.
	public int chunkWidth;

	//Identification number of chunk.
	public int chunkNumber;

	public float difficulty;

	public LayerMask GroundLayerMask;

	public void Start ()
	{
		//ConfigureChunkWidth ();
		Load ();
	}




	//Any objects in the chunk should be spawned here.
	private void Load ()
	{
		
		SpawnGround ("Desert Ground", new string[]{ "Desert_a", "Desert_b", "Desert_c" });

		//Must be spawned after ground.
		SpawnGroundObstacles ("Ground_Obstacle", .1f, difficulty, 3);

		SpawnScenery ("Cactus", .04f, -.2f, new string[]{ "cactus_a", "cactus_b" }); 

		SpawnScenery ("Rock", .02f, -.45f); 

		SpawnScenery ("Shrub", .10f, -.5f, new string[]{ "shrub_a", "shrub_b" }); 

		SpawnScenery ("Bones", .02f, -.5f, new string[]{ "bones_a", "cowskull_a" }); 

		SpawnScenery ("Sign", .01f, -0.12f); 

		SpawnPickup ("Ammo", 0.02f, -0.42f);


	}

	private void SpawnGroundObstacles (string prefab, float spawnChance, float difficulty, int maxGrouping)
	{
		//Clamping arguments.
		spawnChance = Mathf.Clamp01 (spawnChance);
		difficulty = Mathf.Clamp01 (difficulty);

		//Predefine the ground obstacle to be spawned and the beginning leftmost chunk location.
		GameObject groundObstacleToSpawn = Resources.Load (prefab) as GameObject;
		Vector2 spawnLocation = new Vector2 (transform.position.x - chunkWidth / 2, 0);

		ArrayList spawnedGroundObstacles = new ArrayList ();

		//Iterate through chunk.
		for (int i = 0; i < chunkWidth; i++) {

			//Offset for player start.
			if(chunkNumber == 1 && i == 0){
				i = Mathf.RoundToInt(chunkWidth/8*5);
				spawnLocation.x += Mathf.RoundToInt(chunkWidth/8*5);
			}

			//Spawn chance.
			if (Random.value < spawnChance) {

				//Check if there is ground for the ground obstacle to spawn on.
				Vector2 rayCastStart = new Vector2 (spawnLocation.x, chunkWidth / 2);
				RaycastHit2D hitInfo = Physics2D.Raycast (rayCastStart, Vector2.down, (float)chunkWidth, GroundLayerMask);
				if (hitInfo.collider != null) {

					//Check for this spawn location being too close to other, already spawned, ground obstacles.
					bool tooClose = false;

					//Check previous chunk.
					if (transform.parent.childCount > 1) {

						GameObject previousChunk = transform.parent.GetChild (0).gameObject;

						for (int j = 0; j < previousChunk.transform.childCount; j++) {
							

							if (spawnLocation.x - previousChunk.transform.GetChild (j).transform.position.x <= 10 - (5 * difficulty)) {
								
								if (previousChunk.transform.GetChild (j).tag == "collision_obstacle") {
									
									tooClose = true;
								}
							}
						}
					}

					//Check this chunk.
					for (int j = 0; j < spawnedGroundObstacles.Count; j++) {
						if (Mathf.Abs (((GameObject)spawnedGroundObstacles [j]).transform.position.x - spawnLocation.x) < 10 - (5 * difficulty)) {
							tooClose = true;
						}
					}

					if (!tooClose) {

						//Atleast 1.
						int groupSize = 1;
						//Add 0 - 2;
						groupSize +=  Mathf.RoundToInt (difficulty * (maxGrouping - 1));

						//Limit to chunk's bounds.
						if (spawnLocation.x + groupSize > transform.position.x + chunkWidth / 2) {
							groupSize -= Mathf.RoundToInt(Mathf.Abs ((spawnLocation.x + groupSize) - (transform.position.x + chunkWidth / 2)));
						}

						for (int j = 0; j < groupSize; j++) {

							//Figure appropriate spawnLocation.y to ensure ground contact.
							spawnLocation.y = hitInfo.point.y + groundObstacleToSpawn.GetComponent<BoxCollider2D> ().size.y;
					
							//Spawn ground obstacle.
							GameObject spawnedGroundObstacle = Instantiate (groundObstacleToSpawn,
								new Vector2(spawnLocation.x + j, spawnLocation.y),
								Quaternion.Euler (0, 0, 0)) as GameObject;
							spawnedGroundObstacle.gameObject.transform.parent = transform;
							spawnedGroundObstacle.gameObject.name = prefab;

							spawnedGroundObstacles.Add (spawnedGroundObstacle);



						}

						spawnLocation.x += groupSize;
					}



	
				}
			}

			//Move forward in the chunk.
			spawnLocation.x++;
		}

	}

	private void SpawnPickup (string prefab, float spawnChance, float spawnHeight)
	{
		spawnChance = Mathf.Clamp01 (spawnChance);

		//Farthest left point in chunk.
		Vector2 spawnLocation = new Vector2 (transform.position.x - chunkWidth / 2, spawnHeight);
		Vector2 startLocation = spawnLocation;

		//Iterates through chunk left to right.
		for (int i = 0; i < chunkWidth; i++) {
			
			//Chance to spawn.
			if (Random.value < spawnChance) {

				bool overlappingObstacle = false;
				for (int j = 0; j < transform.childCount; j++) {
					
					if (spawnLocation.x == transform.GetChild (j).position.x
					    &&
					    transform.GetChild (j).tag == "collision_obstacle") {
//						print ("Overlap between me: " + spawnLocation.x + " and this:" + transform.GetChild (j).tag);
						overlappingObstacle = true;
					}
				}

				if (!overlappingObstacle) {

					GameObject spawnedPickup = Instantiate (Resources.Load (prefab),
						                           new Vector2 (spawnLocation.x, spawnLocation.y),
						                           Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;

					spawnedPickup.transform.parent = transform;
					spawnedPickup.name = prefab;
				}
			}
			spawnLocation.x += 1;
		}
	}

	private void SpawnScenery (string prefab, float spawnChance, float spawnHeight, string[] alternateSprites = null)
	{
		spawnChance = Mathf.Clamp01 (spawnChance);

		//Farthest left point in chunk.
		Vector2 spawnLocation = new Vector2 (transform.position.x - chunkWidth / 2, spawnHeight);
		Vector2 startLocation = spawnLocation;

		//Iterates through chunk left to right.
		for (int i = 0; i < chunkWidth; i++) {
			//Chance to spawn.
			if (Random.value < spawnChance) {

				bool overlappingObstacle = false;
				for (int j = 0; j < transform.childCount; j++) {

					if (spawnLocation.x == transform.GetChild (j).position.x
					    &&
					    (transform.GetChild (j).tag == "Scenery" || transform.GetChild (j).tag == "collision_obstacle")) {

//						print ("Overlap between" + (Resources.Load (prefab) as GameObject).tag + " and " + transform.GetChild (j).tag);

						overlappingObstacle = true;
					}
				}

				if (!overlappingObstacle) {
					GameObject spawnedScenery = Instantiate (Resources.Load (prefab),
						                            new Vector2 (spawnLocation.x, spawnLocation.y),
						                            Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
				
					spawnedScenery.transform.parent = transform;
					spawnedScenery.name = prefab;
					if (alternateSprites != null) {
						spawnedScenery.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> (alternateSprites [Random.Range (0, alternateSprites.Length)]);
					} 
				}
			}
			spawnLocation.x++;
		}
	}

	private void SpawnGround (string prefab, string[] alternateSprites = null)
	{

		//Farthest left point in chunk.
		Vector3 spawnLocation = new Vector3 (transform.position.x - chunkWidth / 2, -1, 0);

		//Iterates through chunk left to right.
		for (int i = 0; i < chunkWidth; i++) {
			GameObject spawnedGround = Instantiate (Resources.Load (prefab), spawnLocation, Quaternion.Euler (0, 0, 0)) as GameObject;
			spawnedGround.transform.parent = transform;
			spawnedGround.name = prefab;
			spawnLocation.x++;
		}
	}

	//Any gameObjects loaded in this chunk are despawned in Unload().
	public void Unload ()
	{     
		//For each child gameObjects.
		for (int i = 0; i < gameObject.transform.childCount; i++) {
			//Remove it from scene.
			Object.Destroy (gameObject.transform.GetChild (i));

			//Remove from i so that we don't skip objects in the ArrayList.
			i--;
		}

		print ("Chunk #" + chunkNumber + " unloaded.");
	}

	/*Spawns prefabs across the chunk from the leftmost edge to the rightmost edge according to
    parameters.*/
	private void SpawnPrefab (string prefabName,
	                            float spawnChancePerUnit,
	                            int noCloserThan,
	                            int atleastOnePer, float height, string[] altSprites = null)
	{

		/*Clamps parameter [spawnChancePerUnit] so that my code doesn't crash if someone
        passes something like 5000 into it. It will just equate to 1 instead.*/
		spawnChancePerUnit = Mathf.Clamp (spawnChancePerUnit, 0, 1);

		/*At instantiation this is the leftmost coordinate in chunk. This will 
        be the Vector2 we move forward and possibly spawn the prefabs at.*/
		Vector2 indexPosition = new Vector2 (transform.position.x - chunkWidth / 2, height);

		/*We will store any spawned prefabs of [prefabName] in this temp array so that
        we can compare across them their distances from one another to make sure
        we do not spawn any too close together or too far apart.*/
		ArrayList prefabsOfSameType = new ArrayList ();

		/*This loop will push indexPosition by one unit to the right each iteration
        and run a series of tests for each index to see if a prefab should be spawned there.*/
		for (int i = 0; i < chunkWidth; i++) {
			/*If this is the first chunk and we are on the first loop iteration,
            go ahead and skip the first 7 units. We don't want to spawn anything
            too close to the player.*/
			if (chunkNumber == 1 && i == 0 && ((GameObject)Resources.Load (prefabName)).tag != "Ground") {
				i = Mathf.CeilToInt (chunkWidth / 2);
				indexPosition.x += Mathf.CeilToInt (chunkWidth / 2);
			}

			//This is the if statement that will spawn a prefab by chance, according to
			//[spawnChancePerUnit], a (0 - 1) float.
			if (Mathf.Round (Random.Range (0, chunkWidth)) <= (chunkWidth * spawnChancePerUnit)) {
				/*Creates a gameObject clone of the passed prefab from within the Resources folder at
                the current indexPosition.*/
				GameObject spawnedPrefab = Object.Instantiate (Resources.Load (prefabName),
					                                       indexPosition, Quaternion.Euler (0, 0, 0)) as GameObject;
				if (altSprites != null) {
					spawnedPrefab.GetComponent<SpriteRenderer> ().sprite = Resources.Load <Sprite> (altSprites [Random.Range (0, altSprites.Length)]);
				}

				//MARK

				//Set the gameObject's parent as the chunk so that when the chunk moves, the prefab moves.
				spawnedPrefab.transform.parent = transform;

				//Add to array list of similiar prefabs to compare distances between each.
				prefabsOfSameType.Add (spawnedPrefab);

				//spawnedPrefab.transform.name = spawnedPrefab.transform.name.Replace("(Clone)", " " + prefabsOfSameType.Count);
				spawnedPrefab.transform.name = prefabName;

			


				//If the last prefab spawned is closer than [noCloserThan], delete it.
				//if (prefabsOfSameType.Count >= 2 && Mathf.Abs(((GameObject)prefabsOfSameType[prefabsOfSameType.Count - 2]).transform.position.x - spawnedPrefab.transform.position.x) <= noCloserThan)
				if (prefabsOfSameType.Count >= 2 && Mathf.Abs (((GameObject)prefabsOfSameType [prefabsOfSameType.Count - 2]).transform.position.x - spawnedPrefab.transform.position.x) <= noCloserThan) {

					//Remove it from the similiar prefabs array.
					prefabsOfSameType.Remove (spawnedPrefab);

					//Remove it from scene.
					Object.Destroy (spawnedPrefab);

				}

				if (prefabsOfSameType.Count == 1) {
					//Same check as above but cross chunk.
					GameObject lastObject;
					for (int j = 0; j < transform.parent.GetChild (0).childCount; j++) {
						if (transform.parent.GetChild (0).GetChild (j).name == prefabName) {
							lastObject = transform.parent.GetChild (0).GetChild (j).gameObject;
//							print("Distance to " + lastObject.name + " = " + Mathf.Abs (lastObject.transform.position.x - spawnedPrefab.transform.position.x));
							if (Mathf.Abs (lastObject.transform.position.x - spawnedPrefab.transform.position.x) <= noCloserThan) {

								//Remove it from the similiar prefabs array.
								prefabsOfSameType.Remove (spawnedPrefab);

								//Remove it from scene.
								Object.Destroy (spawnedPrefab);

							}

						}
					}
				}



			}

			if ((prefabsOfSameType.Count == 0 && Mathf.Abs ((transform.position.x - chunkWidth / 2) - indexPosition.x) >= atleastOnePer)
			             ||
			             (prefabsOfSameType.Count != 0 && Mathf.Abs (((GameObject)prefabsOfSameType [prefabsOfSameType.Count - 1]).transform.position.x - indexPosition.x) >= atleastOnePer)) {

				/*Creates a gameObject clone of the passed prefab from within the Resources folder at
                the current indexPosition.*/
				GameObject spawnedPrefab = Object.Instantiate (Resources.Load (prefabName),
					                                       indexPosition, Quaternion.Euler (0, 0, 0)) as GameObject;

				if (altSprites != null) {
					print (altSprites [Random.Range (0, altSprites.Length - 1)]);
					spawnedPrefab.GetComponent<SpriteRenderer> ().sprite = Resources.Load <Sprite> (altSprites [Random.Range (0, altSprites.Length - 1)]);
				}

				//Set the gameObject's parent as the chunk so that when the chunk moves, the prefab moves.
				spawnedPrefab.transform.parent = transform;

				//Add to array list of similiar prefabs to compare distances between each.
				prefabsOfSameType.Add (spawnedPrefab);

				//spawnedPrefab.transform.name = spawnedPrefab.transform.name.Replace("(Clone)", " " + prefabsOfSameType.Count);
				spawnedPrefab.transform.name = prefabName;

			}

			indexPosition.x++;
		}
	}
}

/*Description: A chunk generates a multitude of game objects within it's bounds
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

    //Arraylist of all gameObjects in this chunk.
    private ArrayList _gameObjects = new ArrayList();

    //Constructor.
    public Chunk(Vector2 position, int chunkNumber)
    {
        this.position = position;
        this.chunkNumber = chunkNumber;

        Load();

    }

    //Any objects in the chunk should be spawned here.
    private void Load()
    {
        /*Spawn ground obstacles,
        at a 10% chance per unit,
        with none closer than 5 units together,
        but none any farther than 10 units apart.*/
        SpawnPrefab("Ground_Obstacle", .10f, 5, 10, 0);

        //If you want to spawn something rare, than do this.
        //SpawnPrefab("Rare_Thing", .02f, 0, chunkWidth);
        //It will spawn at a 2% chance and disregards proximity.    

        Debug.Log("Chunk #" + chunkNumber + " loaded.");
    }

    //Any gameObjects loaded in this chunk are despawned in Unload().
    public void Unload()
    {     
        //For each gameObject in gameObjects.
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            //Remove it from scene.
            Object.Destroy((GameObject)_gameObjects[i]);

            //Remove it from array.
            _gameObjects.Remove(((GameObject)_gameObjects[i]));

            //Remove from i so that we don't skip objects in the ArrayList.
            i--;
        }


        Debug.Log("Chunk #" + chunkNumber + " unloaded.");
    }

    /*Spawns prefabs across the chunk from the leftmost edge to the rightmost edge according to
    parameters.*/
    private void SpawnPrefab(string prefabName,
        float spawnChancePerUnit,
        int noCloserThan,
        int atleastOnePer, float height)
    {
        /*Clamps parameter [spawnChancePerUnit] so that my code doesn't crash if someone
        passes something like 5000 into it. It will just equate to 1 instead.*/
        spawnChancePerUnit = Mathf.Clamp(spawnChancePerUnit, 0, 1);

        /*At instantiation this is the leftmost coordinate in chunk. This will 
        be the Vector2 we move forward and possibly spawn the prefabs at.*/
        Vector2 indexPosition = new Vector2(position.x - chunkWidth / 2, height);

        /*We will store any spawned prefabs of [prefabName] in this temp array so that
        we can compare across them their distances from one another to make sure
        we do not spawn any too close together or too far apart.*/
        ArrayList prefabsOfSameType = new ArrayList();

        /*This loop will push indexPosition by one unit to the right each iteration
        and run a series of tests for each index to see if a prefab should be spawned there.*/
        for (int i = 0; i < chunkWidth; i++)
        {
            /*If this is the first chunk and we are on the first loop iteration,
            go ahead and skip the first 7 units. We don't want to spawn anything
            too close to the player.*/
            if (chunkNumber == 1 && i == 0)
            {
                i = 7;
                indexPosition.x += 7;
            }

            //This is the if statement that will spawn a prefab by chance, according to
            //[spawnChancePerUnit], a (0 - 1) float.
            if (Mathf.Round(Random.Range(0, chunkWidth)) <= (chunkWidth * spawnChancePerUnit))
            {
                /*Creates a gameObject clone of the passed prefab from within the Resources folder at
                the current indexPosition.*/
                GameObject spawnedPrefab = Object.Instantiate(Resources.Load(prefabName),
                    indexPosition, Quaternion.Euler(0, 0, 0)) as GameObject;

                /*Moves the gameObject up by half the prefab's height so its sitting on the ground,
                as opposed to in it. gameObject positions coordinate to their center.*/
                //spawnedPrefab.transform.position = new Vector2(spawnedPrefab.transform.position.x,
                //    spawnedPrefab.transform.position.y + spawnedPrefab.GetComponent<Renderer>().bounds.size.y / 2);

                //Add to array list of similiar prefabs to compare distances between each.
                prefabsOfSameType.Add(spawnedPrefab);

                /*Add to array list of all gameObjects in chunk so on unload we can 
                conveniently delete all of them.*/
                _gameObjects.Add(spawnedPrefab);

                //If the last prefab spawned is closer than [noCloserThan], delete it.
                if (prefabsOfSameType.Count >= 2 && Mathf.Abs(((GameObject)prefabsOfSameType[prefabsOfSameType.Count - 2]).transform.position.x - spawnedPrefab.transform.position.x) <= noCloserThan)
                {

                    //Remove it from the total gameObjects array.
                    _gameObjects.Remove(spawnedPrefab);

                    //Remove it from the similiar prefabs array.
                    prefabsOfSameType.Remove(spawnedPrefab);

                    //Remove it from scene.
                    Object.Destroy(spawnedPrefab);
                  
                }
            }

            if ((prefabsOfSameType.Count == 0 && Mathf.Abs((position.x - chunkWidth / 2) - indexPosition.x) >= atleastOnePer) 
                || 
                (prefabsOfSameType.Count != 0 && Mathf.Abs(((GameObject)prefabsOfSameType[prefabsOfSameType.Count - 1]).transform.position.x - indexPosition.x) >= atleastOnePer))
            {

                /*Creates a gameObject clone of the passed prefab from within the Resources folder at
                the current indexPosition.*/
                GameObject spawnedPrefab = Object.Instantiate(Resources.Load("Ground_Obstacle"),
                    indexPosition, Quaternion.Euler(0, 0, 0)) as GameObject;

                /*Moves the gameObject up by half the prefab's height so its sitting on the ground,
                as opposed to in it.*/
                spawnedPrefab.transform.position = new Vector2(spawnedPrefab.transform.position.x,
                    spawnedPrefab.transform.position.y + spawnedPrefab.GetComponent<Renderer>().bounds.size.y / 2);


                //Add to array list of similiar prefabs to compare distances between each.
                prefabsOfSameType.Add(spawnedPrefab);

                //Add to array list of all prefabs in chunk so on unload we can delete all of them.
                _gameObjects.Add(spawnedPrefab);
            }

            indexPosition.x++;
        }
    }
}

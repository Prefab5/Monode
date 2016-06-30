using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public ChunkController chunkController;

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "collision_obstacle")
        {
            chunkController.PlayerCollision();
            PlayerCollision();
        }
    }

    void PlayerCollision()
    {

    }

}

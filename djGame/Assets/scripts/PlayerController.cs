using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public ChunkController chunkController;

    public float jumpHeight = 100f;

    public Transform groundPoint;
    public float groundPointRadius;
    public LayerMask groundMask;

    bool isGrounded;
    Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Checks to see if player is in contact with ground directly beneath them.
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, groundPointRadius, groundMask);
    }

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

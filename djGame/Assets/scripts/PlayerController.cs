using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public ChunkController chunkController;

    private float jumpHeight = 500f;
    private float knockDownTime = 2f;
    private float timeKnockedDown = 0;
    private bool collision = false;

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

        //Jumping controls.
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb2D.AddForce(new Vector2(0, jumpHeight));

        }

        PlayerCollision();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "collision_obstacle")
        {
            chunkController.PlayerCollision();
            collision = true;
        }
    }

    void PlayerCollision()
    {
        if (collision)
        {
            timeKnockedDown += Time.deltaTime;

            if(timeKnockedDown > knockDownTime)
            {
                timeKnockedDown = 0;
                collision = false;
                chunkController.GetComponent<ChunkController>().ResumeMovement();
            }
        }
    }

}

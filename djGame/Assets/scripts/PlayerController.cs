/************************************************
 * Definition: Methods for controlling the player.
 * 
 * Created: ??/??/??
 * **********************************************/

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public ChunkController chunkController;

	private float jumpHeight = 500f;
	private float knockDownTime = 2f;
	private float timeKnockedDown = 0;
	private bool collision = false;

	//For detecting grounded state to allow jumping.
	public Transform groundPoint;
	public float groundPointRadius;
	public LayerMask groundMask;
	bool isGrounded;

	Rigidbody2D rb2D;

	//For controlling the score.
	public GameObject scoreObject;
	private ScoreController score;

	public GameObject Health;
	private HealthController healthController;

	public GameObject Pause;
	private bool paused = false;



	void Start ()
	{
		rb2D = GetComponent<Rigidbody2D> ();

		score = scoreObject.GetComponent<ScoreController> ();
		score.SetScoreRate (10);

		healthController = Health.GetComponent<HealthController> ();
	}

	void Update ()
	{
		JumpControl ();

		if (collision) {
			KnockDown ();
		}

	}

	void JumpControl ()
	{
		//Checks to see if player is in contact with ground directly beneath them.
		isGrounded = Physics2D.OverlapCircle (groundPoint.position, groundPointRadius, groundMask);

		/*Check to see if game is paused. We don't want them adding jumpforce
		to the character while the game is paused.*/
		paused = Pause.GetComponent<Pause> ().paused;


		//Jumping controls.
		if (Input.GetKeyDown (KeyCode.Space) && isGrounded && !paused) {
			rb2D.AddForce (new Vector2 (0, jumpHeight));

		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
        
		if (other.gameObject.tag == "collision_obstacle") {
			chunkController.PlayerCollision ();
			collision = true;
			score.PauseScore ();
			healthController.LoseHealth (20);

		}
	}

	void KnockDown ()
	{
		timeKnockedDown += Time.deltaTime;

		if (timeKnockedDown > knockDownTime) {
			timeKnockedDown = 0;
			collision = false;
			chunkController.GetComponent<ChunkController> ().ResumeMovement ();
			score.ResumeScore ();
		}
	}

}

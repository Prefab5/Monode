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



	void Start ()
	{
		rb2D = GetComponent<Rigidbody2D> ();

		score = scoreObject.GetComponent<ScoreController> ();
		score.SetScoreRate (10);

		healthController = Health.GetComponent<HealthController> ();
	}

	void Update ()
	{

		int fingerCount = 0;
		foreach (Touch touch in Input.touches) {
			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
				fingerCount++;
			if (isGrounded) {
				rb2D.AddForce (new Vector2 (0, jumpHeight));
			}
		}
		if (fingerCount > 0)
			print("User has " + fingerCount + " finger(s) touching the screen");
		



		JumpControl ();

		if (collision) {
			KnockDown ();
		}

	}

	void JumpControl ()
	{
		//Checks to see if player is in contact with ground directly beneath them.
		isGrounded = Physics2D.OverlapCircle (groundPoint.position, groundPointRadius, groundMask);

		//Jumping controls.
		if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
			rb2D.AddForce (new Vector2 (0, jumpHeight));

		}





	}

	void OnTriggerEnter2D (Collider2D other)
	{
        
		if (other.gameObject.tag == "collision_obstacle") {
			chunkController.PlayerCollision ();
			collision = true;
			score.PauseScore ();
			healthController.LoseHealth ();

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

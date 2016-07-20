/************************************************
 * Definition: Methods for controlling the player.
 * 
 * Created: ??/??/??
 * **********************************************/

using UnityEngine;
using UnityEngine.EventSystems;
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

	public GameObject pause;
	private bool paused;

	Rigidbody2D rb2D;

	//For controlling the score.
	public GameObject scoreObject;
	private ScoreController score;

	public GameObject Health;
	private HealthController healthController;

	private bool touched = false;



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

		paused = pause.GetComponent<Pause> ().paused;

		//Jumping controls.
		if (Input.GetKeyDown (KeyCode.Space) && isGrounded && !paused) {
			rb2D.AddForce (new Vector2 (0, jumpHeight));

		}

		foreach (Touch touch in Input.touches) {

			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled) {
				if (isGrounded && !paused && !touched && !EventSystem.current.IsPointerOverGameObject(touch.fingerId)) {
					rb2D.AddForce (new Vector2 (0, jumpHeight));
					touched = true;
				}
				if (EventSystem.current.IsPointerOverGameObject (touch.fingerId)) {
					print ("UI");
				}
			}
		}
		if (Input.touches.Length == 0) {
			touched = false;
		}

	}

	void OnTriggerEnter2D (Collider2D other)
	{
        
		if (other.gameObject.tag == "collision_obstacle") {
			chunkController.PlayerCollision ();
			collision = true;
			score.PauseScore ();
			healthController.LoseHealth (34);
			GameObject.Destroy (other.gameObject);

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

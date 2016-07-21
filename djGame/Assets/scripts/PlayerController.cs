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

	public GameObject AmmoController;
	private AmmoController ammoController;

	private bool touched = false;



	void Start ()
	{
		rb2D = GetComponent<Rigidbody2D> ();

		score = scoreObject.GetComponent<ScoreController> ();
		score.SetScoreRate (10);

		healthController = Health.GetComponent<HealthController> ();
		ammoController = AmmoController.GetComponent<AmmoController> ();
	}

	void Update ()
	{
		Controls ();

		if (collision) {
			KnockDown ();
		}

	}
		
	void Controls ()
	{
		Jumping ();

		Shooting ();
	
	}

	void Jumping(){

		//PC

		//Checks to see if player is in contact with ground directly beneath them.
		isGrounded = Physics2D.OverlapCircle (groundPoint.position, groundPointRadius, groundMask);

		//Check if game is paused.
		paused = pause.GetComponent<Pause> ().paused;

		//Jumping controls.
		if (Input.GetKeyDown (KeyCode.W) && isGrounded && !paused) {
			rb2D.AddForce (new Vector2 (0, jumpHeight));

		}

		//Touch Device
		foreach (Touch touch in Input.touches) {

			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled) {
				if (touch.position.x > Screen.width / 2) {
					if (isGrounded && !paused && !touched && !EventSystem.current.IsPointerOverGameObject (touch.fingerId)) {
						rb2D.AddForce (new Vector2 (0, jumpHeight));
						//So that jumping can only be triggered once per touch and release.
						touched = true;
					}
				}
			}
		}

		if (Input.touches.Length == 0) {
			touched = false;
		}


	}

	void Shooting(){
		//PC
		if (Input.GetKeyDown (KeyCode.Space)) {
			Shoot ();
		}

		//Touch Device
		foreach (Touch touch in Input.touches) {
			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled) {
				if (touch.position.x < Screen.width / 2) {
					Shoot ();
				}
			}
		}
	}

	void Shoot(){
		if (ammoController.Fireable()) {
			
			ammoController.DecreaseAmmoCount (1);
			GameObject bullet = Instantiate(Resources.Load("Bullet"), new Vector3(transform.position.x + .75f,
				transform.position.y + .125f , 0),
				Quaternion.Euler(0,0,0)) as GameObject;

			bullet.GetComponent<Bullet>().TravelDirection (new Vector2 (1, 0));

			
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

		if (other.gameObject.tag == "Ammo") {
			ammoController.IncreaseAmmoCount (5);
			Destroy (other.gameObject);
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

using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private int health = 1;
	private float movementSpeed = 1;
	private float attackRange = 1f;
	private float attackCooldown = 5;
	private bool onCooldown = false;
	private float cooldown = 0; 

	public GameObject playerObject;
	private PlayerController player;
	private float directionOfPlayer;

	public LayerMask layermask;

	void Start(){
		player = playerObject.GetComponent<PlayerController> ();
	}

	void Update(){
		DirectionOfPlayer ();
		Movement ();
		Attack ();


	}

	void Attack(){
		if (RangeCheck ()) {
			Strike ();
		}

		Cooldown ();
	}

	public void Damage(int amount){
		health -= amount;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}

	private void Cooldown(){
		if (cooldown > 0) {
			cooldown -= Time.deltaTime;
		} else {
			cooldown = 0;
		}
	}

	private void Strike(){
		
		RaycastHit2D hitInfo = Physics2D.Raycast (transform.position, new Vector2 (1 * directionOfPlayer, 0), .5f, layermask);
		if (hitInfo.collider != null) {
			if (cooldown == 0) {
				cooldown = attackCooldown;
				if (hitInfo.transform.tag == "Player") {
					player.Damage (34);
				}
			}
		}
	}

	private bool RangeCheck(){
		if (Mathf.Abs(transform.position.x - player.transform.position.x) < attackRange) {
			
			return true;
		}
		return false;
	}

	void Movement(){
		if (!RangeCheck()) {
			transform.Translate (new Vector3 (movementSpeed * Time.deltaTime, 0, 0));
		}
	}

	void DirectionOfPlayer(){
		if (transform.position.x < player.transform.position.x) {
			directionOfPlayer = 1;
		} else {
			directionOfPlayer = -1;
		}
	}

}

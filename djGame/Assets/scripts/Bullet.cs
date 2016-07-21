using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private float velocity = 16;
	private float spawnTime;
	private float lifeSpan = 3;

	void Start(){
		spawnTime = Time.time;
		gameObject.name = "Bullet";
	}

	void Update () {
		transform.Translate(new Vector3(velocity * Time.deltaTime, 0, 0));

		if (Time.time - spawnTime > lifeSpan) {
			Destroy (gameObject);
		}

	}

	public void TravelDirection(Vector2 direction){
		if (direction.x < 0) {
			velocity = velocity * -1;
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "Enemy") {
			other.gameObject.GetComponent<Enemy> ().Damage (1);
			Destroy (gameObject);
		}


	}

}

using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public GameObject player;

	private float spawnTimer = 5;
	private float timer = 0;

	public float difficulty = 0;

	private float directionOfPlayer;

	void Update(){
		SpawnTiming ();
	}
		
	void SpawnTiming(){
		spawnTimer = 10 - (7 * difficulty);

		if (timer >= spawnTimer) {
			SpawnEnemy ();
			timer = 0;
		} else {
			timer += Time.deltaTime;
		}

	}

	void SpawnEnemy(){
		float screenWidth = Camera.main.orthographicSize * Screen.width / Screen.height * 2;
		float enemyWidth = (Resources.Load ("Enemy") as GameObject).GetComponent<Renderer> ().bounds.size.x;

		Vector3 SpawnLocation = new Vector3 ((screenWidth / 2 + enemyWidth) * -1, 0, 0);
		GameObject enemy = Instantiate (Resources.Load ("Enemy"), SpawnLocation, Quaternion.Euler (0, 0, 0)) as GameObject;

		enemy.GetComponent<Enemy> ().playerObject = player;
		enemy.GetComponent<Enemy> ().name = "Enemy";
			
	}
		

}

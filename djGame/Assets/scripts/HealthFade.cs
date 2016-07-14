using UnityEngine;
using System.Collections;

public class HealthFade : MonoBehaviour {

	private float totalLength;
	private float currentLength;

	private float maxHealth = 100;
	private float currentHealth;
	private float endingHealth;


	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		endingHealth = currentHealth;

		totalLength = GetComponent<RectTransform> ().sizeDelta.x;
		currentLength = totalLength;
	}
	
	// Update is called once per frame
	void Update () {

		HealthEasing ();

		currentLength = (currentHealth / maxHealth) * totalLength;
		GetComponent<RectTransform> ().sizeDelta = new Vector2 (currentLength, GetComponent<RectTransform> ().sizeDelta.y);
		GetComponent<RectTransform> ().anchoredPosition = new Vector2 ((currentLength / 2) + 7, GetComponent<RectTransform> ().anchoredPosition.y);
	}

	public void Adjust(float target){
		endingHealth = target;
	}

	//Eases the hp bar movement.
	void HealthEasing(){
		float change = Mathf.Abs (currentHealth - endingHealth);
		if (currentHealth > endingHealth) {
			currentHealth -= 1;
		} else {
			currentHealth = endingHealth;
		}

		if (change <= 1) {
			currentHealth = endingHealth;
		}
	}
}

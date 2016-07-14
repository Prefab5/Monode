using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
	private float totalLength;
	private float currentLength;

	private float maxHealth = 100;
	private float currentHealth;
	private float endingHealth;

	private float timeSteady = 0;

	public GameObject HealthFade;

	void Start(){
		currentHealth = maxHealth;
		endingHealth = currentHealth;
		totalLength = GetComponent<RectTransform> ().sizeDelta.x;
		currentLength = totalLength;
	}

	void Update(){
		if (currentHealth <= 0) {
			SceneManager.LoadScene ("scenes/game_over");
		} else {

			_HealthFade ();

			HealthEasing ();

			currentLength = (currentHealth / maxHealth) * totalLength;
			GetComponent<RectTransform> ().sizeDelta = new Vector2 (currentLength, GetComponent<RectTransform> ().sizeDelta.y);
			GetComponent<RectTransform> ().anchoredPosition = new Vector2 ((currentLength / 2) + 7.65f, GetComponent<RectTransform> ().anchoredPosition.y);



		}
	}

	private void _HealthFade(){

		if (currentHealth == endingHealth) {
			timeSteady += Time.deltaTime;
		} else {
			timeSteady = 0;
		}

		if(timeSteady >= 0.5f){
			HealthFade.GetComponent<HealthFade> ().Adjust(endingHealth);
		}

	}

	public void GiveHealth (int amount)
	{
		endingHealth+= amount;
	}

	public void LoseHealth (int amount)
	{
		endingHealth-= amount;
	}

	//Eases the hp bar movement.
	void HealthEasing(){
		float change = Mathf.Abs (currentHealth - endingHealth);
		if (currentHealth > endingHealth) {
			currentHealth -= change / 4;
		} else {
			currentHealth += change / 4;
		}

		if (change <= 0.25f) {
			currentHealth = endingHealth;
		}
	}

}

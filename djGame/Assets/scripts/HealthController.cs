using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthController : MonoBehaviour {

	private int health = 3;

	public void GiveHealth(){
		health++;
		UpdateHealth ();
	}

	public void LoseHealth(){
		health--;
		UpdateHealth ();
	}

	private void UpdateHealth(){
		switch (health) {
		case 1:
			gameObject.transform.GetChild (0).GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Heart");
			gameObject.transform.GetChild (1).GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Empty Heart");
			gameObject.transform.GetChild (2).GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Empty Heart");
			break;

		case 2:
			gameObject.transform.GetChild (0).GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Heart");
			gameObject.transform.GetChild (1).GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Heart");
			gameObject.transform.GetChild (2).GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Empty Heart");
			break;

		case 3:
			gameObject.transform.GetChild (0).GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Heart");
			gameObject.transform.GetChild (1).GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Heart");
			gameObject.transform.GetChild (2).GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Heart");
			break;
		}
	}

}

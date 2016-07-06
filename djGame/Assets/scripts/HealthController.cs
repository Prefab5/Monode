using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{

	private int health = 3;

	public void GiveHealth ()
	{
		health++;
		UpdateHealth ();
	}

	public void LoseHealth ()
	{
		health--;
		UpdateHealth ();
	}

	private void UpdateHealth ()
	{
		switch (health) {
		case 0:
			gameObject.transform.GetChild (0).GetComponent<Image> ().sprite = Resources.Load <Sprite> ("EmptyHeart");
			gameObject.transform.GetChild (1).GetComponent<Image> ().sprite = Resources.Load <Sprite> ("EmptyHeart");
			gameObject.transform.GetChild (2).GetComponent<Image> ().sprite = Resources.Load <Sprite> ("EmptyHeart");
			SceneManager.LoadScene ("scenes/game_over");
			break;

		case 1:
			gameObject.transform.GetChild (0).GetComponent<Image> ().sprite = Resources.Load <Sprite> ("Heart");
			gameObject.transform.GetChild (1).GetComponent<Image> ().sprite = Resources.Load <Sprite> ("EmptyHeart");
			gameObject.transform.GetChild (2).GetComponent<Image> ().sprite = Resources.Load <Sprite> ("EmptyHeart");
			break;

		case 2:
				
			gameObject.transform.GetChild (0).GetComponent<Image> ().sprite = Resources.Load <Sprite> ("Heart");
			gameObject.transform.GetChild (1).GetComponent<Image> ().sprite = Resources.Load <Sprite> ("Heart");
			gameObject.transform.GetChild (2).GetComponent<Image> ().sprite = Resources.Load <Sprite> ("EmptyHeart");
			break;

		case 3:
			gameObject.transform.GetChild (0).GetComponent<Image> ().sprite = Resources.Load <Sprite> ("Heart");
			gameObject.transform.GetChild (1).GetComponent<Image> ().sprite = Resources.Load <Sprite> ("Heart");
			gameObject.transform.GetChild (2).GetComponent<Image> ().sprite = Resources.Load <Sprite> ("Heart");
			break;
		}
	}

}

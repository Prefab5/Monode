using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pause : MonoBehaviour {

	public bool paused = false;

	public GameObject pauseScreen;

	public GameObject pauseImage;

	public void _Pause(){


		if (!paused) {
			Time.timeScale = 0;
			paused = true;
			pauseScreen.SetActive (true);
			pauseImage.GetComponent<Image> ().sprite = Resources.Load <Sprite>("play button");

		} else {
			Time.timeScale = 1;
			paused = false;
			pauseScreen.SetActive (false);
			pauseImage.GetComponent<Image> ().sprite = Resources.Load <Sprite>("pause button");

		}


	}
}

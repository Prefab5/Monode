using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pause : MonoBehaviour {

	public bool paused = false;

	public Text _text;

	public GameObject pauseScreen;

	public void _Pause(){
		if (!paused) {
			paused = true;
			Time.timeScale = 0;
			_text.text = "Unpause";
			_text.rectTransform.offsetMin = new Vector2 (_text.rectTransform.offsetMin.x, 0);
			pauseScreen.SetActive (true);

		} else {
			paused = false;
			Time.timeScale = 1;
			_text.text = "Pause";
			_text.rectTransform.offsetMin = new Vector2 (_text.rectTransform.offsetMin.x, 0);
			pauseScreen.SetActive (false);

		}
	}
}

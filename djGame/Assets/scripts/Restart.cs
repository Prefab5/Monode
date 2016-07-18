using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

	public GameObject gameOverScreen;

	public void _Restart ()
	{
		Time.timeScale = 1;
		gameOverScreen.SetActive(false);

		SceneManager.LoadScene ("scenes/Master Scene");		

	}
}

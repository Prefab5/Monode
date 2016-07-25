using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public void _StartGame(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Master Scene");
	}

}

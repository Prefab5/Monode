using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

	public void _Restart ()
	{
		SceneManager.LoadScene ("scenes/Master Scene");		
	}
}

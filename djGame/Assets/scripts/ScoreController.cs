/********************************************************************
 * Definition: Methods for controlling the score.
 * Requirements: PlayerController references the gameObject
 * containing this script.
 * 
 * Created: 7/3/16
 * ******************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreController : MonoBehaviour
{

	public int scoreRate = 0;
	private float score = 0;
	private bool pause = false;

	void Update ()
	{		
		IncrementScore ();
		UpdateScore ();
	}

	public void IncreaseScore (int amount)
	{
		score += amount;
	}

	public void DescreaseScore (int amount)
	{
		score -= amount;
		if (score < 0) {
			score = 0;
		}
	}

	public float GetScore(){
		return score;
	}

	void IncrementScore ()
	{
		if (!pause) {
			score += scoreRate * Time.deltaTime;
		}
	}

	void UpdateScore ()
	{
		gameObject.GetComponent<Text> ().text = "Score: " + Mathf.Round (score).ToString ();
	}

	public void PauseScore ()
	{
		pause = true;
	}

	public void ResumeScore ()
	{
		pause = false;
	}

	public void SetScoreRate (int RatePerSecond)
	{
		scoreRate = RatePerSecond;
	}
}

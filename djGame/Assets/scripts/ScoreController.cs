﻿/********************************************************************
 * Definition: Methods for controlling the score.
 * Requirements: PlayerController references the gameObject
 * containing this script.
 * 
 * Created: 7/3/16
 * ******************************************************************/

using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour
{

	public int scoreRate;
	private float score;
	private bool pause = false;

	void Start ()
	{

	}

	void Update ()
	{		
		IncrementScore ();	
	}

	void IncrementScore ()
	{
		if (scoreRate != null && !pause) {
			score += scoreRate * Time.deltaTime;
			UpdateScore ();
		}
	}

	void UpdateScore ()
	{
		gameObject.GetComponent<GUIText> ().text = "Score: " + Mathf.Round (score).ToString();
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

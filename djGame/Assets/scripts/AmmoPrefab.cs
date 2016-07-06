using UnityEngine;
using System.Collections;
/************************************
 * Written by Travis Nutting
 * 7/6/16
 * Ammo Prefab class that randomizes the 
 * pickups count
 ************************************/
public class AmmoPrefab : MonoBehaviour {

	//Public ints that set the lowest and highest
	//possible ammo counts for the bullet prefabs
	public int lowestAmmoRate = 15;
	public int highestAmmoRate = 25;

	private int ammoRate;

	// Use this for initialization
	void Start () {
		//randomize the ammorate for this prefab
		ammoRate = (int) Mathf.Round (Random.Range (lowestAmmoRate, highestAmmoRate));
	}

	//called when the player picks up the prefab
	public int getAmmoForPrefab () {
		return ammoRate;
	}

}

using UnityEngine;
using System.Collections;
/************************************
 * Written by Travis Nutting
 * 7/6/16
 * Class to Keep track of current amount
 * of ammo that the player has.
 ************************************/
public class AmmoController : MonoBehaviour {

	private int ammo; 

	// Use this for initialization
	void Start () {
		//Start player with no ammo and set the ammo count on screen
		ammo = 0;
		gameObject.GetComponent<GUIText> ().text = "Ammo: " + Mathf.Round (ammo).ToString();
	}
	

	//Increase the ammo count the given amount
	//and set the new ammo count on the screen
	public void IncrementAmmo (int newAmmo) {
		ammo = ammo + newAmmo;
		gameObject.GetComponent<GUIText> ().text = "Ammo: " + Mathf.Round (ammo).ToString();
	}

	//Method to decrease the ammor for the player.
	//This will be used when a weapon is introduced (7/6/16)
	public void DecreaseAmmo (int newAmmo) {
		ammo = ammo - newAmmo;
		gameObject.GetComponent<GUIText> ().text = "Ammo: " + Mathf.Round (ammo).ToString();
	}

//No longer keep ammorate inside Ammocontroller. Check the AmmoPrefab Class.
//	public void SetAmmoRate(int r) {
//		ammo = r;
//		gameObject.GetComponent<GUIText> ().text = "Ammo: " + Mathf.Round (ammo).ToString();
//	}
}

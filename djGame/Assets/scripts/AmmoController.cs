using UnityEngine;
using System.Collections;

public class AmmoController : MonoBehaviour {

	private int ammo; 
	public int ammoRate;


	// Use this for initialization
	void Start () {
		ammo = 0;
		ammoRate = 1;
		gameObject.GetComponent<GUIText> ().text = "Ammo: " + Mathf.Round (ammo).ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IncrementAmmo () {
		ammo = ammo + ammoRate;
		gameObject.GetComponent<GUIText> ().text = "Ammo: " + Mathf.Round (ammo).ToString();
	}

	public void SetAmmoRate(int r) {
		ammoRate = r;
		ammo = r;
		gameObject.GetComponent<GUIText> ().text = "Ammo: " + Mathf.Round (ammo).ToString();
	}
}

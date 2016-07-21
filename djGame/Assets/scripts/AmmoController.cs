using UnityEngine;
using System.Collections;

public class AmmoController : MonoBehaviour {

	private int ammoCount = 5;

	public void IncreaseAmmoCount(int amount){
		ammoCount += amount;
	}

	public void DecreaseAmmoCount(int amount){
		ammoCount -= amount;
	}

	void Update(){
		UpdateText ();
	}

	void UpdateText(){
		GetComponent<UnityEngine.UI.Text> ().text = "Ammo: " + ammoCount;
	}

	public bool Fireable(){
		if (ammoCount > 0) {
			return true;
		} else {
			return false;
		}
	}

}

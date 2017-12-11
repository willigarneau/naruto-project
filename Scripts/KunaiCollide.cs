using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiCollide : MonoBehaviour {
	void OnTriggerEnter(Collider other) { // Si le kunai touche quelque chose, on détruit le kunai
		GameObject.Destroy (this.gameObject);
	}
}

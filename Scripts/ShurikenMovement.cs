using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenMovement : MonoBehaviour {

	void Start () {
		GetComponent<Rigidbody> ().velocity = transform.forward * 15;
		StartCoroutine ("destroyShuriken");
	}

	IEnumerator destroyShuriken() {
		yield return new WaitForSeconds (2.0f);
		GameObject.Destroy (gameObject);
	}
}

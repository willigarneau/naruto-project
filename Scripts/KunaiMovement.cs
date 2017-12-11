using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiMovement : MonoBehaviour {

	// Use this for initialization
	void Start () { // Faire une séparation de tous les kunai pour pas qu'ils s'empilent
		GetComponent<Rigidbody> ().velocity = transform.forward * 10;
	}
}

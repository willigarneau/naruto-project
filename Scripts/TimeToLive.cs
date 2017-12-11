using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToLive : MonoBehaviour {

	public float time = 1.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine ("explosionCoroutine");
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator explosionCoroutine() {
		yield return new WaitForSeconds (time);
		GameObject.Destroy (this.gameObject);
	}
}

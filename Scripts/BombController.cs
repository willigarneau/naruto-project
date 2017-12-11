using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {
	
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		StartCoroutine ("explosionCoroutine");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator explosionCoroutine() {//Décompte de la bombe, instancier l'explosion et détruire la bombe
		yield return new WaitForSeconds (1.3f);
		GameObject.Instantiate (explosion, transform.position, Quaternion.identity);
		GameObject.Destroy (this.gameObject);
	}
    //Savoir si le joueur est dans la frame ou non
	void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
		}
	}
    //Collision quand le joueur est dans la range
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
		}
	}
}

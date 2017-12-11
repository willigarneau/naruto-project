using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovementMouse : Photon.PunBehaviour {
	
	public float forwardSpeed = 5.0f;
	public bool isGrounded = false;
	public float jumpForce = 500f;
	private Animator anim;
	private Rigidbody rb;
	public bool lockCtrl = false;
	public bool isDown = false;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();

		if (!photonView.isMine) {
			rb.isKinematic = true;
		}
	}

	void Update () { // Mouvements instanciés selon le input du clavier
		
		if (!photonView.isMine)
			return;

		if(Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 1.0f)) {//On est ou
			isGrounded = true;
			anim.SetBool ("Grounded", true);
		}
		else {
			isGrounded = false;
			anim.SetBool ("Grounded", false);
		}

        //Dans l'asset de départ
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Base Layer.Shout")) {
			lockCtrl = true;
			rb.velocity = new Vector3(0, 0, 0);
		} else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Base Layer.Idle")) {
			lockCtrl = false;
		} 
			
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
		Vector3 m_Move = v*m_CamForward + h*Camera.main.transform.right;

		if ((Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D))
			&& isGrounded && !lockCtrl  && !isDown) {
			Vector3 move =  Vector3.Normalize(m_Move) * forwardSpeed;
			rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
			transform.LookAt (transform.position + new Vector3(rb.velocity.x, 0, rb.velocity.z));
		}

		if(!(Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D))
			&& isGrounded && !lockCtrl  && !isDown) {
			rb.velocity = new Vector3(0, rb.velocity.y, 0);
		}

		if (isGrounded && Input.GetKeyDown (KeyCode.Space) && !lockCtrl  && !isDown) {
			rb.AddForce (Vector3.up * jumpForce);
		}

		anim.SetFloat("Jump", rb.velocity.y);
	}
    //Dans l'asset de départ
	public void AddForce(Vector3 dir) {
		isDown = true;
		GetComponent<Rigidbody> ().AddForce (new Vector3(dir.x , dir.y, dir.z));
		StartCoroutine ("downCoroutine");
	}
    //Update sender
	IEnumerator downCoroutine() {
		yield return new WaitForSeconds (0.3f);
		isDown = false;
	}
}

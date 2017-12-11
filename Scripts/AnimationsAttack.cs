using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimatorController : Photon.MonoBehaviour
{
	
	private Animator animator;

	public GameObject ownPunch;
	public GameObject ownKick;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		if (!animator)
		{
			Debug.LogError("AttackAnimatorController is Missing Animator Component",this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!animator)
		{
			return;
		}

		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);  

		if (stateInfo.IsName ("Base Layer.punch_21") || stateInfo.IsName ("Base Layer.punch_22")) {
			ownPunch.SetActive (true);
			ownKick.SetActive (false);
		} else if (stateInfo.IsName ("Base Layer.kick_21") || stateInfo.IsName ("Base Layer.kick_24")) {
			ownPunch.SetActive (false);
			ownKick.SetActive (true);
		} else {
			ownPunch.SetActive (false);
			ownKick.SetActive (false);
		}

		if (photonView.isMine == false && PhotonNetwork.connected == true)
		{
			return;
		}
			
        //Si on est sur le sol
		if (stateInfo.IsName("Base Layer.Grounded"))
		{
			if (Input.GetButtonDown("Fire2")) 
			{
				animator.SetTrigger("Kick");
				animator.ResetTrigger ("Punch1");
				animator.ResetTrigger ("Punch2");
				animator.ResetTrigger ("Kick3");
			}                  
		}
        //Si on est sur le sol
		if (stateInfo.IsName("Base Layer.Grounded"))
		{
			if (Input.GetButtonDown("Fire1")) 
			{
				animator.SetTrigger("Punch1");
				animator.ResetTrigger ("Kick3");
				animator.ResetTrigger ("Punch2");
				animator.ResetTrigger ("Kick");
			}                  
		}
        //Faire un coup dans les airs
		if (stateInfo.IsName("Base Layer.punch_21"))
		{
			if (Input.GetButtonDown("Fire1")) 
			{
				animator.SetTrigger("Punch2");
				animator.ResetTrigger ("Punch1");
				animator.ResetTrigger ("Kick3");
				animator.ResetTrigger ("Kick");
			}                  
		}
        //Faire un coup dans les airs
		if (stateInfo.IsName("Base Layer.punch_22"))
		{
			if (Input.GetButtonDown("Fire2")) 
			{
				animator.SetTrigger("Kick3");
				animator.ResetTrigger ("Punch1");
				animator.ResetTrigger ("Punch2");
				animator.ResetTrigger ("Kick");

			}                  
		}
        //Donner un coup de pied sur le sol
		if (stateInfo.IsName("Base Layer.kick_21"))
		{
			animator.ResetTrigger("Kick3");
			animator.ResetTrigger ("Punch1");
			animator.ResetTrigger ("Punch2");
			animator.ResetTrigger ("Kick");                
		}
        //Donner un coup de pieds sur le sol
		if (stateInfo.IsName("Base Layer.kick_24"))
		{
			animator.ResetTrigger("Kick3");
			animator.ResetTrigger ("Punch1");
			animator.ResetTrigger ("Punch2");
			animator.ResetTrigger ("Kick");                
		}
	}
}

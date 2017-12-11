using UnityEngine;
using System.Collections;

public class MyPlayerAnimatorManager : Photon.MonoBehaviour
{
	private Animator animator;
    //Direction, pouvoir l'update avec photon
	public float DirectionDampTime = .25f;

	void Start() 
	{
		animator = GetComponent<Animator>();
		if (!animator)
		{
			Debug.LogError("Il manque d'informations pour animer le personnage",this);
		}
	}
		
	void Update() 
	{
		if (photonView.isMine == false && PhotonNetwork.connected == true)
		{
			return;
		}
		if (!animator)
		{
			return;
		}

		//Sauter
		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);          
		//Autoriser sauter juste si on cours
		if (stateInfo.IsName("Base Layer.Run"))
		{
			// Ou quand on presse le bouton
			if (Input.GetButtonDown("Fire2")) 
			{
				animator.SetTrigger("Jump");
			}                
		}
        //Direction manager
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		if (v < 0)
		{
			v = 0;
		}
		animator.SetFloat("Speed", h * h + v * v);
		animator.SetFloat("Direction", h, DirectionDampTime, Time.deltaTime);
	}


}
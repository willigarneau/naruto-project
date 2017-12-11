using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (MyThirdPersonCharacter))]
public class MyUserControls : Photon.MonoBehaviour
{
	private MyThirdPersonCharacter m_Character; 
	private Transform m_Cam;              
	private Vector3 m_CamForward;            
	private Vector3 m_Move;
	private bool m_Jump;                


	private void Start()
	{
		if (Camera.main != null)
		{
			m_Cam = Camera.main.transform;
		}
		m_Character = GetComponent<MyThirdPersonCharacter>();
	}


	private void Update()
	{
		if (!photonView.isMine)
			return;
		
		if (!m_Jump)
		{
			m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
		}
	}

	private void FixedUpdate()
	{
		if (!photonView.isMine)
			return;
		//Ou on va
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");
        //Calculer dans quelle direction il va falloir aller
		if (m_Cam != null)
		{
			//Calculer comment la caméra va aussi devoir bouger
			m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
			m_Move = v*m_CamForward + h*m_Cam.right;
		}
		else
		{
            //Sinon, on se déplace tout droit
			m_Move = v*Vector3.forward + h*Vector3.right;
		}
		if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
		// Envoyer les mouvements a thirdpersoncharacter
		m_Character.Move(m_Move, false, m_Jump);
		m_Jump = false;
	}
}
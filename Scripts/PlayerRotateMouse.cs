using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotateMouse : Photon.PunBehaviour {
	public float offset = 130;

	void Update () {
		if (!photonView.isMine)
			return;
	}
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Player manager. 
/// Handles fire Input and Beams.
/// </summary>
public class MyPlayerManager : Photon.PunBehaviour, IPunObservable
{
	public static GameObject LocalPlayerInstance;
	public float Health = 1f;
	public GameObject Beams;//Controlles du joueur et de ses macros
	public GameObject PlayerUiPrefab;
	private bool IsFiring;//Savoir quand on tire

	void Awake()
	{
		// Est-ce que j'ai des beams de créés (shuriken, ult, big shuriken, bomb)
		if (Beams == null)
		{
			Debug.LogError("Il manque d'information pour les beams", this);
		}
		else
		{
			Beams.SetActive(false);
		}
		if (photonView.isMine)
		{
			MyPlayerManager.LocalPlayerInstance = this.gameObject;
		}
		DontDestroyOnLoad(this.gameObject);
	}
		
	void Start()
	{
		ExitGames.Demos.DemoAnimator.CameraWork _cameraWork = this.gameObject.GetComponent<ExitGames.Demos.DemoAnimator.CameraWork>();
		if (PlayerUiPrefab!=null)
		{
			GameObject _uiGo =  Instantiate(PlayerUiPrefab) as GameObject;
			_uiGo.SendMessage ("SetTarget", this, SendMessageOptions.RequireReceiver);
		} else {
            Debug.LogError("Il manque une prefab pour votre joueur");
        }
		if (_cameraWork != null)
		{
			if (photonView.isMine)
			{
				_cameraWork.OnStartFollowing();
			}
		}
		else
		{
			Debug.LogError("<Color=Red><a>Il manque</a></Color> la caméra pour faire marcher",this);
		}
		UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, loadingMode) =>
		{
			if(this != null) {
				this.CalledOnLevelWasLoaded(scene.buildIndex);
			}
		};
	}

	void CalledOnLevelWasLoaded(int level)
	{
		//Spawn sur une plateforme stable
		if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
		{
			transform.position = new Vector3(0f, 5f, 0f);
		}

		//Suivre le joueur
		GameObject _uiGo = Instantiate(this.PlayerUiPrefab) as GameObject;
		_uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
	}
		
	void Update()
	{
		if (photonView.isMine) {
			ProcessInputs ();
		}
		if (Health <= 0f && photonView.isMine)
		{
			MyGameManager.Instance.LeaveRoom();
		} 
		if (Beams != null && IsFiring != Beams.GetActive()) 
		{
			Beams.SetActive(IsFiring);
		}
	}
		//Savoir qui va lancer un power
	void OnTriggerEnter(Collider other) 
	{
		if (!photonView.isMine)
		{
			return;
		}
		if (!other.name.Contains("Beam"))
		{
			return;
		}
		Health -= 0.1f;
	}
		//Si on garde le bouton enfoncé pour tirer
	void OnTriggerStay(Collider other) 
	{
		if (!photonView.isMine) 
		{
			return;
		}
		if (!other.name.Contains("Beam"))
		{
			return;
		}
		Health -= 0.1f*Time.deltaTime; 
	}
	void ProcessInputs()
	{
		if (Input.GetButtonDown("Fire1")) 
		{
			IsFiring = true;
		}

		if (Input.GetButtonUp("Fire1")) 
		{
			IsFiring = false;
		}
	}

	void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) { // Si le client possède le joueur
			stream.SendNext (IsFiring);
			stream.SendNext (Health);
		} else {
            //Joueur en réseau, donc envoyer l'information
			this.IsFiring = (bool) stream.ReceiveNext();
			this.Health = (float)stream.ReceiveNext ();
		}
	}
}
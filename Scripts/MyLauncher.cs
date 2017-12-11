using UnityEngine;

public class MyLauncher : Photon.PunBehaviour
{
	public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;

	//Maximum de joueurs possible
	public byte MaxPlayersPerRoom = 20;
	//UI du début pour commencer la partie
	public GameObject controlPanel;
	//UI pour dire que la connexion est en cours
	public GameObject progressLabel;
	public GameObject controlsPanel;

	private string _gameVersion = "1";//Savoir quel lobby on utilise
	private bool isConnecting;

	void Awake()
	{//Connexion à Photon Cloud
		PhotonNetwork.autoJoinLobby = false;
		PhotonNetwork.automaticallySyncScene = true;
		PhotonNetwork.logLevel = Loglevel;
	}
		
	void Start()
	{
		//UI initiale
		progressLabel.SetActive(false);
		controlPanel.SetActive(true);
		controlsPanel.SetActive (false);
	}

	public void ShowControls() {
		progressLabel.SetActive(false);
		controlPanel.SetActive(false);
		controlsPanel.SetActive (true);
	}

	public void HideControls() {
		progressLabel.SetActive(false);
		controlPanel.SetActive(true);
		controlsPanel.SetActive (false);
	}
		
	public void Connect()
	{
		//Connexion au lobby
		isConnecting = true;

		//Connexion...
		progressLabel.SetActive(true);
		controlPanel.SetActive(false);

		//Si on est connect au serveur
		if (PhotonNetwork.connected)
		{
			PhotonNetwork.JoinRandomRoom();
		}
		else
		{
			//Sinon on retourne au menu
			PhotonNetwork.ConnectUsingSettings(_gameVersion);
		}
	}

	public override void OnConnectedToMaster()
	{
		//Voir si on a cliqué sur Jouer
		if (isConnecting)
		{
			PhotonNetwork.JoinRandomRoom();
		}
	}
		
	public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
	{//Si on ne trouve pas de lobby, on en crée un
		PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
	}

	public override void OnJoinedRoom()
	{//En entrant dans un nouveau lobby
		if (PhotonNetwork.room.PlayerCount == 1)
		{
			PhotonNetwork.LoadLevel("Room for 1"); 
		}
	}

	public override void OnDisconnectedFromPhoton()
	{
		// Revenir au menu principal
		progressLabel.SetActive(false);
		controlPanel.SetActive(true);
	}

}

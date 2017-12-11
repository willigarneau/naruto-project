using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(InputField))]
public class MyPlayerNameInputField : MonoBehaviour
{
	static string playerNamePrefKey = "PlayerName";

	void Start () {
		string defaultName = "";
		InputField _inputField = this.GetComponent<InputField>();

		if (_inputField!=null)
		{
			//Charger le dernier nom qui avait été mit chez le client
			if (PlayerPrefs.HasKey(playerNamePrefKey))
			{
				defaultName = PlayerPrefs.GetString(playerNamePrefKey);
				_inputField.text = defaultName;
			}
		}
		PhotonNetwork.playerName =  defaultName;
	}
		//Sauvegarder le nom pour les prochaines sessions
	public void SetPlayerName(string value)
	{
		PhotonNetwork.playerName = value + " "; //Mettre le nom au dessus de la tete et partir la musique au debut du jeu
		PlayerPrefs.SetString(playerNamePrefKey,value);
		GetComponent<AudioSource> ().Play ();
	}

}

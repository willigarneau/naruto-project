using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MyPlayerUI : MonoBehaviour 
{
	public Text PlayerNameText;
	public Slider PlayerHealthSlider;
	public Vector3 ScreenOffset = new Vector3(0f,45f,0f);

	TaichiPlayerManager _target; // Joueur
	float _characterControllerHeight = 0f;
	Vector3 _targetPosition;

	void Awake()
	{
		this.GetComponent<Transform>().SetParent (GameObject.Find("Canvas").GetComponent<Transform>());
	}

	void Update()
	{
		//Si le personnage a quit, détruire son instance
		if (_target == null) 
		{
			Destroy(this.gameObject);
			return;
		}
		//Afficher la vie du bonhomme
		if (PlayerHealthSlider != null) 
		{
			PlayerHealthSlider.value = _target.Health;
		}
	}

	void LateUpdate() 
	{//Suivre le personnage uniquement associé au client
		if (_target!=null)
		{
			_targetPosition = _target.transform.position;
			_targetPosition.y += _characterControllerHeight;
			this.transform.position = Camera.main.WorldToScreenPoint (_targetPosition) + ScreenOffset;
		}
	}
		
	public void SetTarget(TaichiPlayerManager target)
	{
		_target = target;
		CharacterController _characterController = _target.GetComponent<CharacterController> (); // Savoir qui est le joueur pour que la caméra le suive
		if (_characterController != null)
		{
			_characterControllerHeight = _characterController.height;
		}
		if (PlayerNameText != null) 
		{
			PlayerNameText.text = _target.photonView.owner.name;
		}
	}
}
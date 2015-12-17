using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public Character character;

	void Awake()
	{
		character = new Character (this.gameObject);
		
		this.gameObject.name = "Pacman_" + character.GetPosName ();
	}

	void Update()
	{
		this.gameObject.name = "Pacman_" + character.GetPosName ();
	}

}

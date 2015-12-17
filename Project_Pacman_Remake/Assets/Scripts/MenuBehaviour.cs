using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour
{
	public GameObject prefabCharacter;
	private GameObject[] allChars;
	public Sprite[] allSprites;
	private bool ready;
	private Vector3 finalWorldPos;
	private Vector3 startWorldPos;
	public Text text;

	private void Start()
	{
		this.startWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (-Screen.width, -Screen.height, 0));
		this.finalWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));

		this.allChars = new GameObject[5];
		for (int i = 0 ; i < 5; i++)
		{
			this.allChars[i] = (GameObject)Instantiate(this.prefabCharacter,new Vector3(finalWorldPos.x  + i * 2 + 13,0,0), Quaternion.identity);
			this.allChars[i].GetComponent<SpriteRenderer>().sprite = allSprites[i];
		}
		print (Screen.width);
		this.ready = true;

		StartCoroutine (Anim ());
		StartCoroutine (WinkText ());
	}

	private void Update()
	{
		if (Input.GetKeyDown (KeyCode.Return))
			Application.LoadLevel ("Game");

		if (!ready)
			return;

		for (int i = 0 ; i < 5; i++)
		{
			if(allChars[i] != null)
			allChars[i].transform.Translate(-0.05f,0,0);

			if(allChars[i].transform.position.x < this.startWorldPos.x - 13 )
				this.allChars[i].transform.position = new Vector3(finalWorldPos.x + 13 	,0,0);
		}
	}

	private IEnumerator Anim()
	{
		yield return new WaitForSeconds (0.1f);
		for (int i = 0 ; i < 5; i++)
		{
			if(allChars[i].GetComponent<SpriteRenderer>().sprite == allSprites[i])
				allChars[i].GetComponent<SpriteRenderer>().sprite = allSprites[i+5];
			else
				allChars[i].GetComponent<SpriteRenderer>().sprite = allSprites[i];
		}

		StartCoroutine (Anim ());

	}

	private IEnumerator WinkText()
	{
		yield return new WaitForSeconds (0.3f);
		if (text.text == "Press Enter To Start")
			text.text = "";
		else
			text.text = "Press Enter To Start";

		StartCoroutine (WinkText ());
	}
}

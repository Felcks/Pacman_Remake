using UnityEngine;
using System.Collections;

public class SuperFood : MonoBehaviour
{
	private Vector3 startPos;

	private void Start()
	{
		this.startPos = this.transform.position;
		StartCoroutine (Winky());
	}

	IEnumerator Winky()
	{
		yield return new WaitForSeconds(0.2f);
		if (this.transform.position != new Vector3 (999, 999, 999))
			this.transform.position = new Vector3 (999, 999, 999);
		else
			this.transform.position = this.startPos;

		StartCoroutine (Winky());

	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
	public List<GameObject> openList;
	public List<GameObject> closedList;

	GameObject objetive;
	string objetivePosX;
	string objetivePosY;
	GameObject you;
	string yourPosX;
	string yourPosY;
	public bool done;
	public GameObject nextTile;
	public int nextTileIndex;



	public void SetObjetive(GameObject you, GameObject objetive)
	{
		this.objetive = objetive;
		this.objetivePosX = objetive.name.Substring (this.objetive.name.Length - 4, 2);
		this.objetivePosY = objetive.name.Substring  (this.objetive.name.Length - 2, 2);
		this.you = you;		
		this.yourPosX = objetive.name.Substring  (this.you.name.Length - 4, 2);
		this.yourPosY = objetive.name.Substring  (this.you.name.Length - 2, 2);

		this.openList = new List<GameObject> ();
		this.closedList = new List<GameObject> ();
		this.closedList.Add (you);
		this.nextTile = null;
		this.nextTileIndex = 1;

		StartCoroutine(this.StartPathfinding ());
	}

	public IEnumerator StartPathfinding()
	{
		yield return new WaitForFixedUpdate ();
		for(int i=0; i<this.closedList.Count;i++)
		{
			int index_X = int.Parse (this.closedList[i].name.Substring(this.closedList[i].name.Length - 4, 2));
			int index_Y = int.Parse (this.closedList[i].name.Substring(this.closedList[i].name.Length - 2, 2));

			string addZeroX =  (index_X <= 9) ? "0" : "";
			string addZeroY =  (index_Y <= 9) ? "0" : "";

			
			GameObject upTile =  GameObject.Find ("Ground_"  + ((index_X + 1 <= 9) ? "0" : "") + (index_X + 1) + addZeroY + index_Y );
			GameObject downTile =  GameObject.Find ("Ground_" + ((index_X - 1 <= 9) ? "0" : "") + ((index_X) - 1) + "" + addZeroY + index_Y );
			GameObject rightTile =  GameObject.Find ("Ground_" + addZeroX + index_X + ((index_Y + 1 <= 9) ? "0" : "") + (index_Y + 1));
			GameObject leftTile =  GameObject.Find ("Ground_" + addZeroX + index_X + ((index_Y-1 <=9) ? "0" : "") + (index_Y - 1));

			if(index_Y == 27)
			{
				rightTile = GameObject.Find("Ground_1800"); 
			} 
			if(upTile.tag.Equals(Tags.ground) && openList.Contains(upTile) == false && closedList.Contains(upTile) == false)
			{
				openList.Add(upTile);
			}
			if(downTile.tag.Equals(Tags.ground) && openList.Contains(downTile) == false && closedList.Contains(downTile) == false)
			{
				openList.Add(downTile);
			}
			if(rightTile.tag.Equals(Tags.ground) && openList.Contains(rightTile) == false && closedList.Contains(rightTile) == false)
			{
				openList.Add(rightTile);
			}
			if(leftTile.tag.Equals(Tags.ground) && openList.Contains(leftTile) == false && closedList.Contains(leftTile) == false)
			{
				openList.Add(leftTile);
			}
		}

		StartCoroutine (this.NextSteps());
	}
	

	private IEnumerator NextSteps()
	{
		yield return new WaitForFixedUpdate ();
		float betterDistance = 999;
		int indexBetterTile = 0;
		for(int i=0; i<this.openList.Count; i++)
		{
			float distance = Vector3.Distance(this.objetive.transform.position, this.openList[i].transform.position);
			if(distance < betterDistance)
			{
				betterDistance = distance;
				indexBetterTile = i;
			}
		}
		if (this.openList.Count > 0)
		{
			this.openList [indexBetterTile].GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Objetive");
			this.closedList.Add (this.openList [indexBetterTile]);
		}
		if (this.nextTile == null && this.closedList.Count > 1)
		{
			this.nextTile = this.closedList [1];
			this.nextTileIndex = 1;
		}

		this.openList = new List<GameObject> ();

		StartCoroutine (CheckIfDone ());
	}

	private IEnumerator CheckIfDone()
	{
		yield return new WaitForFixedUpdate ();
	 	 GameObject lastTile = this.closedList[this.closedList.Count-1];
		string lastTileX = lastTile.name.Substring(lastTile.name.Length-4,2);
		string lastTileY = lastTile.name.Substring(lastTile.name.Length-2,2);

		if(lastTileX != this.objetivePosX || lastTileY != this.objetivePosY)
		{
			StartCoroutine (this.StartPathfinding());
		}
		else
		{
			done = true;
		}
	}

	public void GoToNextTile()
	{
		this.nextTileIndex++;
		this.nextTile = this.closedList[this.nextTileIndex];
	}
}

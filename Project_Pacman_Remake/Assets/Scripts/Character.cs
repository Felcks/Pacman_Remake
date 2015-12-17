using UnityEngine;
using System.Collections;

public class Character 
{
	private GameObject gameObject;
	public int posX, posY;

	public Character(GameObject gameObject)
	{
		this.gameObject = gameObject;
	}

	public string GetPosName()
	{
		float betterDistance = 999;
		string posName = "";
		for(int i=0; i<CreateTiles.allGrounds.Count; i++)
		{
			for(int j=0; j<CreateTiles.allGrounds[i].Count; j++)
			{
				float distance = Vector3.Distance(gameObject.transform.position, CreateTiles.allGrounds[i][j].transform.position);
				if(distance < betterDistance)
				{
					betterDistance = distance;
					posName = CreateTiles.allGrounds[i][j].name.Substring(CreateTiles.allGrounds[i][j].name.Length-4,4);
				}
			}
		}

		return posName;
	}
}

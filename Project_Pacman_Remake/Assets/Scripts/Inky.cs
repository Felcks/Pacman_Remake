using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Inky : Ghost
{
	public List<GameObject> openList;
	public List<GameObject> closedList;
	public List<GameObject> rightWay;
	public GameObject objetive;
	public GameObject twoTilesFoward;
	public GameObject player;
	public GameObject blinky;
	public PlayerController playerController;
	public GameObject nexTile;
	public Vector3 currentDirection;
	public GameObject lastTile;
	string posX;
	string posY;
	private bool started;

	public Material frigh;
	public Material defaultMat;

	void Start()
	{
		openList = new List<GameObject> ();
		closedList = new List<GameObject> ();
		rightWay = new List<GameObject> ();
		
		this.posX = this.name.Substring (this.name.Length - 4, 2);
		this.posY = this.name.Substring (this.name.Length - 2, 2);

		this.player = GameObject.FindWithTag (Tags.player);
		this.playerController = this.player.GetComponent<PlayerController> ();
		this.SetBlinky ();

		this.scatterTile = GameObject.Find ("Ground_0027");
		this.ghostBehaviour = GHOSTBEHAVIOUR.NONE;

	}

	private void Frightened()
	{
		this.posX = this.name.Substring (this.name.Length - 4, 2);
		this.posY = this.name.Substring (this.name.Length - 2, 2);
		
		int index_X = int.Parse (posX);
		int index_Y = int.Parse (posY);
		
		string addZeroX =  (index_X <= 9) ? "0" : "";
		string addZeroY =  (index_Y <= 9) ? "0" : "";
		
		GameObject upTile =  GameObject.Find ("Ground_"  + ((index_X + 1 <= 9) ? "0" : "") + (index_X + 1) + addZeroY + index_Y );
		GameObject downTile =  GameObject.Find ("Ground_"  + ((index_X - 1 <= 9) ? "0" : "") + ((index_X) - 1) + "" + addZeroY + index_Y );
		GameObject rightTile =  GameObject.Find ("Ground_"  + addZeroX + index_X + ((index_Y + 1 <= 9) ? "0" : "") + (index_Y + 1));
		GameObject leftTile =  GameObject.Find ("Ground_"  + addZeroX + index_X + ((index_Y-1 <=9) ? "0" : "") + (index_Y - 1));
		
		if(index_Y == 27)
		{
			rightTile = GameObject.Find("Ground_1800"); 
		}
		if (upTile.tag.Equals(Tags.ground) && upTile != lastTile)
		{
			openList.Add(upTile);
		}
		if(downTile.tag.Equals(Tags.ground) && downTile != lastTile)
		{
			openList.Add(downTile);
		}
		if (rightTile.tag.Equals(Tags.ground) && rightTile != lastTile)
		{
			openList.Add(rightTile);
		}
		if (leftTile.tag.Equals(Tags.ground) && leftTile != lastTile)
		{
			openList.Add(leftTile);
		}
		
		int sortedTileIndex = Random.Range (0, openList.Count - 1);
		this.lastTile = this.nexTile;
		this.nexTile = this.openList[sortedTileIndex];
		this.openList = new List<GameObject> ();
	}

   private void SetBlinky()
   {
		GameObject[] allEnemys = GameObject.FindGameObjectsWithTag (Tags.enemy);
		for(int i=0; i<allEnemys.Length;i++)
		{
			if(allEnemys[i].name.Contains("Blinky"))
			{
				this.blinky = allEnemys[i];
				break;
			}
		}
	}

	void SetTwoTilesFoward()
	{
		string playerPosX = this.player.name.Substring (this.player.name.Length - 4, 2);
		string playerPosY = this.player.name.Substring (this.player.name.Length - 2, 2);
		
		int player_X = int.Parse (playerPosX);
		int player_Y = int.Parse (playerPosY);

		switch(this.playerController.getCurrentDirection())
		{
			case (int)DIRECTION.UP:
				player_X += 2;
				break;
			case (int)DIRECTION.DOWN:
				player_X += -2;
				break;
			case (int)DIRECTION.LEFT:
				player_Y += -2;
				break;
			case (int)DIRECTION.RIGHT:
				player_Y += 2;
				break;
		}

		string addZeroX =  (player_X <= 9) ? "0" : "";
		string addZeroY =  (player_Y <= 9) ? "0" : "";

		twoTilesFoward = GameObject.Find ("Ground_" + addZeroX + "" + player_X + "" + addZeroY + "" + player_Y);
		this.SetObjetive();
	}

	private void SetObjetive()
	{
		if (twoTilesFoward == null)
			return;

		Vector3 vector = this.twoTilesFoward.transform.position - this.blinky.transform.position;
		vector = vector * 2;
		this.objetive = this.GetGroundNextVector (vector);
		this.GetAroundTiles ();
	}

	private GameObject GetGroundNextVector(Vector3 pos)
	{
	
		float betterDistance = 999;
		GameObject theObjetive = this.gameObject;
		for(int i=0; i<CreateTiles.allGrounds.Count; i++)
		{
			for(int j=0; j<CreateTiles.allGrounds[i].Count; j++)
			{
				float distance = Vector3.Distance(pos, CreateTiles.allGrounds[i][j].transform.position);
				if(distance < betterDistance)
				{
					betterDistance = distance;
					theObjetive = CreateTiles.allGrounds[i][j];
				}
			}
		}
		
		return theObjetive;

	}
	void Update()
	{		
		if (this.ghostBehaviour == GHOSTBEHAVIOUR.NONE)
			return;
		else if(this.ghostBehaviour != GHOSTBEHAVIOUR.NONE && this.started == false)
		{
			this.started = true;
			this.SetTwoTilesFoward ();
		}



		if (this.nexTile == null)
			return;
		
		
		if(Vector3.Distance(this.transform.position, this.nexTile.transform.position) < 0.01f)
		{
			this.name = "Inky_" + this.nexTile.name.Substring (this.nexTile.name.Length - 4, 2) + "" +  this.nexTile.name.Substring (this.nexTile.name.Length - 2, 2);
			if (this.ghostBehaviour == GHOSTBEHAVIOUR.CHASE)
				this.SetTwoTilesFoward ();
			else if (this.ghostBehaviour == GHOSTBEHAVIOUR.SCATTER) 
			{
				this.objetive = this.scatterTile;
				GetAroundTiles ();
			}
			else if(this.ghostBehaviour == GHOSTBEHAVIOUR.FRIGHTENED)
			{
				this.Frightened();
				GetComponent<SpriteRenderer>().material = frigh;
				this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, -2);
			}
			//this.SetTwoTilesFoward ();
			//this.GetAroundTiles();
		}
		
		this.transform.position = Vector3.MoveTowards (this.transform.position, nexTile.transform.position, Time.deltaTime * speed);
	}
	
	public void GetAroundTiles()
	{
		this.posX = this.name.Substring (this.name.Length - 4, 2);
		this.posY = this.name.Substring (this.name.Length - 2, 2);


		int index_X = int.Parse (posX);
		int index_Y = int.Parse (posY);
		
		string addZeroX =  (index_X <= 9) ? "0" : "";
		string addZeroY =  (index_Y <= 9) ? "0" : "";
		
		GameObject upTile =  GameObject.Find ("Ground_"  + ((index_X + 1 <= 9) ? "0" : "") + (index_X + 1) + addZeroY + index_Y );
		GameObject downTile =  GameObject.Find ("Ground_"  + ((index_X - 1 <= 9) ? "0" : "") + ((index_X) - 1) + "" + addZeroY + index_Y );
		GameObject rightTile =  GameObject.Find ("Ground_"  + addZeroX + index_X + ((index_Y + 1 <= 9) ? "0" : "") + (index_Y + 1));
		GameObject leftTile =  GameObject.Find ("Ground_"  + addZeroX + index_X + ((index_Y-1 <=9) ? "0" : "") + (index_Y - 1));
		
		if(index_Y == 27)
		{
			rightTile = GameObject.Find("Ground_1800"); 
		}
		if (upTile.tag.Equals(Tags.ground) && upTile != lastTile)
		{
			openList.Add(upTile);
		}
		if(downTile.tag.Equals(Tags.ground) && downTile != lastTile)
		{
			openList.Add(downTile);
		}
		if (rightTile.tag.Equals(Tags.ground) && rightTile != lastTile)
		{
			openList.Add(rightTile);
		}
		if (leftTile.tag.Equals(Tags.ground) && leftTile != lastTile)
		{
			openList.Add(leftTile);
		}

		
		this.GoToBetterTile ();
	}
	
	private void GoToBetterTile()
	{
		GameObject betterTile = openList[0];
		float betterDistance = 999;
		
		for(int i = 0; i<this.openList.Count; i++)
		{
			float distance = Vector3.Distance(openList[i].transform.position, objetive.transform.position);
			if(distance < betterDistance)
			{
				betterDistance = distance;
				betterTile = this.openList[i];
			}
		}
		
		this.openList = new List<GameObject> ();
		
		if (nexTile == null)
		{
			this.nexTile = betterTile;
			this.lastTile = this.nexTile;
		}
		else if (betterTile != nexTile)
		{
			this.lastTile = this.nexTile;
			this.nexTile = betterTile;
		}

	}
}
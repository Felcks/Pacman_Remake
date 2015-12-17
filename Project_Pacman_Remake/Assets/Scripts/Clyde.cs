using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clyde : Ghost 
{

	public List<GameObject> openList;
	public List<GameObject> closedList;
	public List<GameObject> rightWay;
	public GameObject player;
	public GameObject objetive;
	public GameObject nexTile;
    public GameObject lastTile;
    public bool savedLastTile;
	public Vector3 currentDirection;

	private string posX, posY;
	private string playerX, playerY;
	private bool started;
		
	public Material frigh;
	public Material defaultMat;

	void Start()
	{
		this.ghostBehaviour = GHOSTBEHAVIOUR.CHASE;
        this.scatterTile = GameObject.Find("Ground_0006");

		this.ghostBehaviour = GHOSTBEHAVIOUR.NONE;
		this.player = GameObject.FindWithTag (Tags.player);

		openList = new List<GameObject> ();
		closedList = new List<GameObject> ();
		rightWay = new List<GameObject> ();

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

	private void CheckDistanceToPlayer()
	{
		this.posX = this.name.Substring (this.name.Length - 4, 2);
		this.posY = this.name.Substring (this.name.Length - 2, 2);
		this.playerX = this.player.name.Substring (this.player.name.Length - 4, 2);
		this.playerY = this.player.name.Substring (this.player.name.Length - 2, 2);

		int distance = Mathf.Abs (int.Parse (this.posX) - int.Parse (this.playerX));
		distance += Mathf.Abs (int.Parse (this.posY) - int.Parse (this.playerY));

		if(distance > 8 && this.ghostBehaviour != GHOSTBEHAVIOUR.FRIGHTENED)
		{
			this.ghostBehaviour = GHOSTBEHAVIOUR.CHASE;
			this.objetive = this.player;
			this.GetAroundTiles();
		}
		else if(distance <= 8 && this.ghostBehaviour != GHOSTBEHAVIOUR.FRIGHTENED)
		{
			this.ghostBehaviour = GHOSTBEHAVIOUR.FSCATTER;
            this.objetive = this.scatterTile;
            this.GetAroundTiles();
		}
	}
	
	void Update()
	{

		if (this.ghostBehaviour == GHOSTBEHAVIOUR.NONE)
			return;
		else if(this.ghostBehaviour != GHOSTBEHAVIOUR.NONE && this.started == false)
		{
			this.started = true;
			CheckDistanceToPlayer ();
		}

		if (this.nexTile == null)
			return;
        

		if (Vector3.Distance (this.transform.position, this.nexTile.transform.position) < 0.05f) 
        {
			this.name = "Clyde_" + this.nexTile.name.Substring(this.nexTile.name.Length - 4, 2) + "" + this.nexTile.name.Substring(this.nexTile.name.Length - 2, 2);
			if(this.ghostBehaviour == GHOSTBEHAVIOUR.CHASE)
			{
				this.CheckDistanceToPlayer ();
			}
			else if(ghostBehaviour == GHOSTBEHAVIOUR.FSCATTER)
			{
				this.objetive = this.scatterTile;
				this.GetAroundTiles();
			}
			else if(ghostBehaviour == GHOSTBEHAVIOUR.SCATTER)
			{
				this.objetive = this.scatterTile;
				this.GetAroundTiles();
			}
			else if(this.ghostBehaviour == GHOSTBEHAVIOUR.FRIGHTENED)
			{
				print ("uaheuahueahah");
				this.Frightened();
				GetComponent<SpriteRenderer>().material = frigh;
				this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, -2);
			}
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
        if (downTile.tag.Equals(Tags.ground) && downTile != lastTile)
		{
			openList.Add(downTile);
		}
        if (rightTile.tag.Equals(Tags.ground) && rightTile != lastTile)
		{
			openList.Add(rightTile);
		}
        if (leftTile.tag.Equals(Tags.ground) && leftTile != lastTile )
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

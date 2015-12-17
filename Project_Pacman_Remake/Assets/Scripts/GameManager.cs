using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public List<GameObject> tiles;
	public GameObject player;
	public GameObject blinky;
	public GameObject inky;
	public GameObject pinky;
	public GameObject clyde;
	public GameObject ready;
	public bool started;
	public PlayerController playerController;
	public GameObject[][] ground;
	private float[][]blocos;
	private float count;
	private float fightnedCount;
	GameObject b,i,pi,c;
	private bool startClyde;
	private bool clydeReady;
	private bool startInky;
	private bool inkyReady;
	private bool pinkyReady;
	private bool startedGame;
	private bool change1;
	private bool change2;
	private bool change3;
	private bool startFightened;

	void Start()
	{
		this.ground = new GameObject[28][];
		for(int i=0; i<this.ground.Length;i++)
		{
			this.ground[i] = new GameObject[36];
			for(int j=0; j<this.ground[i].Length; j++)
				this.ground[i][j] = GameObject.Find("GameManager"); //Preenche para nao dar erro!
		}

	}

	void Update()
	{
		StartGame ();
		this.count += Time.deltaTime;
		StartPinky ();
		StartInky ();
		StartClyde ();

		if (startFightened)
			StartFightened ();

		if(this.count > 3 && this.startedGame == false)
		{
			this.playerController.StartGame();
			this.b.GetComponent<Blinky>().ghostBehaviour = GHOSTBEHAVIOUR.SCATTER;
			Destroy(this.ready);
			startedGame = true;
		}
		if(count > 6 && startInky == false)
		{
			startInky = true;
		}
		if(count > 10 && change1 == false)
		{
			this.b.GetComponent<Blinky>().ghostBehaviour = GHOSTBEHAVIOUR.CHASE;
			this.pi.GetComponent<Pinky>().ghostBehaviour = GHOSTBEHAVIOUR.CHASE;
			this.i.GetComponent<Inky>().ghostBehaviour = GHOSTBEHAVIOUR.CHASE;
			change1 = true;
		}  
		if (count > 30 && change2 == false) 
		{
			this.startClyde = true;
			this.b.GetComponent<Blinky>().ghostBehaviour = GHOSTBEHAVIOUR.SCATTER;
			this.pi.GetComponent<Pinky>().ghostBehaviour = GHOSTBEHAVIOUR.SCATTER;
			this.i.GetComponent<Inky>().ghostBehaviour = GHOSTBEHAVIOUR.SCATTER;
			change2 = true;
		}
		if(count > 37 && change3 == false)
		{
			this.b.GetComponent<Blinky>().ghostBehaviour = GHOSTBEHAVIOUR.CHASE;
			this.pi.GetComponent<Pinky>().ghostBehaviour = GHOSTBEHAVIOUR.CHASE;
			this.i.GetComponent<Inky>().ghostBehaviour = GHOSTBEHAVIOUR.CHASE;
			this.c.GetComponent<Clyde>().ghostBehaviour = GHOSTBEHAVIOUR.CHASE;
			change3 = true;
		}  
	}

	public void StartFightened()
	{
		if (this.fightnedCount == 0)
		{
			fightnedCount = this.count;
			startFightened = true;
		}

		if(fightnedCount + 7 < this.count)
		{
			fightnedCount = 0;
			startFightened = false;

			this.b.GetComponent<Blinky>().ghostBehaviour = GHOSTBEHAVIOUR.CHASE;
			this.pi.GetComponent<Pinky>().ghostBehaviour = GHOSTBEHAVIOUR.CHASE;
			this.i.GetComponent<Inky>().ghostBehaviour = GHOSTBEHAVIOUR.CHASE;
			this.c.GetComponent<Clyde>().ghostBehaviour = GHOSTBEHAVIOUR.CHASE;

			this.b.transform.position = new Vector3(b.transform.position.x,b.transform.position.y,0);
			this.b.GetComponent<SpriteRenderer>().material = this.b.GetComponent<Blinky>().defaultMat;
			this.i.transform.position = new Vector3(i.transform.position.x,i.transform.position.y,0);
			this.i.GetComponent<SpriteRenderer>().material = this.i.GetComponent<Inky>().defaultMat;
			this.pi.transform.position = new Vector3(pi.transform.position.x,pi.transform.position.y,0);
			this.pi.GetComponent<SpriteRenderer>().material = this.pi.GetComponent<Pinky>().defaultMat;
			this.c.transform.position = new Vector3(c.transform.position.x,c.transform.position.y,0);
			this.c.GetComponent<SpriteRenderer>().material = this.c.GetComponent<Clyde>().defaultMat;

			 
			return;
		}

		this.b.GetComponent<Blinky> ().ghostBehaviour = GHOSTBEHAVIOUR.FRIGHTENED;
		this.pi.GetComponent<Pinky> ().ghostBehaviour = GHOSTBEHAVIOUR.FRIGHTENED;

		if(this.inkyReady)
			this.i.GetComponent<Inky> ().ghostBehaviour = GHOSTBEHAVIOUR.FRIGHTENED;
		if(this.clydeReady)
			this.c.GetComponent<Clyde> ().ghostBehaviour = GHOSTBEHAVIOUR.FRIGHTENED;
	}

	private void StartClyde()
	{
		if(startClyde && clydeReady == false)
		{
			c.transform.position = Vector3.MoveTowards(c.transform.position,CreateTiles.allGrounds[21][13].transform.position,Time.deltaTime);
			
			if(Vector3.Distance(c.transform.position,CreateTiles.allGrounds[21][13].transform.position) < 0.05f)
			{
				clydeReady = true;
				this.c.GetComponent<Clyde>().ghostBehaviour = GHOSTBEHAVIOUR.SCATTER;
			}
		}
	}
	private void StartInky()
	{
		if(startInky && inkyReady == false)
		{
			i.transform.position = Vector3.MoveTowards(i.transform.position,CreateTiles.allGrounds[21][13].transform.position,Time.deltaTime);
			
			if(Vector3.Distance(i.transform.position,CreateTiles.allGrounds[21][13].transform.position) < 0.05f)
			{
				inkyReady = true;
				this.i.GetComponent<Inky>().ghostBehaviour = GHOSTBEHAVIOUR.SCATTER;
			}
		}
	}

	private void StartPinky()
	{
		if(startedGame && pinkyReady == false)
		{
			pi.transform.position = Vector3.MoveTowards(pi.transform.position,CreateTiles.allGrounds[21][13].transform.position,Time.deltaTime);

			if(Vector3.Distance(pi.transform.position,CreateTiles.allGrounds[21][13].transform.position) < 0.1f)
			{
				pinkyReady = true;
				this.pi.GetComponent<Pinky>().ghostBehaviour = GHOSTBEHAVIOUR.SCATTER;
			}
		}
	}

	void StartGame()
	{
		if(this.tiles.Count == 28*36 && !this.started) //Se todos os Tiles foram instanciados
		{
			this.started = true;
			GameObject p = (GameObject)Instantiate(this.player,CreateTiles.allGrounds[15][13].transform.position,Quaternion.identity);
			this.playerController = p.GetComponent<PlayerController>();
			this.playerController.objetiveIndex = this.tiles.Count/2 + 10;
			this.playerController.gameManager = this;
			this.playerController.score = this.GetComponent<ScoreManager>();

			 b = (GameObject)Instantiate(this.blinky,CreateTiles.allGrounds[21][13].transform.position,Quaternion.identity);
			 i = (GameObject)Instantiate(this.inky,CreateTiles.allGrounds[18][11].transform.position,Quaternion.identity);
			 pi = (GameObject)Instantiate(this.pinky,CreateTiles.allGrounds[18][13].transform.position,Quaternion.identity);
			 c = (GameObject)Instantiate(this.clyde,CreateTiles.allGrounds[18][15].transform.position,Quaternion.identity);
			b.gameObject.name = "Blinky_" + 21 + "" + 13;
			i.gameObject.name = "Inky_" + 21 + "" + 13;
			pi.gameObject.name = "Pinky_" + 21 + "" + 13; 
			c.gameObject.name = "Clyde_" + 21 + "" + 13;
		}
	}
}



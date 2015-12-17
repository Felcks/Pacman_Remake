using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateTiles : MonoBehaviour
{
	private GameManager gameManager;
	public static List<List<GameObject>> allGrounds;
	public GameObject blackTile;
	public GameObject powerUp;
	public GameObject normalFood;
	public GameObject decisionTile;
	public GameObject continuosTile;

	public GameObject groundTiles;
	public GameObject decisionTiles;
	public GameObject continuosTiles;
	public GameObject normalFoods;
	public GameObject powerUps;
    public GameObject extraSideTiles;


	private int amountHorizontalTiles = 28;
	private int amountVerticalTiles = 36;


	void Start()
	{
		this.gameManager = this.GetComponent<GameManager> ();
		this.gameManager.tiles = new List<GameObject> ();
		allGrounds = new List<List<GameObject>> ();
	}

	void Update()
	{
		if(this.gameManager.ground[this.gameManager.ground.Length -1] != null && this.gameManager.tiles.Count == 0)
		{
			this.InstantiateTiles();
		}
	}

	void DefineType(GameObject b, int i, int j)
	{


	}

	void InstantiateTiles()
	{
		Vector3 worldStartPos = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 0));
		Vector3 worldEndPos = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));

		float width = 10f / amountVerticalTiles;//18f / amountHorizontalTiles;
		float height = 10f / amountVerticalTiles;
		
		this.blackTile.transform.localScale = new Vector3 (width, height, 1);

		for (int i=0; i<MapCode.getInstance().map.Length; i++) 
		{
			allGrounds.Add(new List<GameObject>());
			for (int j=0; j<MapCode.getInstance().map[i].Length; j++)
			{

				GameObject b = (GameObject)Instantiate (this.blackTile, new Vector3 (width * j + width / 2 - worldEndPos.x / 2 + ((width * amountHorizontalTiles) / 8),
					                                                                  height * i + height / 2 - worldEndPos.y, 0),
					                                       Quaternion.identity);
				string addZeroX =  (i <= 9) ? "0" : "";
				string addZeroY =  (j <= 9) ? "0" : "";
				b.name = "Ground_" + addZeroX + i + addZeroY + j;
				b.transform.parent = this.groundTiles.transform;
				allGrounds[allGrounds.Count-1].Add(b);

				if(MapCode.getInstance ().map [MapCode.getInstance().map.Length - (1 + i)] [j] == 10 )
				{
					b.tag = Tags.wall;
				}
				if (MapCode.getInstance ().map [MapCode.getInstance().map.Length - (1 + i)] [j] == 1)
				{
					b.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/Wall1");
					b.AddComponent<BoxCollider2D>();
					b.tag = Tags.wall;
				}
				 if (MapCode.getInstance ().map [MapCode.getInstance().map.Length - (1 + i)] [j] == 2 ||
				     MapCode.getInstance ().map [MapCode.getInstance().map.Length - (1 + i)] [j] == 6 ||
				     MapCode.getInstance ().map [MapCode.getInstance().map.Length - (1 + i)] [j] == 9)
				{
					GameObject p = (GameObject)Instantiate (this.powerUp, new Vector3 (width * j + width / 2 - worldEndPos.x / 2 + ((width * amountHorizontalTiles) / 8),
					                                          height * i + height / 2 - worldEndPos.y), Quaternion.identity);
					p.transform.parent = powerUps.transform;
				}
				 if (MapCode.getInstance ().map [MapCode.getInstance().map.Length - (1 + i)] [j] == 0 ||
				    MapCode.getInstance ().map [MapCode.getInstance().map.Length - (1 + i)] [j] ==  4 ||
				    MapCode.getInstance ().map [MapCode.getInstance().map.Length - (1 + i)] [j] == 7)
				{
					GameObject n = (GameObject)Instantiate (this.normalFood, new Vector3 (width * j + width / 2 - worldEndPos.x / 2 + ((width * amountHorizontalTiles) / 8),
					                                        height * i + height / 2 - worldEndPos.y), Quaternion.identity);
					n.transform.parent = normalFoods.transform;
				}
				 if (MapCode.getInstance ().map [MapCode.getInstance().map.Length - (1 + i)] [j] == 4 ||
				     MapCode.getInstance ().map [MapCode.getInstance().map.Length - (1 + i)] [j] == 5 ||
				     MapCode.getInstance ().map [MapCode.getInstance().map.Length - (1 + i)] [j] == 6)
				{
					GameObject d = (GameObject)Instantiate (this.decisionTile, new Vector3 (width * j + width / 2 - worldEndPos.x / 2 + ((width * amountHorizontalTiles) / 8),
					                                           height * i + height / 2 - worldEndPos.y), Quaternion.identity);

					d.name = "DecisionTile_" + addZeroX + i + addZeroY + j;
					d.transform.parent = decisionTiles.transform;
				}
                 if (MapCode.getInstance().map[MapCode.getInstance().map.Length - (1 + i)][j] == 7 ||
                    MapCode.getInstance().map[MapCode.getInstance().map.Length - (1 + i)][j] == 8 ||
                    MapCode.getInstance().map[MapCode.getInstance().map.Length - (1 + i)][j] == 9)
                 {
                     GameObject d = (GameObject)Instantiate(this.continuosTile, new Vector3(width * j + width / 2 - worldEndPos.x / 2 + ((width * amountHorizontalTiles) / 8),
                                                                                             height * i + height / 2 - worldEndPos.y), Quaternion.identity);

                     d.name = "ContinuosTile_" + addZeroX + i + addZeroY + j;
                     d.transform.parent = continuosTiles.transform;
                 }
                
				this.gameManager.tiles.Add (b);
			}

            //Criar tiles Extras!!!__!!
            GameObject extra = (GameObject)Instantiate(this.blackTile, new Vector3(width * 28 + width / 2 - worldEndPos.x / 2 + ((width * amountHorizontalTiles) / 8),
                                                                                      height * 18 + height / 2 - worldEndPos.y, 0),
                                                           Quaternion.identity);
            GameObject extra2 = (GameObject)Instantiate(this.blackTile, new Vector3(width * 28 + width / 2 - worldEndPos.x / 2 + ((width * amountHorizontalTiles) / 8),
                                                                                      height * 18 + height / 2 - worldEndPos.y, 0),
                                                           Quaternion.identity);
            extra.transform.parent = this.extraSideTiles.transform;
            extra2.transform.parent = this.extraSideTiles.transform;
		}
	}

}

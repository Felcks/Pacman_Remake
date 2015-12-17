using UnityEngine;
using System.Collections;

public class DecisionTile : MonoBehaviour
{
	public string posX;
	public string posY;

	//Up, Righ, Down, Left
	public bool[] canTurn;

	void Start()
	{
		this.canTurn = new bool[4];
		this.posX = this.name.Substring (this.name.Length - 4, 2);
		this.posY = this.name.Substring (this.name.Length - 2, 2);

		int index_X = int.Parse (posX);
		int index_Y = int.Parse (posY);

		string addZeroX =  (index_X <= 9) ? "0" : "";
		string addZeroY =  (index_Y <= 9) ? "0" : "";

		GameObject upTile =  GameObject.Find ("Ground_"  + ((index_X + 1 <= 9) ? "0" : "") + (index_X + 1) + addZeroY + index_Y );
		GameObject downTile =  GameObject.Find ("Ground_" + addZeroX + ((index_X) - 1) + "" + addZeroY + index_Y );
		GameObject rightTile =  GameObject.Find ("Ground_" + addZeroX + index_X + ((index_Y + 1 <= 9) ? "0" : "") + (index_Y + 1));
		GameObject leftTile =  GameObject.Find ("Ground_" + addZeroX + index_X + addZeroY + (index_Y - 1));

		if (upTile.tag.Equals (Tags.ground))
			canTurn[0] = true;
		if (rightTile.tag.Equals (Tags.ground))
			canTurn[1] = true;
		if (downTile.tag.Equals (Tags.ground))
			canTurn[2] = true;
		if (leftTile.tag.Equals (Tags.ground))
			canTurn[3] = true;
	}
}

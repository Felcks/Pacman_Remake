using UnityEngine;
using System.Collections;

public class ContinuosTile : MonoBehaviour 
{
	public string posX;
	public string posY;	
	public DIRECTION goDirection1;
	public DIRECTION goDirection2;
	
	void Start()
	{
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

		bool goDirection1Seted = false;
		if (upTile.tag.Equals (Tags.ground))
		{
			goDirection1 = DIRECTION.UP;
			goDirection1Seted = true;
		} 
		if (rightTile.tag.Equals (Tags.ground))
		{
			if(goDirection1Seted)
				goDirection2 = DIRECTION.RIGHT;
			else
			{
				goDirection1 = DIRECTION.RIGHT;
				goDirection1Seted = true;
			}
		} 
		if (downTile.tag.Equals (Tags.ground))
		{
			if(goDirection1Seted)
				goDirection2 = DIRECTION.DOWN;
			else
			{
				goDirection1 = DIRECTION.DOWN;
				goDirection1Seted = true;
			}
		}
		if (leftTile.tag.Equals (Tags.ground))
		{
			goDirection2 = DIRECTION.LEFT;
		}
	}
}

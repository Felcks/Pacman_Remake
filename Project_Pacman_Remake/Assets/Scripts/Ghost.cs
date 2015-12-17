using UnityEngine;
using System.Collections;

public enum GHOSTBEHAVIOUR { CHASE, SCATTER , FRIGHTENED, FSCATTER, NONE};

public class Ghost : MonoBehaviour 
{
	public GHOSTBEHAVIOUR ghostBehaviour;
	protected GameObject scatterTile;
	protected float speed = 1.5f;
}

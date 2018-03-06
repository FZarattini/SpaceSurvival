using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageWrap : MonoBehaviour {

	//public Bounds bounds = new Bounds();
	private Vector2 size;
	private Vector2 localOffset;

	public Vector2 CornerUpLeft{ get{ return getCorner (-1, -1); } }
	public Vector2 CornerUpRight{ get{ return getCorner (1, -1); } }
	public Vector2 CornerDownLeft{ get{ return getCorner (-1, -1); } }
	public Vector2 CornerDownRight{ get{ return getCorner (-1, -1); } }

	public Bounds Bounds{
		get{
			return new Bounds (transform.position + (Vector3)localOffset, (Vector3)size);
		}
	}

	private Vector2 getCorner(int horizontalDirection, int verticalDirection){
		return (Vector2)transform.position + localOffset + new Vector2 (horizontalDirection*size.x, verticalDirection*size.y);
	}

	void Awake(){
		Bounds bounds = new Bounds();
		SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer> ();
		foreach (SpriteRenderer renderer in renderers) {
			bounds.Encapsulate (renderer.bounds);
		}
		localOffset = bounds.center - transform.position;
		size = (Vector2)bounds.size;

	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

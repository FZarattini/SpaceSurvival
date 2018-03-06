using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWrap : MonoBehaviour {

	public StageWrap stageWrap;
    public LayerMask verticalLimitMask;
    public LayerMask horizontalLimitMask;
	private Camera camera;

	void Awake(){
		camera = GetComponent<Camera> ();
	}

	private void OnDrawGizmos(){
		Bounds bounds = stageWrap.Bounds;
		Gizmos.DrawWireCube (bounds.center, bounds.size);
    }

	public void ClampCamera(){
		if (stageWrap == null)
			return;
		Bounds bounds = stageWrap.Bounds;
		Vector2 min = bounds.min;
		Vector2 max = bounds.max;
		Vector3 pos = transform.position;
		float height = camera.orthographicSize * 2f;
		float width = height * camera.aspect;
		pos.x = Mathf.Clamp (pos.x, min.x+width/2, max.x-width/2);
		pos.y = Mathf.Clamp (pos.y, min.y+height/2, max.y-height/2);
		transform.position = pos;
	}

    public void CheckForLimits()
    {
        Bounds cameraBounds = OrthographicBounds(camera);

        float leftBorder = cameraBounds.center.x - cameraBounds.extents.x;
        float rightBorder = cameraBounds.center.x + cameraBounds.extents.x;
        float topBorder = cameraBounds.center.y + cameraBounds.extents.y;
        float bottomBorder = cameraBounds.center.y - cameraBounds.extents.y;

        //LeftCast
        RaycastHit2D leftRay = Physics2D.Raycast(new Vector2(leftBorder, topBorder), Vector2.down, cameraBounds.extents.y * 2, horizontalLimitMask);
        if(leftRay.collider != null)
        {
            float size = leftRay.collider.transform.position.x;
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, size + cameraBounds.extents.x, pos.x);
            transform.position = pos;
        }

        //RigthCast
        RaycastHit2D rightRay = Physics2D.Raycast(new Vector2(rightBorder, topBorder), Vector2.down, cameraBounds.extents.y * 2, horizontalLimitMask);
        if (rightRay.collider != null)
        {
            float size = rightRay.collider.transform.position.x;
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, pos.x, size - cameraBounds.extents.x);
            transform.position = pos;
        }

        //TopCast 
        RaycastHit2D topRay = Physics2D.Raycast(new Vector2(leftBorder, topBorder), Vector2.right, cameraBounds.extents.x * 2, verticalLimitMask);
        if (topRay.collider != null)
        {
            float size = topRay.collider.transform.position.y;
            Vector3 pos = transform.position;
            pos.y = Mathf.Clamp(pos.y, pos.y, size - cameraBounds.extents.y);
            transform.position = pos;
        }

        //BottomCast
        RaycastHit2D bottomRay = Physics2D.Raycast(new Vector2(leftBorder, bottomBorder), Vector2.right, cameraBounds.extents.x * 2, verticalLimitMask);
        if (bottomRay.collider != null)
        {
            float size = bottomRay.collider.transform.position.y;
            Vector3 pos = transform.position;
            pos.y = Mathf.Clamp(pos.y, size + cameraBounds.extents.y, pos.x);
            transform.position = pos;
        }
    }

    private Bounds OrthographicBounds(Camera camera)
    {
        float vertExtend = camera.orthographicSize * 2;
        float horzExtend = vertExtend * camera.aspect;
        Bounds bounds = new Bounds(
            camera.transform.position,
            new Vector3(horzExtend, vertExtend, 0));
        return bounds;
    }

    void Update(){

    }

    void LateUpdate(){
	}
}

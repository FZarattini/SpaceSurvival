using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public enum CameraStates
    {
        Follow
    }

	public Transform target;
    public float zoomSpeed;
    public float targetSize;

    private CameraStates currentState = CameraStates.Follow;
    private float vel_x;
	private float vel_y;
    private Transform originalTarget;
    private Camera _camera;
    private CameraWrap _cameraWrap;

    private float x_lock = 0;
    private float y_lock = 0;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _cameraWrap = GetComponent<CameraWrap>();
        originalTarget = target;
        targetSize = _camera.orthographicSize;
    }

    // Update is called once per frame
    void Update () {
        switch (currentState)
        {
            case CameraStates.Follow:
                followBehaviour();
                break;
        }

        if (_camera.orthographicSize != targetSize)
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, targetSize, zoomSpeed * Time.deltaTime);

        _cameraWrap.ClampCamera();
        _cameraWrap.CheckForLimits();
    }

    private void followBehaviour()
    {
        Vector3 pos = transform.position;
        Vector3 t_pos = target.position;

        if (x_lock != 0)
            t_pos.x = x_lock;
        if (y_lock != 0)
            t_pos.y = y_lock;

        pos.x = Mathf.SmoothDamp(pos.x, t_pos.x, ref vel_x, 0.2f); //t_pos.x;
        pos.y = Mathf.SmoothDamp(pos.y, t_pos.y, ref vel_y, 0.2f); //t_pos.y;
        transform.position = pos;
    }

    public void changeTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void changeCameraZoom(float zoom)
    {
        targetSize = zoom;
    }

    public void resetTarget()
    {
        target = originalTarget;
    }

    public void xLock(float place)
    {
        x_lock = place;
    }

    public void yLock(float place)
    {
        y_lock = place;
    }

    public void resetLocks()
    {
        x_lock = 0;
        y_lock = 0;
    }
}

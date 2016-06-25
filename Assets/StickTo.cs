using UnityEngine;
using System.Collections;

public class StickTo : MonoBehaviour {

    public Transform target;

    private Camera targetCam;
    private Camera cam;

	// Use this for initialization
	void Start () {
        cam = gameObject.GetComponent<Camera>();
        targetCam = target.gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = target.position;
        transform.rotation = target.rotation;

        if (cam != null && targetCam != null)
        {
            cam.aspect = targetCam.aspect;
            cam.fieldOfView = targetCam.fieldOfView;
            cam.fov = targetCam.fov;
            cam.projectionMatrix = targetCam.projectionMatrix;
            cam.worldToCameraMatrix = targetCam.worldToCameraMatrix;
        }
	}
}

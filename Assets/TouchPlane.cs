using UnityEngine;
using System.Collections;
using System;

public class TouchPlane : MonoBehaviour {

    public delegate void TouchEvent(float x, float y);
    
    public TouchEvent onTouchStart;
    public TouchEvent onTouchMove;
    public TouchEvent onTouchEnd;

    private bool touchInProgress = false;
    private float lastTouchX = 0f;
    private float lastTouchY = 0f;

    public void Reset()
    {
        touchInProgress = false;
        onTouchStart = null;
        onTouchMove = null;
        onTouchEnd = null;
    }

	// Update is called once per frame
	void Update () {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 5);

        float deltaX = hit.point.x - lastTouchX;
        float deltaY = hit.point.y - lastTouchY;

        if (touchInProgress)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (onTouchEnd != null) { onTouchEnd(deltaX, deltaY); }
                touchInProgress = false;
            }
            else 
            {
                if (onTouchMove != null) { onTouchMove(deltaX, deltaY); }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (onTouchStart != null) { onTouchStart(hit.point.x, hit.point.y); }
                touchInProgress = true;
            }
        }

        lastTouchX = hit.point.x;
        lastTouchY = hit.point.y;
	}

}

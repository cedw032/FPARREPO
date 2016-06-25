using UnityEngine;
using System.Collections;

public class TouchZoomer : MonoBehaviour {

    public TouchCatcher touchCatcher;

    private bool wasZooming = false;
    private float lastTouchDistance = 0f;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!State.vr) { return; } 

        if (touchCatcher.DoubleTouch)
        {
            float touchDistance = (touchCatcher.PositionOne - touchCatcher.PositionTwo).magnitude;

            if (wasZooming)
            {
                float deltaDistance = touchDistance - lastTouchDistance;
                Vector3 scale = transform.localScale;
                scale.x = scale.y = scale.z = Mathf.Min(Mathf.Max(1f, scale.z + deltaDistance / 100f), 2.64f);
                transform.localScale = scale;
            }

            lastTouchDistance = touchDistance;
            wasZooming = true;
        } 
        else 
        {
            wasZooming = false;
        }
	}

    public void Reset()
    {
        transform.localScale = Vector3.one;
    }
}

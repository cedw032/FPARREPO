using UnityEngine;
using System.Collections;

public class RenderCircleWithLineRenderer : MonoBehaviour {

    public LineRenderer lineRenderer;
    public float radius;

    private int positionCount = 15;

    private Vector3 v = new Vector3();

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        lineRenderer.useWorldSpace = false;
        for (int i = 0; i < positionCount; ++i)
        {
            v.x = Mathf.Cos((float)i / (float)(positionCount - 2) * 2 * Mathf.PI) * radius;
            v.y = Mathf.Sin((float)i / (float)(positionCount - 2) * 2 * Mathf.PI) * radius;
            lineRenderer.SetPosition(i, v);
        }
	}
}

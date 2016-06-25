using UnityEngine;
using System.Collections;

public class RubberBandToTarget : MonoBehaviour {

    public Transform target;
    public float strength;

    public Vector3 offset = new Vector3(-3.145f, 0f, 0f);

    private Vector3 pos;

    public Vector3 posScaleMod = Vector3.one;

	// Use this for initialization
	void Start () {
        transform.position = pos = target.position + offset;
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            pos = pos + (target.position - pos) * strength;
            Vector3 temp = new Vector3(offset.x * posScaleMod.x, offset.y * posScaleMod.y, offset.z * posScaleMod.z);
            transform.position = pos + temp;
        }
	}
}

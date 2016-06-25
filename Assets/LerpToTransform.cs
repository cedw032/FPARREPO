using UnityEngine;
using System.Collections;

public class LerpToTransform : MonoBehaviour {

    public Transform target;
    public float tension;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (target != null)
        {
            transform.position = (target.position - transform.position) * tension + transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, tension);
            transform.localScale = (target.localScale - transform.localScale) * tension + transform.localScale;
        }
    }
}

using UnityEngine;
using System.Collections;

public class DepthSortChildren : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 camPos = Camera.main.transform.position;
        for (int i = 1; i < transform.childCount; ++i)
        {
            Transform a = transform.GetChild(i - 1);
            Transform b = transform.GetChild(i);

            float distA = (camPos - a.position).sqrMagnitude;
            float distB = (camPos - b.position).sqrMagnitude;

            if (distA < distB)
            {
                b.SetSiblingIndex(i - 1);
            }
        }
	}
}

using UnityEngine;
using System.Collections;

public class ProbeCoverAdjuster : MonoBehaviour {

    bool wasChamberFocus = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (State.focus == State.Focus.CHAMBER)
        {
            if (!wasChamberFocus)
            {
                wasChamberFocus = true;

                Vector3 scale = transform.localScale;
                scale.z *= 0.5f;
                transform.localScale = scale;

                Vector3 pos = transform.localPosition;
                pos.z -= 0.004f;
                transform.localPosition = pos;
            }
        }
        else
        {
            if (wasChamberFocus)
            {
                wasChamberFocus = false;

                Vector3 scale = transform.localScale;
                scale.z *= 2f;
                transform.localScale = scale;

                Vector3 pos = transform.localPosition;
                pos.z += 0.004f;
                transform.localPosition = pos;
            }
        }
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HideIfNotSelected : MonoBehaviour {

    public Callout callout;
    public Text target;

    private bool wasSelected = true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (callout.selected != wasSelected)
        {
            wasSelected = target.enabled = callout.selected;
        }
	}
}

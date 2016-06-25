using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HideIfNotMain : MonoBehaviour {

    private Image image;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (State.focus == State.Focus.MAIN)
        {
            Color col = image.color;
            col.a = Mathf.Min(col.a + Time.deltaTime * 2f, 1f);
            image.color = col;
        }
        else
        {
            Color col = image.color;
            col.a = Mathf.Max(col.a - Time.deltaTime * 2f, 0f);
            image.color = col;
        }
	}
}

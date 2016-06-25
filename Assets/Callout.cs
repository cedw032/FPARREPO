using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Callout : MonoBehaviour {

    public static List<Callout> callouts = new List<Callout>();
    public static Callout selectedCallout = null;

    public State.Focus focusGroup;
    public State.Focus switchTo;

    public CanvasGroup canvasGroup;

    public bool selected
    {
        get;
        private set;
    }

    public TouchRotator touchRotator;
    public TouchZoomer touchZoomer;

    public LineRenderer line;
    public Transform anchor;
    public Transform target;

    public RenderCircleWithLineRenderer circle;
    public float uiDistance;

    public CanvasGroup alphaController;

    private Transform model;
    private Transform destination;
    private float initialLineLength = 0f;
    private Vector3 v = new Vector3();
    //private Vector3 v2 = new Vector3();

    public GameObject backing;

    public Transform bottomLeft;
    public Transform bottomRight;

    public Transform circleLeft;
    public Transform circleRight;

    public RubberBandToTarget band;

	// Use this for initialization
	void Start () {

        band = GetComponent<RubberBandToTarget>();

        selected = false;

        Disable();

        callouts.Add(this);

        model = GameObject.Find("Model").transform;
        destination = band.target;

        if (line != null)
        {
            line.SetPosition(0, target.position);
            line.SetPosition(1, anchor.position);

            initialLineLength = (destination.position - model.position).magnitude;
        }

    }

    void OnDestroy()
    {
        callouts.Remove(this);
    }

	// Update is called once per frame
	void Update () {

        if (selected && focusGroup != State.focus)
        {
            Deselect();
        }

        circle.transform.position = target.transform.position;
        
        v = Camera.main.transform.position - target.position;
        v = v * uiDistance / v.magnitude + target.position;
        circle.transform.position = v;

        line.SetPosition(0, anchor.position);
        if ((bottomLeft.transform.position.x + bottomRight.transform.position.x) / 2f > 0f)
        {
            line.SetPosition(1, circleLeft.position);
        }
        else
        {
            line.SetPosition(1, circleRight.position);
        }

        bool shouldBeVisible =
            State.displayInfo &&
            (State.tracking || State.vr) &&
            State.focus == focusGroup &&
            !State.exploded;

        if (shouldBeVisible) 
        {
            alphaController.alpha = Mathf.Min(1f, alphaController.alpha + Time.deltaTime * 5f);
            ScaleWithCamera();
            UpdateLineAnchorPosition();

            if (backing != null)
            {
                backing.SetActive(true);
            }
        } 
        else 
        {
            alphaController.alpha = Mathf.Max(0f, alphaController.alpha - Time.deltaTime * 5f);
            if (backing != null)
            {
                backing.SetActive(false);
            }
        }

        if (alphaController.alpha > 0.5f && !line.enabled)
        {
            Enable();
        }
        else if (alphaController.alpha < 0.5f && line.enabled)
        {
            Disable();
        }

	}

    public void UpdateLineAnchorPosition()
    {
        float t = (bottomLeft.transform.position.x + bottomRight.transform.position.x) / 2;
        t = Mathf.Min(Mathf.Max(-2f, t), 2f);
        t += 2f;
        t /= 4f;
        anchor.position = (bottomLeft.position - bottomRight.position) * t + bottomRight.position;
    }

    public void ScaleWithCamera()
    {
        float minDist = 17f;
        float maxDist = 25f;
        float dist = (model.position - Camera.main.transform.position).magnitude;
        
        float t = Mathf.Min(Mathf.Max(0.3f, (dist - minDist) / (maxDist - minDist)), 1f);

        if (State.vr)
        {
            t = 0.9881006f;
        }

        t = Mathf.Max(0.6f, t);

        v = (destination.position - model.position).normalized * (t * initialLineLength);
        v += model.position;
        destination.position = v;

        float minScale = 0.002f;
        float maxScale = 0.008f;
        Vector3 scale = Vector3.one * (t * (maxScale - minScale) + minScale);
        scale.z = 1f;
        transform.localScale = scale;

        Vector3 psm = band.posScaleMod;
        psm.x = t;
        band.posScaleMod = psm;
    }

    public void Disable()
    {
        line.enabled = false;
        circle.gameObject.active = false;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void Enable()
    {
        line.enabled = true;//!IsOtherSelected && transform.position.z < 0 || selected;
        circle.gameObject.active = true;//!IsOtherSelected && transform.position.z < 0 || selected;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnCalloutClicked() 
    {
        touchRotator.PointAtCamera();
        touchZoomer.Reset();
        Select();
    }

    public void Select()
    {
        if (selected) 
        {
            if (switchTo != State.Focus.NULL) {
                State.focus = switchTo;
            }
            return;
        }

        selected = true;
        selectedCallout = this;
        circle.lineRenderer.SetWidth(0.08f, 0.08f);

        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(1600, 800);

        Vector3 bs = backing.transform.localScale;
        bs.y = 800;
        backing.transform.localScale = bs;
        
        Vector3 bp = backing.transform.localPosition;
        bp.y = 400;
        backing.transform.localPosition = bp;

        Vector3 offset = band.offset;
        offset.y = -0.78625f;
        band.offset = offset;

        for (int i = 0; i < callouts.Count; ++i)
        {
            if (callouts[i] == this) { continue; }
            callouts[i].Deselect();
        }
    }

    public void Deselect()
    {
        if (selected)
        {
            selected = false;
            circle.lineRenderer.SetWidth(0.04f, 0.04f);
            transform.localScale = transform.localScale / 1.1f;

            RectTransform rt = GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(1600, 400);

            Vector3 bs = backing.transform.localScale;
            bs.y = 400;
            backing.transform.localScale = bs;

            Vector3 bp = backing.transform.localPosition;
            bp.y = 200;
            backing.transform.localPosition = bp;

            Vector3 offset = band.offset;
            offset.y = 0f;
            band.offset = offset;

            if (selectedCallout == this) {
                selectedCallout = null;
            }
        }
    }

    public static void DeselectAll() {
        for (int i = 0; i < callouts.Count; ++i)
        {
            callouts[i].Deselect();
        }
        selectedCallout = null;
    }

    public bool IsOtherSelected
    {
        get
        {
            return selectedCallout != null && selectedCallout != this;
        }
    }
}

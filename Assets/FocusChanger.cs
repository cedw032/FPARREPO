using UnityEngine;
using System.Collections;

public class FocusChanger : MonoBehaviour {

    public GameObject heaterBase;
    public GameObject cartridge;
    public GameObject chamber;
    public GameObject inspiratoryLimb;
    public GameObject dryLine;
    public GameObject expiratoryLimb;
    public GameObject heaterWire;
    public GameObject inspiratoryDetail;
    public GameObject expiratoryDetail;

    public Transform mainHeroTransform;
    public Transform heaterBaseHeroTransform;
    public Transform cartridgeHeroTransform;
    public Transform chamberHeroTransform;
    public Transform inspiratoryHeroTransform;
    public Transform expiratoryHeroTransform;

    public LerpToTransform modelLerper;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        switch (State.focus)
        {
            case State.Focus.MAIN:
                modelLerper.target = mainHeroTransform;
                ShowSubAssembly(heaterBase);
                ShowSubAssembly(cartridge);
                ShowSubAssembly(chamber);
                ShowSubAssembly(dryLine);
                ShowSubAssembly(expiratoryLimb);
                ShowSubAssembly(inspiratoryLimb);
                ShowSubAssembly(heaterWire);
                HideSubAssembly(inspiratoryDetail);
                HideSubAssembly(expiratoryDetail);
                break;

            case State.Focus.HEATER_BASE:
                modelLerper.target = heaterBaseHeroTransform;
                ShowSubAssembly(heaterBase);
                ShowSubAssembly(cartridge);
                HideSubAssembly(chamber);
                HideSubAssembly(dryLine);
                HideSubAssembly(expiratoryLimb);
                HideSubAssembly(inspiratoryLimb);
                HideSubAssembly(heaterWire);
                HideSubAssembly(inspiratoryDetail);
                HideSubAssembly(expiratoryDetail);
                break;

            case State.Focus.CARTRIDGE:
                modelLerper.target = cartridgeHeroTransform;
                HideSubAssembly(heaterBase);
                ShowSubAssembly(cartridge);
                HideSubAssembly(chamber);
                HideSubAssembly(dryLine);
                HideSubAssembly(expiratoryLimb);
                HideSubAssembly(inspiratoryLimb);
                HideSubAssembly(heaterWire);
                HideSubAssembly(inspiratoryDetail);
                HideSubAssembly(expiratoryDetail);
                break;

            case State.Focus.CHAMBER:
                modelLerper.target = chamberHeroTransform;
                HideSubAssembly(heaterBase);
                HideSubAssembly(cartridge);
                ShowSubAssembly(chamber);
                HideSubAssembly(dryLine);
                HideSubAssembly(expiratoryLimb);
                HideSubAssembly(inspiratoryLimb);
                HideSubAssembly(heaterWire);
                HideSubAssembly(inspiratoryDetail);
                HideSubAssembly(expiratoryDetail);
                break;

            case State.Focus.INSPIRATORY:
                modelLerper.target = inspiratoryHeroTransform;
                HideSubAssembly(heaterBase);
                HideSubAssembly(cartridge);
                HideSubAssembly(chamber);
                HideSubAssembly(dryLine);
                HideSubAssembly(expiratoryLimb);
                HideSubAssembly(inspiratoryLimb);
                HideSubAssembly(heaterWire);
                ShowSubAssembly(inspiratoryDetail);
                HideSubAssembly(expiratoryDetail);
                break;

            case State.Focus.EXPIRATORY:
                modelLerper.target = expiratoryHeroTransform;
                HideSubAssembly(heaterBase);
                HideSubAssembly(cartridge);
                HideSubAssembly(chamber);
                HideSubAssembly(dryLine);
                HideSubAssembly(expiratoryLimb);
                HideSubAssembly(inspiratoryLimb);
                HideSubAssembly(heaterWire);
                HideSubAssembly(inspiratoryDetail);
                ShowSubAssembly(expiratoryDetail);
                break;
        }
	}

    private void ShowSubAssembly(GameObject go)
    {
        ScaleTo(go, 1f);
    }

    public void HideSubAssembly(GameObject go)
    {
        ScaleTo(go, 0f);
    }

    private void ScaleTo(GameObject go, float targetScale)
    {
        Vector3 scale = go.transform.localScale;
        if (scale.x > targetScale)
        {
            scale.x = scale.y = scale.z = Mathf.Max(scale.x - Time.deltaTime * 10f, targetScale);
        }
        else if (scale.x < targetScale)
        {
            scale.x = scale.y = scale.z = Mathf.Min(scale.x + Time.deltaTime * 10f, targetScale);
        }
        go.transform.localScale = scale;
    }

}

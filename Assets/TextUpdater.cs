using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour {

    public Text text;

	// Update is called once per frame
	void Update () {
        switch (State.focus)
        {
            case State.Focus.MAIN:
                text.text = "Features";
                break;

            case State.Focus.HEATER_BASE:
                text.text = "Humidifier";
                break;

            case State.Focus.CARTRIDGE:
                text.text = "Sensor Cartridge";
                break;

            case State.Focus.CHAMBER:
                text.text = "Water Chamber";
                break;

            case State.Focus.INSPIRATORY:
                text.text = "AirSpiral™ Inspiratory";
                break;

            case State.Focus.EXPIRATORY:
                text.text = "Evaqua™ 2 Expiratory";
                break;
        }
	}
}

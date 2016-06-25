using UnityEngine;
using System.Collections;

public class State{

    public enum Focus
    {
        NULL,
        MAIN,
        HEATER_BASE,
        CARTRIDGE,
        CHAMBER, 
        INSPIRATORY, 
        EXPIRATORY
    }

    public static Focus focus = Focus.MAIN;
    public static bool vr = false;
    public static bool displayInfo = false;
    public static bool tracking = false;
    public static bool exploded = false;
}

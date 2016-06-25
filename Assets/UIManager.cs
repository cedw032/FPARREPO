using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

    public TouchRotator touchRotator;
    public TouchZoomer touchZoomer;
    public Fader fader;

    public Animator explodeAnimator;
    private float explodeClickedAt = -3f;
    private float explodeDuration = 3f;

    void Awake()
    {
        State.exploded = false;
    }

    public void OnVRPressed()
    {
        if (!State.vr)
        {
            fader.doRun = true;
        }
    }

    public void OnARPressed()
    {
        if (State.vr)
        {
            fader.doRun = true;
        }
    }

    public void OnExplodePressed()
    {
        if (Time.time - explodeClickedAt < explodeDuration || State.focus != State.Focus.MAIN) { return; }
        if (State.exploded)
        {
            Collapse();
        }
        else
        {
            Explode();
        }

        
    }

    private void Explode()
    {
        if (!State.exploded)
        {
            explodeAnimator.Play("Explode");
            explodeClickedAt = Time.time;
            State.exploded = true;
        }
    }

    private void Collapse()
    {
        if (State.exploded)
        {
            explodeAnimator.Play("Collapse");
            explodeClickedAt = Time.time;
            State.exploded = false;
        }
    }

    public void OnHomePressed()
    {
        State.focus = State.Focus.MAIN;
        Callout.DeselectAll();
        touchRotator.Reset();
        State.displayInfo = false;
        Collapse();
    }

    public void OnResetPressed()
    {
        touchRotator.Reset();
        touchZoomer.Reset();
        Callout.DeselectAll();
        State.displayInfo = false;
        Collapse();
    }

    public void OnInfoPressed()
    {
        State.displayInfo = !State.displayInfo;
    }

    public void OnBackPressed()
    {
        State.focus = State.Focus.MAIN;
        Callout.DeselectAll();
    }
}

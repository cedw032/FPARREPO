//#define EDITOR_TWO_TOUCH_TEST

using UnityEngine;
using System.Collections;

public class TouchCatcher : MonoBehaviour {

    public bool SingleTouch
    {
        get
        {
#if EDITOR_TWO_TOUCH_TEST
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                return false;
            }
            else
            {
                return Input.touchCount == 1 || Input.GetMouseButton(0);
            }
#else
            return Input.touchCount == 1 || Input.GetMouseButton(0);
#endif
        }
    }

    public bool DoubleTouch
    {
        get
        {
#if EDITOR_TWO_TOUCH_TEST
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                return Input.GetMouseButton(0);
            }
            else
            {
                return Input.touchCount == 2;
            }
#else
            return Input.touchCount == 2;
#endif
        }
    }

    public Vector2 PositionOne
    {
        get
        {
#if EDITOR_TWO_TOUCH_TEST
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                return Vector2.zero;
            }
            else
            {
                if (Input.touchCount == 0)
                {
                    return Input.mousePosition;
                }
                return Input.GetTouch(0).position;
            }
#else
            if (Input.touchCount == 0)
            {
                return Input.mousePosition;
            }
            return Input.GetTouch(0).position;
#endif
        }
    }

    public Vector2 PositionTwo
    {
        get
        {
#if EDITOR_TWO_TOUCH_TEST

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                return Input.mousePosition;
            }
            else
            {
                return Input.GetTouch(1).position;
            }
#else
            return Input.GetTouch(1).position;
#endif
        }
    }

}

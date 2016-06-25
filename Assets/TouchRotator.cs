using UnityEngine;
using System.Collections;

public class TouchRotator : MonoBehaviour {

    public TouchCatcher touchCatcher;

    public Transform rollTransform;
    public Transform yawTransform;

    public float sensitivity;

    private float dx = 0f;
    private float dy = 0f;

    private float lastX = 0;
    private float lastY = 0;

    private bool wasTouching = false;

	// Use this for initialization
	void Start () {
	}

    void Update()
    {
        if (touchCatcher.SingleTouch)
        {
            if (!wasTouching)
            {
                lastX = touchCatcher.PositionOne.x;
                lastY = touchCatcher.PositionOne.y;
            }

            dx = touchCatcher.PositionOne.x - lastX;
            dy = touchCatcher.PositionOne.y - lastY;

            dx *= sensitivity;
            dy *= sensitivity;

            wasTouching = true;
            lastX = touchCatcher.PositionOne.x;
            lastY = touchCatcher.PositionOne.y;
        } 
        else 
        {
            if (wasTouching)
            {
                wasTouching = false;
            }

            if (touchCatcher.DoubleTouch)
            {
                dx = 0f;
                dy = 0f;
            }
            else
            {
                dx *= 0.98f;
                dy *= 0.98f;
            }
        }

        if (State.vr)
        {
            yawTransform.Rotate(new Vector3(0f, -1f, 0f), dx, Space.Self);
            rollTransform.Rotate(new Vector3(1f, 0f, 0f), dy, Space.Self);
            EnsureAxisStability();
            ConstrainRoll();
        }
        else
        {
           yawTransform.Rotate(new Vector3(0f, -1f, 0f), dx, Space.Self);
        }
    }

    public void EnsureAxisStability()
    {
        Vector3 yaw = yawTransform.localEulerAngles;
        yaw.x = yaw.z = 0f;
        yawTransform.localEulerAngles = yaw;

        Vector3 roll = rollTransform.localEulerAngles;
        roll.y = roll.z = 0f;
        rollTransform.localEulerAngles = roll;
    }

    public void ConstrainRoll()
    {
        Vector3 roll = rollTransform.localEulerAngles;
        float x = roll.x;

        while (x < -180) { x += 360; }
        while (x >= 180) { x -= 360; }
        x = Mathf.Min(Mathf.Max(-50f, x), 20f);

        roll.x = x;
        rollTransform.localEulerAngles = roll;
    }

    public void PointAtCamera()
    {
        Reset();

        float dx = yawTransform.position.x - Camera.main.transform.position.x;
        float dy = yawTransform.position.z - Camera.main.transform.position.z;
        float theta = Mathf.Atan2(dx, dy);
        yawTransform.eulerAngles = new Vector3(0f, theta * 180f / Mathf.PI, 0f);
    }

    public void Reset()
    {
        wasTouching = false;
        dx = 0f;
        dy = 0f;
        lastX = 0f;
        lastY = 0f;
        yawTransform.rotation = Quaternion.identity;
        rollTransform.rotation = Quaternion.identity;
    }
}

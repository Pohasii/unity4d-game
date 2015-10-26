using UnityEngine;
using System.Collections;

public class WallRotation : MonoBehaviour 
{
    public Vector3 AxisOfRotation;
    public enum RotationAxis { forward, back, Up, Down, left, right }
    public RotationAxis Axis;

    public float SpeedRotation;
    public float Duration;
    public float time;

    Quaternion target;

	void Start () 
    {
        target = Quaternion.Euler(transform.position - (transform.position - Vector3.up * 10));
	}
	
	void Update () 
    {
        WallRotate(Axis);
  	}

    void WallRotate(RotationAxis pAxis)
    {
        switch (pAxis)
        {
            case RotationAxis.forward: AxisOfRotation = Vector3.forward; break;
            case RotationAxis.back: AxisOfRotation = Vector3.back; break;
            case RotationAxis.Up: AxisOfRotation = Vector3.up; break;
            case RotationAxis.Down: AxisOfRotation = Vector3.down; break;
            case RotationAxis.left: AxisOfRotation = Vector3.left; break;
            case RotationAxis.right: AxisOfRotation = Vector3.right; break;
        }

        time += Time.deltaTime * SpeedRotation;
        time = Mathf.Clamp(time, 0, Duration);
        if (time < Duration)
        {
            transform.Rotate(AxisOfRotation * SpeedRotation * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            time = 0;
        }
    }
}

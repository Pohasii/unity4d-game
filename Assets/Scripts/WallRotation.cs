using UnityEngine;
using System.Collections;

public class WallRotation : MonoBehaviour 
{
    Vector3 AxisOfRotation;
    public enum RotationAxis { forward, back, Up, Down, left, right }
    public RotationAxis Axis;
    bool rotateF = true , rotate = true;

    public float SpeedRotation;
    public float Duration;
    public float PauseDuration;
    float Pause = 0;
    float time;
    float hz;

    Transform localEA;

	void Start () 
    {
        localEA = GetComponent<Transform>();
	}
	
	void Update ()
    {
        WallRotate(Axis);
  	}

    void WallRotate(RotationAxis pAxis)
    {
        switch (pAxis)
        {
            case RotationAxis.forward: AxisOfRotation = Vector3.forward; hz = localEA.localEulerAngles.z; localEA.localEulerAngles = new Vector3(0,0,Mathf.Clamp(localEA.localEulerAngles.z,0,Duration)); break;
            case RotationAxis.back: AxisOfRotation = Vector3.back; hz = -localEA.localEulerAngles.z; localEA.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(-localEA.localEulerAngles.z, 0, Duration)); break;
            case RotationAxis.Up: AxisOfRotation = Vector3.up; hz = localEA.localEulerAngles.y; localEA.localEulerAngles = new Vector3(0, Mathf.Clamp(localEA.localEulerAngles.y, 0, Duration),0); break;
            case RotationAxis.Down: AxisOfRotation = Vector3.down; hz = -localEA.localEulerAngles.y; localEA.localEulerAngles = new Vector3(0, Mathf.Clamp(-localEA.localEulerAngles.y, 0, Duration), 0); break;
            case RotationAxis.left: AxisOfRotation = Vector3.left; hz = -localEA.localEulerAngles.x; localEA.localEulerAngles = new Vector3(Mathf.Clamp(-localEA.localEulerAngles.x, 0, Duration), 0, 0); break;
            case RotationAxis.right: AxisOfRotation = Vector3.right; hz = localEA.localEulerAngles.x; localEA.localEulerAngles = new Vector3(Mathf.Clamp(localEA.localEulerAngles.x, 0, Duration), 0, 0); break;
        }

        //time += Time.deltaTime * SpeedRotation;
        //time = Mathf.Clamp(time, 0, Duration);
        //if (time < Duration && rotateF)
        if (rotateF && rotate)
        {
            transform.Rotate(AxisOfRotation * SpeedRotation * Time.deltaTime);
        }
        if(!rotateF && rotate)
        {
            transform.Rotate(-AxisOfRotation * SpeedRotation * Time.deltaTime);
        }
        if (hz >= Duration)
        {
            rotate = false;
            Pause += Time.deltaTime;
            if(Pause >= PauseDuration)
            {
                Pause = 0;
                rotateF = false;
                rotate = true;
            }
        }
        if (hz <= 0)
        {
            rotate = false;
            Pause += Time.deltaTime;
            if (Pause >= PauseDuration)
            {
                Pause = 0;
                rotateF = true;
                rotate = true;
            }
        }
        //if (time > Duration)
        //{
        //    rotateF = false;
        //}
        //if(!rotateF)
        //{
        //    time += Time.deltaTime;
        //    if (Pause == PauseDuration)
        //    {
        //        transform.Rotate(AxisOfRotation * SpeedRotation * Time.deltaTime);
        //    }
        //}
    }
}

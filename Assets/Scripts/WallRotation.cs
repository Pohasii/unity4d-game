using UnityEngine;
using System.Collections;

public class WallRotation : MonoBehaviour 
{
    Vector3 AxisOfRotation;
    public enum RotationAxis { forward, back, Up, Down, left, right }
    public RotationAxis Axis;
    public bool rotateF = true;

    public float SpeedRotation;
    public float Duration;
    public float PauseDuration;
    float Pause = 0;
    float time;
    float hz;

    Transform localEA;

	void Start () 
    {
        enabled = Network.isServer;
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
            case RotationAxis.forward: AxisOfRotation = Vector3.forward; hz = localEA.localEulerAngles.z;break; //localEA.localEulerAngles = new Vector3(0,0,Mathf.Clamp(localEA.localEulerAngles.z,0,Duration)); break;
            case RotationAxis.back: AxisOfRotation = Vector3.back; hz = -localEA.localEulerAngles.z; break;//localEA.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(-localEA.localEulerAngles.z, 0, Duration)); break;
            case RotationAxis.Up: AxisOfRotation = Vector3.up; hz = localEA.localEulerAngles.y; break;//localEA.localEulerAngles = new Vector3(0, Mathf.Clamp(localEA.localEulerAngles.y, 0, Duration),0); break;
            case RotationAxis.Down: AxisOfRotation = Vector3.down; hz = -localEA.localEulerAngles.y; break; //localEA.localEulerAngles = new Vector3(0, Mathf.Clamp(-localEA.localEulerAngles.y, 0, Duration), 0); break;
            case RotationAxis.left: AxisOfRotation = Vector3.left; hz = -localEA.localEulerAngles.x; break;//localEA.localEulerAngles = new Vector3(Mathf.Clamp(-localEA.localEulerAngles.x, 0, Duration), 0, 0); break;
            case RotationAxis.right: AxisOfRotation = Vector3.right; hz = localEA.localEulerAngles.x; break;//localEA.localEulerAngles = new Vector3(Mathf.Clamp(localEA.localEulerAngles.x, 0, Duration), 0, 0); break;
        }

        if (rotateF)
        {
            localEA.localEulerAngles -= new Vector3(0, SpeedRotation * Time.deltaTime, 0);
        }
        else
        {
            //localEA.localEulerAngles -= new Vector3(0, SpeedRotation * Time.deltaTime, 0);
        }
        //localEA.localEulerAngles = new Vector3(0, Mathf.Clamp(localEA.localEulerAngles.y, 0, Duration), 0);
        if (hz >= Duration)
        {
            rotateF = false;
        }
        if(hz <= 0)
        {
            rotateF = true;
        }
    }
}

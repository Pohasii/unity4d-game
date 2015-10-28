using UnityEngine;
using System.Collections;

public class Sync : MonoBehaviour
{
    private Vector3 lastPos;
    private Quaternion lastRot;
    private Transform myTransform;
    private NetworkView nV;

    void Start()
    {
        nV = GetComponent<NetworkView>();
        if (nV.isMine)
        {
            myTransform = transform;
        }
        else
        {
            enabled = false;
        }
    }

    void Update()
    {
        if(Vector3.Distance(myTransform.position, lastPos) >= 0.01f)
        {
            lastPos = myTransform.position;
            nV.RPC("UpdateMovment", RPCMode.Others, myTransform.position, myTransform.rotation);
        }
        if (Quaternion.Angle(myTransform.rotation, lastRot) >= 1)
        {
            lastRot = myTransform.rotation;
            nV.RPC("UpdateMovment", RPCMode.Others, myTransform.position, myTransform.rotation);
        }
    }

    [RPC]
    void UpdateMovment(Vector3 newPos, Quaternion newRot)
    {
        transform.position = newPos;
        transform.rotation = newRot;
    }
}

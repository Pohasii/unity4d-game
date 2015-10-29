using UnityEngine;
using System.Collections;

public class Sync : MonoBehaviour
{
    private Vector3 lastPos;
    private Quaternion lastRot;
    private Transform myTransform;
    private NetworkView nV;
    Player player;

    float x;
    float z;
    float energy;
    bool jumping, run;

    void Start()
    {
        nV = GetComponent<NetworkView>();
        player = GetComponent<Player>();
        x = player.player.X;
        z = player.player.Z;
        energy = player.player.CurEnergy1;
        jumping = !player.player.Jumping;
        run = player.player.run2;

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
            nV.RPC("UpdateMovment", RPCMode.OthersBuffered, myTransform.position, myTransform.rotation);
        }
        if (Quaternion.Angle(myTransform.rotation, lastRot) >= 1)
        {
            lastRot = myTransform.rotation;
            nV.RPC("UpdateMovment", RPCMode.OthersBuffered, myTransform.position, myTransform.rotation);
        }
    }

    [RPC]
    void UpdateMovment(Vector3 newPos, Quaternion newRot)
    {
        transform.position = newPos;
        transform.rotation = newRot;
    }
}

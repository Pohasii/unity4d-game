using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    Transform myTransform;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float hitPoint;
    [SerializeField]
    private float energy;
    public PlayerClass player;
    private NetworkView networkView1;
    public string str;

    void Start()
    {
        networkView1 = GetComponent<NetworkView>();
        myTransform = GetComponent<Transform>();
        player = new PlayerClass(myTransform, hitPoint, moveSpeed, energy, rotationSpeed);
    }

    void Update()
    {
        networkView1.RPC("UpdateFunctions", RPCMode.OthersBuffered, player.X, player.Z, player.CurEnergy1, player.Jumping, player.run2);
        player.Animation();
        if (networkView1.isMine)
        {
            player.EnegryUpdate();
            player.Jump();
        }
    }

    void FixedUpdate()
    {
        if (networkView1.isMine)
        player.Move();
    }

    [RPC]
    void UpdateFunctions(float X, float Z, float Energy, bool j, bool r2)
    {
        str = "НЯНЯНЯНЯ";
        player.X = X;
        player.Z = Z;
        player.CurEnergy1 = Energy;
        player.Jumping = j;
        player.run2 = r2;
    }
}

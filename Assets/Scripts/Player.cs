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
    private bool networkView1;

    void Start()
    {
        networkView1 = GetComponent<NetworkView>().isMine;
        enabled = networkView1;
        myTransform = GetComponent<Transform>();
        player = new PlayerClass(myTransform, hitPoint, moveSpeed, energy, rotationSpeed);
    }

    void Update()
    {
        player.Animation();
        player.EnegryUpdate();
        player.Jump();
    }

    void FixedUpdate()
    {
        player.Move();
    }
}

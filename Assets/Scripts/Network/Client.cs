using UnityEngine;
using System.Collections;

public class Client : MonoBehaviour
{
    public GameObject Player;
    private Transform Resp;

    void Start()
    {
        Resp = GameObject.Find("Respawn").GetComponent<Transform>();
        Network.Instantiate(Player, Resp.position, Resp.rotation, 0);
    }

    void Update()
    {

    }
}

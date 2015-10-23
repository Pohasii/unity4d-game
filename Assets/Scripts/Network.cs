using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Network : NetworkBehaviour 
{
    [SerializeField]
    private GameObject camera;
    [SerializeField]
    private CharacterController controller;

	void Start () 
    {
        camera.SetActive(isLocalPlayer);
        controller.enabled = isLocalPlayer;
	}
}

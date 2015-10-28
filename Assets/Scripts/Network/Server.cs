using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Server : MonoBehaviour 
{
    private enum Windows {MainMenu, HostWindow, ClientWindow }
    private Windows Window;
    private Transform TCanvas;
    private Text IP;
    private Text Port;
    private int MaxConnections = 10;
    private GameObject MainMenu;
    public GameObject StartServer;
    public GameObject Connected;
    private NetworkView NetworkView1;

	void Start ()
    {
        Window = Windows.MainMenu;
        TCanvas = GameObject.Find("Canvas").GetComponent<Transform>();
        IP = GameObject.Find("IP").GetComponent<Text>();
        Port = GameObject.Find("Port").GetComponent<Text>();
        MainMenu = GameObject.Find("MainMenu");
        GameObject.Find("InputIP").GetComponent<InputField>().text = "127.0.0.1";
        GameObject.Find("InputPort").GetComponent<InputField>().text = "7777";
        NetworkView1 = GetComponent<NetworkView>();
	}

    void Update()
    {
        switch (Window)
        {
            case Windows.MainMenu: MainMenu.SetActive(true); break;
            case Windows.HostWindow: StartServer.SetActive(true); MainMenu.SetActive(false); break;
            case Windows.ClientWindow: Connected.SetActive(true); MainMenu.SetActive(false); break;
        }
    }

    void OnServerInitialized()
    {
        Window = Windows.HostWindow;
    }

    void OnConnectedToServer()
    {
        Window = Windows.ClientWindow;
    }

    public void CreateServer()
    {
        Network.InitializeServer(MaxConnections, int.Parse(Port.text), true);
    }

    public void Connect()
    {
        Network.Connect(IP.text, int.Parse(Port.text));
    }

    public void LoadLevel()
    {
        NetworkView1.RPC("LoadLevelRPC", RPCMode.All);
    }

    [RPC]
    public void LoadLevelRPC()
    {
        Application.LoadLevel("New World Kirill");
    }
}

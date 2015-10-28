using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class ButtonEvents : MonoBehaviour, IPointerClickHandler
{
    Server ServerScript;
    Transform TButton;

    void Start()
    {
        ServerScript = GameObject.Find("Main Camera").GetComponent<Server>();
        TButton = GetComponent<Transform>();
    }

    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (TButton.name == "CreateServer")
        {
            ServerScript.CreateServer();
        }
        if (TButton.name == "Connect")
        {
            ServerScript.Connect();
        }
        if (TButton.name == "StartServer")
        {
            ServerScript.LoadLevel();
        }
    }
}

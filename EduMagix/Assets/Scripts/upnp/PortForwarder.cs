using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortForwarder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        uPnPHelper.DebugMode = true;
        uPnPHelper.LogErrors = true;
    }
    void StartPortForwarding(int portNumber){
        uPnPHelper.Start(uPnPHelper.Protocol.UDP, portNumber, 0, "Unity uPnP Port Forward Test.");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

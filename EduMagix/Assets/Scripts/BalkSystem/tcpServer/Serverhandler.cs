using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serverhandler : MonoBehaviour
{
    public BaseCommandClass Command;
    public bool classChanging = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckForClass(string message)
    {
        if (classChanging = true){

        }
        responceToServerMessage(message);
    }
    public void responceToServerMessage(string message)
    {
        
        switch(message)
        {
            case "":
                Command.Invoke();
                break;
            case "AddNumber":
                Command.Invoke();
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecHouseCommand : MonoBehaviour, ISimpleCommand
{
    Serverhandler serverhandler;
    public string HouseName;
    public void Invoke()
    {
        serverhandler = GameObject.Find("Client").GetComponent<Serverhandler>();
        serverhandler.HouseToAddPointsTo = HouseName;
    }
}

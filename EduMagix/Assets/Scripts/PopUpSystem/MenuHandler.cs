using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public GameObject DebugLog;
    public Dictionary<string, GameObject> klasButtons = new Dictionary<string, GameObject>();

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Minus)){
            PopUp(DebugLog);
        }
    }
    public void PopUp(GameObject popUp){
        switch(popUp.activeInHierarchy){
            case true:
                popUp.SetActive(false);
                break;
            case false:
                popUp.SetActive(true);
                break;
        }
    }

}

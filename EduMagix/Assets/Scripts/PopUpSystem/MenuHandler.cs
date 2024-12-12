using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
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

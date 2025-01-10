using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public GameObject DebugLog;
    public Serverhandler serverhandler;
    public HouseImages houseImages;

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
    public void DisplayKlasStats(TMPro.TMP_Text klasName, TMPro.TMP_Text klasPoints, TMPro.TMP_Text klasStudentCount, RawImage KlasImage){
        Data data = ListOfData.GetListOfData().GetData(serverhandler.HouseToAddPointsTo);
        klasName.text = data.houseName;
        klasPoints.text = "" + data.currentAmountOfPoints;
        klasStudentCount.text = "" + data.aantalLeerlingen;
        KlasImage.texture = houseImages.houseTextures[data.houseImage];
    }
}

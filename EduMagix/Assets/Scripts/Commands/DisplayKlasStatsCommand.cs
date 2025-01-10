using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayKlasStatsCommand : MonoBehaviour, ISimpleCommand
{
    
    public Serverhandler serverhandler;
    public HouseImages houseImages;
    public void Invoke()
    {
        serverhandler = GameObject.Find("Client").GetComponent<Serverhandler>();
        GameObject displayObject = gameObject.transform.parent.parent.parent.parent.GetChild(1).gameObject;
        print(displayObject.name);
        TMPro.TMP_Text klasName = displayObject.transform.GetChild(0).GetComponent<TMPro.TMP_Text>();
        TMPro.TMP_Text klasPoints = displayObject.transform.GetChild(1).GetComponent<TMPro.TMP_Text>();
        TMPro.TMP_Text klasStudentCount = displayObject.transform.GetChild(2).GetComponent<TMPro.TMP_Text>();
        RawImage klasKlasImage = displayObject.transform.GetChild(3).GetComponent<RawImage>();
        DisplayKlasStats(klasName, klasPoints, klasStudentCount, klasKlasImage);
    }
    public void DisplayKlasStats(TMPro.TMP_Text klasName, TMPro.TMP_Text klasPoints, TMPro.TMP_Text klasStudentCount, RawImage KlasImage){
        Data data = ListOfData.GetListOfData().GetData(serverhandler.HouseToAddPointsTo);
        klasName.text = "House Name " + data.houseName;
        klasPoints.text = "House Points " + data.currentAmountOfPoints;
        klasStudentCount.text = "Student Count " + data.aantalLeerlingen;
        KlasImage.texture = houseImages.houseTextures[data.houseImage];
    }
}

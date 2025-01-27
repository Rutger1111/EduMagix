using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayKlasStatsCommand : MonoBehaviour, ISimpleCommand
{
    public Serverhandler serverhandler;
    public House houseImages;
    public void Invoke()
    {
        GameObject displayObject;
        serverhandler = GameObject.Find("Client").GetComponent<Serverhandler>();
        if (gameObject.transform.parent.name == "Content"){
            displayObject = gameObject.transform.parent.parent.parent.parent.GetChild(1).gameObject;
            TMPro.TMP_Text klasName = displayObject.transform.GetChild(0).GetComponent<TMPro.TMP_Text>();
            TMPro.TMP_Text klasPoints = displayObject.transform.GetChild(1).GetComponent<TMPro.TMP_Text>();
            TMPro.TMP_Text klasStudentCount = displayObject.transform.GetChild(2).GetComponent<TMPro.TMP_Text>();
            RawImage klasKlasImage = displayObject.transform.GetChild(3).GetComponent<RawImage>();
            DisplayKlasStats(klasName, klasPoints, klasStudentCount, klasKlasImage);
        }
        else{
            displayObject = gameObject;
            TMP_InputField klasName = displayObject.transform.GetChild(0).GetComponent<TMP_InputField>();
            TMP_InputField klasPoints = displayObject.transform.GetChild(1).GetComponent<TMP_InputField>();
            TMP_InputField klasStudentCount = displayObject.transform.GetChild(2).GetComponent<TMP_InputField>();
            RawImage klasKlasImage = displayObject.transform.GetChild(3).GetComponent<RawImage>();
            DisplayKlasStats(klasName, klasPoints, klasStudentCount, klasKlasImage);
        }
        print(displayObject.name + displayObject.transform.GetChild(0).name);

    }
    public void DisplayKlasStats(TMPro.TMP_Text klasName, TMPro.TMP_Text klasPoints, TMPro.TMP_Text klasStudentCount, RawImage KlasImage){
        Data data = ListOfData.GetListOfData().GetData(serverhandler.HouseToAddPointsTo);
        print(klasName.text);
        klasName.text = "House Name " + data.houseName;
        klasPoints.text = "House Points " + data.currentAmountOfPoints;
        klasStudentCount.text = "Student Count " + data.aantalLeerlingen;
        KlasImage.texture = houseImages.houseTextures[data.houseImage].texture;
    }
    public void DisplayKlasStats(TMP_InputField klasName, TMP_InputField klasPoints, TMP_InputField klasStudentCount, RawImage KlasImage){
        Data data = ListOfData.GetListOfData().GetData(serverhandler.HouseToAddPointsTo);
        print(klasName.text);
        klasName.text = "" + data.houseName;
        klasPoints.text = "" + data.currentAmountOfPoints;
        klasStudentCount.text = "" + data.aantalLeerlingen;
        KlasImage.texture = houseImages.houseTextures[data.houseImage].texture;
    }
}

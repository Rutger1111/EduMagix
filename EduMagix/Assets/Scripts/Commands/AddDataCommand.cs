using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AddDataCommand : MonoBehaviour, ISimpleCommand
{
    public Database database;
    public TMPro.TMP_InputField houseNameInputfield;
    public TMPro.TMP_InputField housePointsInputfield;
    public TMPro.TMP_InputField houseStudentCount;
    public TMPro.TMP_InputField houseImageInputfield;
    public void Invoke()
    {
        byte[] fileData = File.ReadAllBytes(houseImageInputfield.text);
        Data data = new Data(fileData,houseNameInputfield.text,float.Parse(housePointsInputfield.text), int.Parse(houseStudentCount.text));
        database.AddClass(data.houseName, data);
    }
}

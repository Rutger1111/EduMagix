using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AddDataCommand : ISimpleCommand
{
    public Database database;
    public InputField houseNameInputfield;
    public InputField housePointsInputfield;
    public InputField houseStudentCount;
    public InputField houseImageInputfield;
    public void Invoke()
    {
        byte[] fileData = File.ReadAllBytes(houseImageInputfield.text);
        Data data = new Data(fileData,houseNameInputfield.text,float.Parse(housePointsInputfield.text), int.Parse(houseStudentCount.text));
    }
}

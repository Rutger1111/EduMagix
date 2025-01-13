using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AddDataCommand : MonoBehaviour, ISimpleCommand
{
    public InitiateDataButtons initiateData;
    public TcpServer server;
    public Database database;
    public TMPro.TMP_InputField houseNameInputfield;
    public TMPro.TMP_InputField housePointsInputfield;
    public TMPro.TMP_InputField houseStudentCount;
    public chooseImage houseImageInputfield;
    public void Invoke()
    {
        Data data = new Data(houseImageInputfield.index,houseNameInputfield.text,float.Parse(housePointsInputfield.text), int.Parse(houseStudentCount.text));
        database.AddClass(data.houseName, data);
        if(server.client != null){
            server.SendDataToClient(new Decryptor().SerializeDB(data));
        }
        initiateData.InitiateClassButton(data);
    }
}

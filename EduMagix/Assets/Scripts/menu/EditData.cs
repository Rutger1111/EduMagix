using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditData : MonoBehaviour
{
    public MenuHandler menuHandler;
    public Serverhandler serverhandler;
    public Database database;
    public TMPro.TMP_InputField houseNameInputfield;
    public TMPro.TMP_InputField housePointsInputfield;
    public TMPro.TMP_InputField houseStudentCount;
    public chooseImage houseImageInputfield;
    public Data dataToEdit;
    public GameObject buttonObject;
    // Start is called before the first frame update
    public void Edit(){
        dataToEdit = ListOfData.GetListOfData().GetData(serverhandler.HouseToAddPointsTo);
        dataToEdit.houseName = houseNameInputfield.text;
        dataToEdit.currentAmountOfPoints = float.Parse(housePointsInputfield.text);
        dataToEdit.aantalLeerlingen = int.Parse(houseStudentCount.text);
        dataToEdit.houseImage = houseImageInputfield.index;
        if(dataToEdit.houseName != serverhandler.HouseToAddPointsTo){
            ListOfData.GetListOfData().Remove(serverhandler.HouseToAddPointsTo);
            menuHandler.klasButtons[serverhandler.HouseToAddPointsTo].GetComponent<SelecHouseCommand>().HouseName = dataToEdit.houseName;

            menuHandler.klasButtons[serverhandler.HouseToAddPointsTo].transform.GetChild(0).GetComponent<TMP_Text>().text = dataToEdit.houseName;
            menuHandler.klasButtons.Add(dataToEdit.houseName, menuHandler.klasButtons[serverhandler.HouseToAddPointsTo]);
            menuHandler.klasButtons.Remove(serverhandler.HouseToAddPointsTo);
            dataToEdit.lastHouseName = serverhandler.HouseToAddPointsTo;
            serverhandler.HouseToAddPointsTo = dataToEdit.houseName;
            ListOfData.GetListOfData().AddData(dataToEdit);
        }
        print(dataToEdit.lastHouseName);
        database.EditClass(dataToEdit.lastHouseName, dataToEdit);
        buttonObject.transform.GetChild(0).GetComponent<TMP_Text>().text = serverhandler.HouseToAddPointsTo;
        print("fskokgkmjkhjshi");
        if(serverhandler.server.client != null){
            serverhandler.server.SendDataToClient(new Decryptor().SerializeDB(dataToEdit));

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InitiateDataButtons : MonoBehaviour
{
    public Serverhandler serverhandler;
    public GameObject buttonPrefab;
    public Database database;
    private ListOfData listOfData;
    private List<string> keys;
    public GameObject contentObject;
    private DebugTextCollector textCollector;
    // Start is called before the first frame update
    void Start()
    {
        database.ReadAllClass();
        listOfData = ListOfData.GetListOfData();
        keys = listOfData.GetDatasKeys();
        textCollector = DebugTextCollector.GetTextCollector();
        InitiateAllClassButtons();
    }
    private void InitiateAllClassButtons(){
        for (int I = 0; I < listOfData.names.Count; I ++){
            Data data = listOfData.GetData(listOfData.names[I]);
            textCollector.AddDebugText("initiated " + data.houseName + keys.Count);
            InitiateClassButton(data);
        }
    }
    public void InitiateClassButton(Data data){
        GameObject textObject = Instantiate<GameObject>(buttonPrefab, contentObject.gameObject.transform);
        print(textObject);
        textObject.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = data.houseName;
        textObject.GetComponent<SelecHouseCommand>().HouseName = data.houseName;
    }

    public void Invoke()
    {
        throw new System.NotImplementedException();
    }
}

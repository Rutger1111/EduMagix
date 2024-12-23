using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InitiateDataCommand : MonoBehaviour, ISimpleCommand
{
    public Serverhandler serverhandler;
    public GameObject buttonPrefab;
    public Database database;
    private ListOfData listOfData;
    private List<string> keys;
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
        for (int I = 0; I < keys.Count; I ++){
            Data data = listOfData.GetData(keys[I]);
            InitiateClassButton(data);
        }
    }
    private void InitiateClassButton(Data data){
        GameObject textObject = Instantiate<GameObject>(buttonPrefab, gameObject.transform);
        textObject.GetComponent<TMPro.TMP_Text>().text = data.houseName;
        textObject.GetComponent<SelecHouseCommand>().HouseName = data.houseName;
    }

    public void Invoke()
    {
        throw new System.NotImplementedException();
    }
}

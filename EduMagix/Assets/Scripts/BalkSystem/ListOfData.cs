using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfData
{
    public static ListOfData listOfData;
    private ListOfData(){

    }
    public static ListOfData GetListOfData(){
        if (ListOfData.listOfData == null){
            ListOfData.listOfData = new ListOfData();
            ListOfData.listOfData.datas = new Dictionary<string, Data>();
        }
        return ListOfData.listOfData;
    }

    Dictionary<string,Data> datas;
    public List<string> names = new List<string>();
    public void AddData(Data data){
        DebugTextCollector.GetTextCollector().AddDebugText(""+data.houseName);
        if(listOfData.datas.ContainsKey(data.houseName) == true){
            listOfData.datas[data.houseName] = data;
        }
        else if(listOfData.datas.ContainsKey(data.houseName) == false){
            ListOfData.listOfData.datas.Add(data.houseName, data); 
            names.Add(data.houseName);
        }

        Debug.Log("komt hier    " + listOfData.datas[data.houseName] + data.houseName); 
    }
    public Data GetData(string houseName){
        return ListOfData.listOfData.datas[houseName];
    }
    public List<string> GetDatasKeys(){
        return new List<string>(datas.Keys);
    }
}

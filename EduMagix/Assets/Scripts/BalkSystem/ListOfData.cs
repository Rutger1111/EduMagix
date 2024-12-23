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
    public void AddData(Data data){
        data.houseName = "a";
        if(listOfData.datas.ContainsKey("a") == true){
            listOfData.datas["a"] = data;
        }
        else if(listOfData.datas.ContainsKey("a") == false){
            ListOfData.listOfData.datas.Add(data.houseName, data); 
        }

        Debug.Log("komt hier    " + listOfData.datas["a"] + data.houseName); 
    }
    public Data GetData(string houseName){
        return ListOfData.listOfData.datas[houseName];
    }
    public List<string> GetDatasKeys(){
        return new List<string>(datas.Keys);
    }
}

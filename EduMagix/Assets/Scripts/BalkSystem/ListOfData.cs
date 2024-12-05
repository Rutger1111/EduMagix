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
        datas.Add(data.houseName, data);
    }
    public Data GetData(string houseName){
        return datas[houseName];
    }

}

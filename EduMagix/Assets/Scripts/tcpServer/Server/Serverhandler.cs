using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Text.RegularExpressions;

using System.Runtime.Serialization.Formatters.Binary;
using System;
using Unity.VisualScripting;

public class Serverhandler : MonoBehaviour, IWorkers
{
    public List<BaseCommandClass> baseCommandClasses;
    public List<string> strings;
    public List<string> House;
    public BaseCommandClass CommandSelectClass;
    public bool waitForAnswer = false;
    public TcpServer server;
    public Thread thread;
    public SplitStrings splitStrings = new SplitStrings();
    public string[] actionString;
    public List<BaseCommandClass> baseCommands;
    public string HouseToAddPointsTo;
    void Start()
    {
        
    }
    public void Callback(string[] words, int numbers)
    {
        actionString = new string[words.Length];
        for (int i = 0; i < words.Length; i++)
        {
            if (Regex.IsMatch(words[i], @"^\d+$") == true){
                Debug.Log(int.Parse(words[i]));
                actionString[i] = words[i];
            }
            else
            {
                actionString[i] = words[i];
            }
        }    
    }

    // Start is called before the first frame update
    public void CheckForClass(string message)
    {
        if (waitForAnswer == true)
        {

        }
        else
        {
            responceToServerMessage(message);
        }
    }
    public void responceToServerMessage(string message)
    {
        /*
        if (waitForAnswer == true){
            CommandAddPoints.amount = int.Parse(message);
            CommandAddPoints.Invoke();
            thread.Start();
            waitForAnswer = false;
            server.ResponceToClient("numbersAdded");
        }
        */
        DebugTextCollector.GetTextCollector().AddDebugText("Responces to client message");
        splitStrings.splitNumbers(this, message);
        DebugTextCollector.GetTextCollector().AddDebugText(actionString[0]);
        switch(actionString[0])
        {
            case "SelectClass":
                server.ResponceToClient("classSelectTrue");
                break;
            case "NumberConfig" :
                server.ResponceToClient("numberSelectTrue");
                break;
            case "AddNumbers":
                Debug.Log("komthierrrrrrr");
                Debug.Log(actionString[2]);
                baseCommandClasses.Add(baseCommands[int.Parse(actionString[2])]);
                strings.Add(actionString[1]);
                House.Add(actionString[2]);
                Debug.Log(House[House.Count - 1]);
                server.ResponceToClient("respondNumberToAdd");
                break;
            case "Received":
                DebugTextCollector.GetTextCollector().AddDebugText("got into case");
                server.SendNextData();
                DebugTextCollector.GetTextCollector().AddDebugText("sendt next data");
                break;
            case "classData":

                break;
        }            

    }
    public void SendClass(){
        DebugTextCollector.GetTextCollector().AddDebugText("kwam hier");
        ListOfData listOfData = ListOfData.GetListOfData();
        Data data = listOfData.GetData(HouseToAddPointsTo);
        
        byte[] bytes = new Decryptor().SerializeDB(data);
        DebugTextCollector.GetTextCollector().AddDebugText("sendData");

        server.SendDataToClient(bytes);

    }
}

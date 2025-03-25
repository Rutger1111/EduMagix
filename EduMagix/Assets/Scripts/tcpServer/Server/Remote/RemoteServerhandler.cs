using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Text.RegularExpressions;

using System.Runtime.Serialization.Formatters.Binary;
using System;
using Unity.VisualScripting;
using TMPro;

public class RemoteServerhandler : MonoBehaviour, IWorkers
{
    public List<BaseCommandClass> baseCommandClasses;
    public List<Data> datas = new List<Data>();
    public RemoteTcpServer client;
    public SplitStrings splitStrings = new SplitStrings();
    public string HouseToAddPointsTo;
    public TMP_InputField inputField;
    //public List<Data> datas;
    public byte[] testTexture;
    public string[] actionString;

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


    void Start()
    {
        splitStrings.splitNumbers(this,"122 124, americaayaaa");
    }
    void Update()
    {

    }
    public void executeSteps(int stepButton)
    {
        switch(stepButton){
            
            case 0:
                break;
            case 1:
                sendPercentage();
                client.ResponceToClient("AddNumbers, " + int.Parse(inputField.text) + HouseToAddPointsTo);
                break;
        }
        /*
        if(indexForAddPointsSteps < messagesForAddPoints.Count)
        {
            indexForAddPointsSteps ++;
        }
        else if(indexForAddPointsSteps >= messagesForAddPoints.Count)
        {
            indexForAddPointsSteps = 0;
            client.ResponceToClient("AddNumbers, 8");
        }
        */
    }
    public void sendPercentage(){
        //datas[0] = new Data(0, "hoi", 10, 20);
        client.ResponceToClient("AddNumbers, " + 100 / datas[0].aantalLeerlingen * int.Parse(inputField.text) + HouseToAddPointsTo);
    }
    public void selectClass(string House)
    {
        HouseToAddPointsTo = House;
    }
    /*
    public void selectAmountToAdd()
    {

    }
    public void setupAtClient()
    {

    }*/
    public void responceToServerMessage(string message)
    {
        switch(actionString[0])
        {
            case "AddNumbers":
                Debug.Log("komthierrrrrrr");
                Debug.Log(actionString[2]);
                //baseCommandClasses.Add(baseCommands[int.Parse(actionString[2])]);
                //strings.Add(actionString[1]);
                //House.Add(actionString[2]);
                //Debug.Log(House[House.Count - 1]);
                //server.ResponceToClient("respondNumberToAdd");
                break;
            case "Percentage":
            
                break;
        }
    }

}

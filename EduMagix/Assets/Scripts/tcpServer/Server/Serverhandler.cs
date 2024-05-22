using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using System;
public class Serverhandler : MonoBehaviour, IWorkers
{
    public List<BaseCommandClass> baseCommandClasses;
    public List<String> strings;
    public List<String> House;
    public BaseCommandClass CommandSelectClass;
    public bool waitForAnswer = false;
    public TcpServer server;
    public Thread thread;
    public SplitStrings splitStrings = new SplitStrings();
    public string[] actionString;
    public List<BaseCommandClass> baseCommands;

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
        splitStrings.splitNumbers(this, message);
        Debug.Log(actionString[0]);
        switch(actionString[0])
        {

            case "SelectClass":
                server.ResponceToClient("classSelectTrue");
                break;
            case "NumberConfig" :
                server.ResponceToClient("numberSelectTrue");
                break;
            case "AddNumbers":
                baseCommandClasses.Add(baseCommands[int.Parse(actionString[2])]);
                strings.Add(actionString[1]);
                House.Add(actionString[2]);
                Debug.Log(House[House.Count - 1]);
                server.ResponceToClient("respondNumberToAdd");
                break;
        }            
    }
}

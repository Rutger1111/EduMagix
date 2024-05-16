using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ClientHandler : MonoBehaviour, IWorkers
{
    public bool ConfiguratingHouse;
    public bool ConfiguratingNumber;
    //public bool Configurating;
    public List<string> messagesForAddPoints;
    public int indexForAddPointsSteps;
    public Client client;
    public SplitStrings splitStrings = new SplitStrings();
    void Start()
    {
        splitStrings.splitNumbers(this,"122 124, americaayaaa");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            if(indexForAddPointsSteps < messagesForAddPoints.Count)
            {
                indexForAddPointsSteps ++;
            }
            else if(indexForAddPointsSteps >= messagesForAddPoints.Count)
            {
                indexForAddPointsSteps = 0;
                client.ResponceToClient("AddNumbers, 8");
            }
        }
    }
    public void selectClass()
    {

    }
    public void selectAmountToAdd()
    {

    }
    public void setupAtClient()
    {

    }
    public void responceToServerMessage(string message)
    {
        
        switch(message)
        {
            case "classSelectTrue":
                ConfiguratingHouse = true;
                indexForAddPointsSteps ++;
                break;
            case "numberSelectTrue" :
                ConfiguratingNumber = true;
                indexForAddPointsSteps ++;
                break;
            case "respondNumberToAdd":
                indexForAddPointsSteps ++;
                break;
            case "numbersAdded" :
                ConfiguratingHouse = false;
                ConfiguratingNumber = false;
                indexForAddPointsSteps = 0;
                break;

        }
    }
    public void Callback(string[] words, int numbers)
    {
        for (int i = 0; i < words.Length; i++)
        {
            if (Regex.IsMatch(words[i], @"^\d+$") == true){
                Debug.Log(int.Parse(words[i]));
            }
        }
    }
}

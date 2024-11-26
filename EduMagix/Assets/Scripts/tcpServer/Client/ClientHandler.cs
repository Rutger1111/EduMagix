using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClientHandler : MonoBehaviour, IWorkers
{
    public bool ConfiguratingHouse;
    public bool ConfiguratingNumber;
    //public bool Configurating;
    public List<string> messagesForAddPoints;
    public int indexForAddPointsSteps;
    public Client client;
    public SplitStrings splitStrings = new SplitStrings();
    public string HouseToAddPointsTo;
    public TMP_InputField inputField;
    public List<Data> datas;
    public byte[] testTexture;
    void Start()
    {
        splitStrings.splitNumbers(this,"122 124, americaayaaa");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {

        }
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
        datas[0] = new Data(testTexture, "hoi", 10);
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

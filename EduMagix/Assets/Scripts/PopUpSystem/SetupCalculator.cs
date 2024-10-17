using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class SetupCalculator : MonoBehaviour
{
    public int House = 0;
    public float Absents = 0;
    public float Ills = 0;
    public float MaxHours = 0;
    public PopUpSYstem popUpSYstem;
    public Client client;
    public void SetAbstents(TMP_InputField  inputField)
    {
        Absents = int.Parse(inputField.text);
    }
    public void SetMaxHours(TMP_InputField  inputField)
    {
        MaxHours = int.Parse(inputField.text);
    }

    public void CalculatePercentage()
    {
        Debug.Log(MaxHours + "" + Ills + "" + Absents);
        float percentage = 100 / (MaxHours) * (MaxHours - Absents);
        Mathf.RoundToInt(percentage);
        client.ResponceToClient("AddNumbers, " + Mathf.RoundToInt(percentage) + ", " + House);
    }
    public void SetInactive(GameObject PopUpWindow){
        PopUpWindow.SetActive(false);
    }
}

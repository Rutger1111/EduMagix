using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetupCalculator : MonoBehaviour
{
    public float Absents = 0;
    public float Ills = 0;
    public float MaxHours = 0;
    public PopUpSYstem popUpSYstem;
    public void SetAbstents(TMP_InputField  inputField)
    {
        Absents = int.Parse(inputField.text);
    }
    public void SetMaxHours(TMP_InputField  inputField)
    {
        Absents = int.Parse(inputField.text);
    }
    public void SetIlls(TMP_InputField inputField)
    {
        Ills = int.Parse(inputField.text);
    }
    public void CalculatePercentage()
    {
        float percentage = 100 / (MaxHours - Ills) * (Absents);
    }
    public void SetInactive(GameObject PopUpWindow){
        PopUpWindow.SetActive(false);
    }
}

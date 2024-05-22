using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class CommandExecuteSystem : MonoBehaviour
{
    public List<BaseCommandClass> baseCommands;
    public Serverhandler serverhandler;
    public List<string> strings;
    public List<string> Housestrings;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait5Frames());
    }
    void Update()
    {
        if(baseCommands.Count > 0)
        {
            baseCommands[0].House = Housestrings[0];
            baseCommands[0].Invoke(strings[0]);
            baseCommands.Remove(baseCommands[0]);

            Housestrings.Remove(Housestrings[0]);
            strings.Remove(strings[0]);
        }
    }
    public IEnumerator<WaitForSeconds> Wait5Frames()
    {
        yield return new WaitForSeconds(0.08333333333f);
        List<BaseCommandClass> tempBaseCommandClasses = serverhandler.baseCommandClasses;
        List<string> tempStrings = serverhandler.strings;
        List<string> tempHouseStrings = serverhandler.House;
        serverhandler.baseCommandClasses = new List<BaseCommandClass>();
        serverhandler.strings = new List<string>();
        serverhandler.House = new List<string>();
        for (int i = 0; i < tempBaseCommandClasses.Count; i++)
        {
            baseCommands.Add(tempBaseCommandClasses[i]);   
            strings.Add(tempStrings[i]);
            Housestrings.Add(tempHouseStrings[i]);
        }
        StartCoroutine(Wait5Frames());
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class CommandExecuteSystem : MonoBehaviour
{
    public List<BaseCommandClass> baseCommands;
    public List<Data> datas = new List<Data>();
    public ClientHandler clientHandler;
    public List<string> strings;
    public List<string> Housestrings;
    private DebugTextCollector textCollector;
    // Start is called before the first frame update
    void Start()
    {
        textCollector = DebugTextCollector.GetTextCollector();
        StartCoroutine(Wait5Frames());
    }
    void Update()
    {
        if(baseCommands.Count > 0)
        {
            //baseCommands[0].House = Housestrings[0];
            print(datas.Count);
            baseCommands[0].Invoke(datas[0]);

            baseCommands.Remove(baseCommands[0]);
            datas.Remove(datas[0]);
            //Housestrings.Remove(Housestrings[0]);
            //strings.Remove(strings[0]);
        }
    }
    public IEnumerator<WaitForSeconds> Wait5Frames()
    {
        yield return new WaitForSeconds(0.08333333333f);
        List<BaseCommandClass> tempBaseCommandClasses = clientHandler.baseCommandClasses;
        List<Data> tempDatas = clientHandler.datas;
        //List<string> tempStrings = serverhandler.strings;
        //List<string> tempHouseStrings = serverhandler.House;

        clientHandler.baseCommandClasses = new List<BaseCommandClass>();

        clientHandler.datas = new List<Data>();

        //serverhandler.strings = new List<string>();
        //serverhandler.House = new List<string>();
        print("ohho" + tempBaseCommandClasses.Count);

        for (int i = 0; i <  tempBaseCommandClasses.Count; i++)
        {
            print("hohho" +i);

            baseCommands.Add(tempBaseCommandClasses[i]);   

            datas.Add(tempDatas[i]);
            //strings.Add(tempStrings[i]);
            //Housestrings.Add(tempHouseStrings[i]);
        }
        StartCoroutine(Wait5Frames());
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ISliderSetUpCommand : BaseCommandClass
{
    public Dictionary<string, GameObject> sliders = new Dictionary<string, GameObject>();
    public GameObject prefabSlider;
    public override void Invoke(Data data)
    {
        regristerNewKlas(data);
    }

    public override void Invoke(string Input)
    {
        DebugTextCollector.GetTextCollector().AddDebugText("Invoked");

    }

    public void regristerNewKlas(Data data)
    {
        DebugTextCollector textCollector = DebugTextCollector.GetTextCollector();
        try{
            ListOfData listOfData = ListOfData.GetListOfData();
            listOfData.AddData(data);
            if(!sliders.ContainsKey(data.houseName)){
                textCollector.AddDebugText("doesnt contain "+data.houseName);
                sliders[data.houseName] = GameObject.Instantiate(prefabSlider, GameObject.Find("HouseCounters").transform);
                textCollector.AddDebugText("madeslider" + sliders[data.houseName]);
                sliders[data.houseName].GetComponent<SliderSetter>().Set(data);
                textCollector.AddDebugText("settedSlider" + sliders[data.houseName]);
            }
            else{
                textCollector.AddDebugText("is setting slider");
                sliders[data.houseName].GetComponent<SliderSetter>().Set(data);
                textCollector.AddDebugText("Setting completed :)");
            }
        }
        catch(Exception ex){
            textCollector.AddDebugText("" + ex);
        }

        
    }

}

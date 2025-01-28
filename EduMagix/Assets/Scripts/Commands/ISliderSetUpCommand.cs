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
            if(!sliders.ContainsKey(data.houseName) && !sliders.ContainsKey(data.lastHouseName)){
                textCollector.AddDebugText("doesnt contain "+data.houseName);
                sliders[data.houseName] = GameObject.Instantiate(prefabSlider, GameObject.Find("HouseCounters").transform);
                textCollector.AddDebugText("madeslider" + sliders[data.houseName]);
                sliders[data.houseName].GetComponent<SliderSetter>().Set(data);
                textCollector.AddDebugText("settedSlider" + sliders[data.houseName]);
            }
            else if(sliders.ContainsKey(data.lastHouseName) && data.lastHouseName != null){
                GameObject tempSlider = sliders[data.lastHouseName];
                sliders.Remove(data.lastHouseName);
                sliders.Add(data.houseName, tempSlider);
                sliders[data.houseName].GetComponent<SliderSetter>().Set(data);
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
        levelSliders();
        
    }
    public void levelSliders(){
        GameObject houseCounters = GameObject.Find("HouseCounters");
        float totalPoints = 0;
        for (int I = 0; I < houseCounters.transform.childCount; I++){
            totalPoints += houseCounters.transform.GetChild(I).GetComponent<SliderSetter>().housePoints;
        }
        for (int I = 0; I < houseCounters.transform.childCount; I++){
            GameObject sliderObject = houseCounters.transform.GetChild(I).gameObject;
            sliderObject.GetComponent<Slider>().value = (100  / totalPoints) * sliderObject.GetComponent<SliderSetter>().housePoints;
        }
    }

}

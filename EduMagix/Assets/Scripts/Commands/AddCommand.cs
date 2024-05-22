using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddCommand : BaseCommandClass
{
    public Serverhandler serverhandler;
    HouseSystem houseSystem;
    public Slider slider;
    public override void Invoke(string Input)
    {
        Debug.Log("houseeeee" + int.Parse(Input));
        Debug.Log("comesHere");
        updateBalk(int.Parse(Input));
    }

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        HouseSystem.Create();
        houseSystem = HouseSystem.Create();
    }
    public void updateBalk(int amount){
        if(houseSystem.HousePoints.ContainsKey(House) == true){
            houseSystem.HousePoints[House] = houseSystem.HousePoints[House] + amount;
            slider.value = houseSystem.HousePoints[House];
        }
        else{
            houseSystem.HousePoints.Add(House, amount);
            slider.value = houseSystem.HousePoints[House];
        }
        Debug.Log("housepoints" + houseSystem.HousePoints[House]);
    }
    // Update is called once per frame
    void Update()
    {
    }
}

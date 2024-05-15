using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateBalk : MonoBehaviour
{
    HouseSystem houseSystem;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        houseSystem = HouseSystem.Create();
    }

    // Update is called once per frame
    void Update()
    {
        updateBalk();
    }
    public void updateBalk(){
        if(houseSystem.HousePoints.ContainsKey("ClassA") == true){
            houseSystem.HousePoints["ClassA"] ++;
            slider.value = houseSystem.HousePoints["ClassA"];
        }
        else{
            houseSystem.HousePoints.Add("ClassA", 0);
            slider.value = houseSystem.HousePoints["ClassA"];
        }
    }
}

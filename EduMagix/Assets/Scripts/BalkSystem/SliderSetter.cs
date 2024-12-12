using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSetter : MonoBehaviour
{
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Set(Data data){
        slider = gameObject.GetComponent<Slider>();
        slider.value = data.currentAmountOfPoints;
        gameObject.transform.GetChild(3).GetComponent<Image>().sprite = data.convertToSprite();
    }
}

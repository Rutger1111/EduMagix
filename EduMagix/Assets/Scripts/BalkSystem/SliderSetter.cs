using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSetter : MonoBehaviour
{
    public float housePoints;
    public House houseImages;
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
        DebugTextCollector textCollector = DebugTextCollector.GetTextCollector();
        housePoints = data.currentAmountOfPoints;
        textCollector.AddDebugText("slidervalue");
        print("jioefhjuiojegtrhjin" +data.houseImage);
        gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = houseImages.houseTextures[data.houseImage];
        gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = houseImages.houseShields[data.houseImage];
        gameObject.transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = houseImages.houseShields[data.houseImage];
        gameObject.transform.GetChild(5).transform.GetChild(1).GetComponent<ParticleSystem>().startColor = houseImages.houseBubblesColor[data.houseImage];
        gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = houseImages.houseBarColor[data.houseImage];

    }
}

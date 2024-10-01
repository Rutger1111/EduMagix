using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class AddCommand : BaseCommandClass
{
    public Serverhandler serverhandler;
    HouseSystem houseSystem;
    public Slider slider;
    [SerializeField]private int pointsToAdd;
    private bool canCall;
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
            pointsToAdd += houseSystem.HousePoints[House];
        }
        else{
            houseSystem.HousePoints.Add(House, amount);
            pointsToAdd += amount;
        }
        Debug.Log("housepoints" + houseSystem.HousePoints[House]);
    }
    // Update is called once per frame
    void Update()
    {
        if (pointsToAdd > 0)
        {
            StartCoroutine(Add());
        }
    }
    public IEnumerator<WaitForSeconds> Add()
    {
        canCall = false;
        yield return new WaitForSeconds(1);
        slider.value++;
        pointsToAdd--;
        canCall = true;
    }
}

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
    public bool added = true;
    public bool adding;
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
        houseSystem.AddHouse(this);
    }
    public void updateBalk(int amount){
        if(houseSystem.HousePoints.ContainsKey(House) == true){
            houseSystem.HousePoints[House] = houseSystem.HousePoints[House] + amount;
            pointsToAdd += amount;
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
        slider.value = houseSystem.HousePoints[House];
        if (added == false && pointsToAdd > 0)
        {
            print(pointsToAdd);
            added = true;
            adding =true;
            StartCoroutine(Add());
        }
        for (int i = houseSystem.HousePoints.Count -1;i >= 1; i--){

        }

    }
    public IEnumerator<WaitForSeconds> Add()
    {
        canCall = false;
        yield return new WaitForSeconds(0.1f);
        slider.value++;
        pointsToAdd--;
        if(pointsToAdd == 0){
            adding = false;
            added = false;
        }
        canCall = true;
        if(adding == true){
            StartCoroutine(Add()); 
        }

    }
}

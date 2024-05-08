using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HouseSystem
{
    public static HouseSystem balkSystem;
    private HouseSystem(){}
    public Dictionary<string, int> HousePoints = new Dictionary<string, int>();
    public static HouseSystem Create(){
        if(HouseSystem.balkSystem == null){
            HouseSystem.balkSystem = new HouseSystem();
        }
        return HouseSystem.balkSystem;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

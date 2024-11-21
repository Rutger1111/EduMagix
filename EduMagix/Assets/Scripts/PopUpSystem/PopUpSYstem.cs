using System.Collections.Generic;
using UnityEngine;

public class PopUpSYstem : MonoBehaviour
{
    public List<GameObject> PopupWindows;
    public int I = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetNextWIndowActive();
    }
    public void SetNextWIndowActive(){

        if (I > 0){
            PopupWindows[I-1].SetActive(false);
        }
        
    
        PopupWindows[I].SetActive(true);
        
        I ++;  
    }
}

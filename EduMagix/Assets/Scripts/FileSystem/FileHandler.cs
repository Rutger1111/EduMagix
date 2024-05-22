using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileHandler : MonoBehaviour
{
    private List<string> Houses = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        FileSystem.GetFileSystem().Write("ClassA", 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

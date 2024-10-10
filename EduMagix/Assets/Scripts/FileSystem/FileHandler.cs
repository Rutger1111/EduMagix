using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileHandler
{
    private static FileHandler fileHandler;
    //private List<string> Houses = new List<string>();
    public List<SaveData> SavedDatas = new List<SaveData>();
    private FileHandler() { }    

    public static FileHandler GetFileHandler()
    {
        if(fileHandler == null)
        {
            fileHandler = new FileHandler();
            fileHandler.GetSaveData();
        }
        return fileHandler;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("dit is de IP ");

        FileSystem.GetFileSystem().Write("ClassA", 20, "/SaveData");
    }

    // Update is called once per frame
    private void GetSaveData()
    {
        SavedDatas.Add (FileSystem.GetFileSystem().Read("", "/IpAddress.js"));
    }
}

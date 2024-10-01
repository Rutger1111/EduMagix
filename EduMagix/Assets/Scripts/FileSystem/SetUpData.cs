using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpData : MonoBehaviour
{
    public List<SaveData> SavedDatas = new List<SaveData>();

    // Start is called before the first frame update
    void Start()
    {
        SavedDatas = FileHandler.GetFileHandler().SavedDatas;
        print(FileSystem.GetFileSystem().DirectoryPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnIn(){
        
    }
}

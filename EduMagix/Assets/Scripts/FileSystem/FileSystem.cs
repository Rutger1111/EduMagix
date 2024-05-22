using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;

public class FileSystem
{
    private FileSystem() { }
    private static FileSystem fileSystem;
    public static FileSystem GetFileSystem()
    {
        if (fileSystem == null)
        {
            fileSystem = new FileSystem();
        }
        return fileSystem;
    }
    private string DirectoryPath;
        // Start is called before the first frame update
    void Start()
    {

    }
    public void Write(string HouseToSave, int AmountToSave)
    {
        DirectoryPath = Application.persistentDataPath + "/saveData/SaveData.js";
        string saveDataString = JsonUtility.ToJson(new SaveData() {House = HouseToSave, Amount = AmountToSave});
        using StreamWriter streamWriter = new StreamWriter(DirectoryPath);
        streamWriter.Write(saveDataString);
    }
    public void Read(string HouseToRead)
    {
        using StreamReader streamReader = new StreamReader(DirectoryPath);
        string SavedDataString = streamReader.ReadToEnd();
        SaveData savedData = JsonUtility.FromJson<SaveData>(SavedDataString);
    }
    // Update is called once per frame
    void Update()
    {

    }
}

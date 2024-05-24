using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;

public class FileSystem
{
    private FileSystem() { }
    private static FileSystem fileSystem;
    private string DirectoryPath;
    private SaveData saveData;
    public static FileSystem GetFileSystem()
    {
        if (fileSystem == null)
        {
            fileSystem = new FileSystem();
            fileSystem.DirectoryPath = Application.persistentDataPath + "/saveData";
        }
        return fileSystem;
    }

        // Start is called before the first frame update
    void Start()
    {

    }
    public void Write(string HouseToSave, int AmountToSave, string fileName)
    {
        string directoryPath = DirectoryPath + fileName;
        string saveDataString = JsonUtility.ToJson(new SaveData() {House = HouseToSave, Amount = AmountToSave});
        using StreamWriter streamWriter = new StreamWriter(DirectoryPath + fileName);
        streamWriter.Write(saveDataString);
    }
    public SaveData Read(string HouseToRead, string FileName)
    {
        using StreamReader streamReader = new StreamReader(DirectoryPath + FileName);
        string SavedDataString = streamReader.ReadToEnd();
        saveData = new SaveData();
        JsonUtility.FromJsonOverwrite(SavedDataString, saveData);
        return saveData;
    }
    // Update is called once per frame
    void Update()
    {

    }
}

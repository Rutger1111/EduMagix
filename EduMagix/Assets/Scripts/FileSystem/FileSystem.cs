using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;

public class FileSystem
{

    private string DirectoryPath = Application.persistentDataPath + "/SaveData";
    private string ZipLocation = Application.persistentDataPath;
    string[] files;
    // Start is called before the first frame update
    void Start()
    {
        files = Directory.GetFiles(DirectoryPath);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

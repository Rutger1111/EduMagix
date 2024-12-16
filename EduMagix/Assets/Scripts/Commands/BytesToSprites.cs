using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BytesToSprites : BaseCommandClass
{
    Texture2D tex;
    Byte[] fileData;
    public override void Invoke(string Input)
    {
        if(File.Exists(Input)){
            fileData = File.ReadAllBytes(Input);
            tex  = new Texture2D(2,2);
            tex.LoadImage(fileData); 
        }
        return;
    }
    public void hallo(){

    }

    public override void Invoke(Data data)
    {
        throw new NotImplementedException();
    }
}

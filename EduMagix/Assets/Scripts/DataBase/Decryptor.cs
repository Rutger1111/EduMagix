using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Decryptor : MonoBehaviour
{
    public Data DeserializeDB(byte[] bytes){
        Data data = null;
        try{
            var formatter  = new BinaryFormatter();
            
            using(var stream = new System.IO.MemoryStream(bytes)){
                Data data1 = (Data)formatter.Deserialize(stream);
                data = data1;
            }
        }
        catch(Exception ex){
            DebugTextCollector.GetTextCollector().AddDebugText("deserialization error: " + ex);
        }

        return data;

    }
}

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
            BinaryFormatter formatter  = new BinaryFormatter();
            
            using(var stream = new System.IO.MemoryStream(bytes, true)){
                stream.Position = 0;
                data = (Data)formatter.Deserialize(stream);
                DebugTextCollector.GetTextCollector().AddDebugText("data" + data);
            }
        }
        catch(Exception ex){
            DebugTextCollector.GetTextCollector().AddDebugText("deserialization error: " + ex);
        }

        return data;

    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Decryptor : MonoBehaviour
{
    public Data DeserializeDB(byte[] bytes){
        var formatter  = new BinaryFormatter();
        using(var stream = new System.IO.MemoryStream(bytes)){
            Data data = (Data)formatter.Deserialize(stream);
            return data;
        }

    }
}

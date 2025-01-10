using System;
using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;
public class Decryptor : MonoBehaviour
{
    public Data DeserializeDB(byte[] bytes)
    {
        Data data = null;
        try
        {
            string json = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log(bytes.Length);
            Debug.Log(json.Length);
            Debug.Log(json);
            DebugTextCollector.GetTextCollector().AddDebugText(json);
            data = JsonUtility.FromJson<Data>(json);
        }
        catch (Exception ex)
        {
            DebugTextCollector.GetTextCollector().AddDebugText("deserialization error: " + ex);
        }
        return data;
    }

    public byte[] SerializeDB(Data data)
    {
        byte[] bytes = null;
        try
        {
            string json = JsonUtility.ToJson(data);
            bytes = System.Text.Encoding.UTF8.GetBytes(json);
        }
        catch (Exception ex)
        {
            DebugTextCollector.GetTextCollector().AddDebugText("serialization error: " + ex);
        }
        return bytes;
    }

}

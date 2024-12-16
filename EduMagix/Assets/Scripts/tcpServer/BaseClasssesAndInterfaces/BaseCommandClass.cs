using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCommandClass : MonoBehaviour
{

    public string House;
    private byte[] bytes;
    public abstract void Invoke(Data data);
    public abstract void Invoke(string Input);
}

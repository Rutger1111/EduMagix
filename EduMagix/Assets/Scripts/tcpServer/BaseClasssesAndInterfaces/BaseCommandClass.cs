using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCommandClass : MonoBehaviour
{
    public string House;
    public abstract void Invoke(string Input);
}

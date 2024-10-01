using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SplitStrings
{
    public string number = "123 USA, America";
    private char[] splitChars;

    // Start is called before the first frame update
 
    public void splitNumbers(IWorkers workers, string stringToSplit)
    {
        splitChars = " ,".ToArray();
        string[] numbers = stringToSplit.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
        workers.Callback(numbers, number[0]);
    }
}

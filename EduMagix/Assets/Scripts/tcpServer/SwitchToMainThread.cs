using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class SwitchToMainThread
{
    private static SwitchToMainThread switchToMainThread;
    private SwitchToMainThread(){}
    public List<BaseCommandClass> baseCommands;

    public static SwitchToMainThread Create()
    {
        if (SwitchToMainThread.switchToMainThread == null){
            switchToMainThread = new SwitchToMainThread();
        }
        return switchToMainThread;
    }
    public IEnumerator ThisWillBeExecutedOnTheMainThread() {
        Debug.Log ("This is executed from the main thread");
        yield return null;
    }
    public void ExampleMainThreadCall() {
    }
    public List<BaseCommandClass> getBaseCommandClasses(){
        return baseCommands;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the architecture of this script is entierly dependend on the architecture of our signalListener script . inbetween those two script we exchange signals, save them
// within a "signalListener" Type List in the Signal script, run through all the list entries and invoke our signal event in the Signal Listener Script

[CreateAssetMenu]
public class SignalSys : ScriptableObject
{
  public List<SignalListener> listeners = new List<SignalListener>(); // creating a List of Signal Listeners

    public void Raise() //this method will run through all our List elements and run a specific method we are going to create on the SignalListener side
    {
        for (int i = listeners.Count -1; i>=0; i--) // we run through the list backwards to circumvent a out of range exception
        {
            listeners[i].OnSignalRaised(); //here we call for all elements in the list the function OnSignalRaised from SignalListener
        }
    }
    public void RegisterListener(SignalListener listener) //here we register if a Signal gets send and add it to our Listeners list
    {
        listeners.Add(listener);
    }

    public void DeRegisterListener(SignalListener listener) //here we register if a deacitvate-signal gets send and substract it from our listeners list
    {
        listeners.Remove(listener);
    }
}

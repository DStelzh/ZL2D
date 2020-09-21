using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// the architecture of this script is entierly dependend on the architecture of our signal script (scriptable object class). inbetween those two script we exchange signals, save them
// within a "signalListener" Type List in the Signal script, run through all the list entries and invoke our signal event in those

public class SignalListener : MonoBehaviour
{
    public SignalSys asignal; //reference to the signal script
    public UnityEvent signalEvent; //ref. to signal event

   public void OnSignalRaised() //when this function gets called
    {
        signalEvent.Invoke(); //the signal event gets called
    }

    private void OnEnable() //when it gets enabled (signal send)
    {
        asignal.RegisterListener(this); //we tell the signal script to add _this_ to the list
    }
    private void OnDisable() //when it gets disabled (deactivate signal send)
    {
        asignal.DeRegisterListener(this); // we tell teh signal scrip tto substract _this_from the list
    }
}

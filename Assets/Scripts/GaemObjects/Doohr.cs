using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorTypeFlat { keyf, buttonf, enemyf }
public class Doohr : Interactable
{
    [Header("Door Variables")]
    public DoorTypeFlat thisDoorTypef;
    public bool isOpenf = false;
    public Animator anim;
    public Inventory playerInvf;
    public BoxCollider2D doorBoxColliderf;


    
    private void Update()
    {
        if (Input.GetButtonDown("interact"))
        {
            if (playerInRange && thisDoorTypef == DoorTypeFlat.keyf)
            {
                // does player have key?
                if(playerInvf.numberOfKeys > 0)
                {
                    // remove a key and
                    playerInvf.numberOfKeys--;
                     //well call open method 
                    Open();
                }
            }

            
        }
    }

    public void Open()
    {
        // animate opening => set bool true for opening in animator
        anim.SetBool("opened", true);
        // set bool in this script to trrue
        isOpenf = true; 
        // deactivate box collider of object 
        doorBoxColliderf.enabled = false;
    }

    public void Close()
    {

    }
}

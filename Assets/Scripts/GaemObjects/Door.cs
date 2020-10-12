using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType { key, button, enemy }
public class Door : Interactable
{
    [Header("Door Variables")]
    public DoorType thisDoorType;
    public bool isOpen = false;
    public SpriteRenderer doorSprite;
    public Inventory playerInv;
    public BoxCollider2D doorBoxCollider;



    private void Update()
    {
        if (Input.GetButtonDown("interact"))
        {
            if (playerInRange && thisDoorType == DoorType.key)
            {
                // does player have key?
                if (playerInv.numberOfKeys > 0)
                {
                    // remove a key and
                    playerInv.numberOfKeys--;
                    //well call open method 
                    Open();
                }
            }


        }
    }

    public void Open()
    {
        doorSprite.enabled = false;
        // set bool in this script to trrue
        isOpen = true;
        // deactivate box collider of object 
        doorBoxCollider.enabled = false;
    }

    public void Close()
    {
        doorSprite.enabled = true;
        // set bool in this script to trrue
        isOpen = false;
        // deactivate box collider of object 
        doorBoxCollider.enabled = true;
    }
}

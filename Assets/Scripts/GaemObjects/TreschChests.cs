using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreschChests : Interactable
{
    [Header("Contents")]
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public BoolValue storedOpen;

    [Header("Signals and Dialog")]
    public SignalSys itemRaise;
    public GameObject dialogeWinow;
    public Text dialogText;

    [Header("Animation")]
    public Animator anim; 

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = storedOpen.RuntimeValue;
        if (isOpen)
        {
            anim.SetBool("opened", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("interact") && playerInRange)
        {
            if (!isOpen)
            {
                OpenChest();
            }
            else
            {
                ChestAlreadyOpen();
            }
        }
    }

    public void OpenChest()
    {
        // Dialog box on
        dialogeWinow.SetActive(true);
        // Dialog text = content text
        dialogText.text = contents.itemDescription;
        // add contents to inv
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        // Rais the Signal to Player to animate correctly
        itemRaise.Raise();
        // raise context clue 
        context.Raise();
        // set chest to opened
        isOpen = true;
        anim.SetBool("opened", true);
        storedOpen.RuntimeValue = isOpen;
    }

    public void ChestAlreadyOpen()
    {
    
        // DIalog off
        dialogeWinow.SetActive(false);
      
        // raise the signal to the player to stop animating
        itemRaise.Raise();
       
       
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger  && !isOpen)
        {
            context.Raise();
            playerInRange = false;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Sign : Interactable   
{
    
    public string textToWrite;
    public Text dialogText;
    public GameObject dialogField;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("interact") && playerInRange)
        {
            if (dialogField.activeInHierarchy)
            {
                dialogField.SetActive(false);
            }
            else
            {
                dialogField.SetActive(true);
                dialogText.text = textToWrite;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = false;
            dialogField.SetActive(false);
        }
    }
}

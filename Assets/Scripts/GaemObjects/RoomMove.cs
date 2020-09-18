using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  //for UI usage

public class RoomMove : MonoBehaviour
{

    public Vector2 camChange; // reference vector for our camera change (minmax values etc)
    public Vector3 playerChange; // reference vector for our change of player pos
    private PlayerCam cam; //reference to our PlayerCam script (for move cam)
    public bool needText; // we use this bool to check if we need to display a tilecard or not (cuz not evry area needs it)
    public string placeName; // we use this string to enter what the name of the place is we enter
    public GameObject text; // this is the reference to our text object itself
    public Text placeText; // this is the reference to the textfield, we use that later so we can set our placeName into the Text


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<PlayerCam>(); // completing of the reference to the PlayerCam script -> Camera.main accesses the main Camera in the scene, GetComponent gets the component assigned to that camera
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collisioninfo) //unitys detection function for entering a trigger, saves info about the Collider2D (boxcollider e.g.) in the variable we named collisioninfo
    {
        if (collisioninfo.CompareTag("Player") && !collisioninfo.isTrigger) //if the tag of the collider is "player" we run this function
        {
            cam.minPos += camChange; // creating references in our script for unity to fill out. here we add our value where our next room begins to our min and max value so the script stays true for the upper room
            cam.maxPos += camChange; 
            collisioninfo.transform.position += playerChange;  // creating a reference to our player position and adding a value to set him into the next "room"

            if (needText) //if need text = true 
            {
                StartCoroutine(PlaceNameCo()); // we starting Place Name Coroutine
            }
        }
    }

    private IEnumerator PlaceNameCo() // IEnumerator lets us iterate over a non-generic collection
    {
        text.SetActive(true); // here we set the Textfield active so it appears
        placeText.text = placeName; // in here we set the text to the string we have in the script 
        yield return new WaitForSeconds(4); // yield : instead of returning everything at once, yield return returns 1 value and when we call the function again it starts where the yield has ended
        // here we set the yield return to wait 4 seconds each time
        text.SetActive(false); //after 4 secs we set the text inactive again
    }

}

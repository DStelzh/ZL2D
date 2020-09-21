
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates { walk, attack, interact, stagger, idle} // our states so far

public class PlayerMovement : MonoBehaviour
{
    [Header("State Machine")]
    public PlayerStates currentState; // our state Var

    [Header("Player Stats")]
    public float speed; // a var we can change in the inspector

    [Header("Player References")]
    private Rigidbody2D myRigidBody;  // reference to our rigidbody component
    private Vector3 change; // reference to a vector3 -> change dir. player is going, vector 3 for the transform.position command 
    private Animator myAnimator; //reference to our animator for player animaton

    [Header("Player Signals")]
    public FloatValue currentHealth; // adding to reference our Scr.Object that holds info for the Signal System (Name PlayeHealth of scrobj)
    public SignalSys playerHealthSignal; //a reference to our signal, that we can signal our healthsignal system -> with that we are going to reduce the hearts
    public VectorVal startingPosition; // a reference to our ScrObj. , we are going to use this for scene transitions, so we will use it in start, as far as what it does: it calles a a stored pos when changing scenes (entrance e.g.)
    public Inventory playerInventory;
    public SpriteRenderer recievedItemSprite;
    public SignalSys playerHit;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerStates.walk; // at the start of the load, we auto set our player in our walk state
        myRigidBody = GetComponent<Rigidbody2D>(); // at the start grab the Rigidbody2d component on the Object this script is sitting on
        myAnimator = GetComponent<Animator>(); // at the start grad the animator for the player model
        myAnimator.SetFloat("Horizontal", 0);
        myAnimator.SetFloat("Vertical", -1);
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        //check if the player is interacting with something or not
        if (currentState == PlayerStates.interact)
        {
            return; //just return to the update methods, dont check inputs at all
        }
        change = Vector3.zero; // every Frame we want to reset how the player has changed 
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && currentState != PlayerStates.attack && currentState != PlayerStates.stagger) // if our player presses the in unity input assigned button for attack and isnt already in the attack state we run the coroutine for attack
        {
            StartCoroutine(AttackCo());
        }

        
    }
    void FixedUpdate()
    {
        if (currentState == PlayerStates.walk || currentState == PlayerStates.idle) //else if our player is in the walk state, we let him do the movement animations and actual movement
        {
            UpdateMovementAndAnimaton();
        }
    }

    private IEnumerator AttackCo() // here we start our Coroutine for Attacking
    {
        myAnimator.SetBool("attacking", true); //we set our bool for attacking to true
        currentState = PlayerStates.attack; // we set our state to attack
        yield return null; // this waits for 1 frame
        myAnimator.SetBool("attacking", false); // then we are going to stop the bool true, else we start the animation all the time
        yield return new WaitForSeconds(.2f); //here we wait for the animation to stop in game
        if (currentState != PlayerStates.interact) // and check if we arent in an interaction
        {
            currentState = PlayerStates.walk; // and finally set our player state to walk
        }
        

    }
    public void RaiseItem()
    {
        if(playerInventory.currentItem != null)
        {
        if(currentState != PlayerStates.interact)
        {
            myAnimator.SetBool("recieve Item", true);
            currentState = PlayerStates.interact;
            recievedItemSprite.sprite = playerInventory.currentItem.itemSprite;
        } else
        {
            myAnimator.SetBool("recieve Item", false);
            currentState = PlayerStates.idle;
            recievedItemSprite.sprite = null;
                playerInventory.currentItem = null;
        }
        }
        
       

    }
    void UpdateMovementAndAnimaton()
    {
        // then we are going to call our MoveCharacter method to move our rigidody somewhere
        if (change != Vector3.zero)
        {
            MoveCharacter();
            myAnimator.SetFloat("Horizontal", change.x); //setting the floats in our animator to get our animaton
            myAnimator.SetFloat("Vertical", change.y);
            myAnimator.SetBool("Moving", true); // and set the bool for activating the animation to true
        }
        else
        {
            myAnimator.SetBool("Moving", false); // if our change vector reverts to zero (as it does every frame we dont press anything) the moving bool is set to false and we revert to our idle state
        }

    }

    void MoveCharacter() // if we want to move the character from other places, e.g. onboard buttons
    {
        change.Normalize();
        myRigidBody.MovePosition(transform.position  + change * speed * Time.fixedDeltaTime); 
    }
    public void Knock(float knockTime, float damage) //this method gets called withing our knockback script if the player gets hit
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockCo(float knockTime) // our coroutine that requires us to put in a float for the knocktime, as myRigidbody is already defined in there
    {
        if (myRigidBody != null) //if there is an enemy (not ded e.g.)
        {
            playerHit.Raise();
            yield return new WaitForSeconds(knockTime); // we wait for our knock time 
            myRigidBody.velocity = Vector2.zero; //then we set the velocity of our enemy to 0
            currentState = PlayerStates.idle; // and finally set the state of the Enemy to idle again
            myRigidBody.velocity = Vector2.zero;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Daemonette : Enemies
{
    public Rigidbody2D myRigidbody; //rb component
    public Transform target; // our target for radius calc
    public float chaseRadius; // our float for our chaseradius
    public float attackRadius; // -""- for our attak radius
    public Transform homePosition; //not used yet, but we are going to fill this out to where our mob should return
    public Animator anim; //reference to our naimator 

    // Start is called before the first frame update
    void Start()
    {
        currentState = enemyStates.idle; // set our state to idle at the start
        myRigidbody = GetComponent<Rigidbody2D>(); // complete our reference to the rb compinent
        anim = GetComponent<Animator>(); //completing our reference to our Animator Component
        target = GameObject.FindWithTag("Player").transform; //completion of the reference of our target
        anim.SetBool("WakeUp", true);
    }

    
    void FixedUpdate()
    {
        CheckDistance(); //our method checkD
    }
    
    public virtual void CheckDistance() //this method is used to check if a) enemy in chase radious b) out of attck radius c) player state is either idle or walk d) an not staggered
    {
        if (Vector3.Distance(transform.position, target.position) <= chaseRadius && Vector3.Distance(transform.position, target.position) > attackRadius) //our if to check the radi (<chase and >attack)
        {
            if (currentState == enemyStates.idle || currentState == enemyStates.walk && currentState != enemyStates.stagger) //state check
            {

                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime); //we are going to save our movement vector as temp
                changeAnim(temp - transform.position); // we call our changeANim method, which is responsible for feeding the right parameters into our Animator for the right movement
                myRigidbody.MovePosition(temp); //then use that vector to move our RB
                ChangeState(enemyStates.walk); // set our state to walk when we start walking
                anim.SetBool("WakeUp", true); //when our player gets within our Radius and our mob wakes up, we are setting our Animator Parameter Bool Wake Up to true, to start our wakeup process
            }
        } else
        {
            anim.SetBool("WakeUp", false); //we set our bool to false if the player is outside the chase radius
        }
    }

    public void changeAnim(Vector2 direction) // we put in an v2 
    {
        direction = direction.normalized;  // normalize it (length 1)
        anim.SetFloat("moveX", direction.x); // when horizontal move x float
        anim.SetFloat("moveY", direction.y); // hen vertical move y float
    }
    private void ChangeState(enemyStates newState) //our change state method
    {
        if(currentState != newState) //if the state isnt the one put in, we set it to the one we want
        {
            currentState = newState; // setting ou rstate as new state, so whatever we input in this method -> if the state != to the state we input, we change it to be that state
        }
    }
}

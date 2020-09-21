using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;
    
   
    private void OnTriggerEnter2D(Collider2D other) //on trigger enter to check if we get a hit
    {
        if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player")) // breakable
        {
            other.GetComponent<Pot>().Smash(); // we run the method smash of the pot
        }
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player")) //if we hit smth with the tag enemy or player
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>(); //here we set a reference to our chosen rigidbody and call it hit
            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position; //here we create a vector that is the diff between enemy - player (important in 2 lines why from enemy to player not reverse)
                difference = difference.normalized; // we normalize this vector (length = 1)
                hit.AddForce(difference, ForceMode2D.Impulse); // then we add a Force to the RB of our enemy, in the direction of difference (so behind the enemy) 
                                                               //and we use the force mode impluse (similar to 3d ForceMode.VelocityChange))
                if (other.gameObject.CompareTag("enemy") && other.isTrigger)
                {
                    hit.GetComponent<Enemies>().currentState = enemyStates.stagger; //here we reference the enemies inheritance script of our enemy and set the state to staggered and use hit as the Rigidbody reference
                    other.GetComponent<Enemies>().Knock(hit, knockTime, damage); // we call our Knockback coroutine from our Enemies script -> we use other here as we need to do this via the boxcollider 
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    if (other.GetComponent<PlayerMovement>().currentState != PlayerStates.stagger)
                    {
                        hit.GetComponent<PlayerMovement>().currentState = PlayerStates.stagger; //here we referene the PlayerMovementscript and set the state of our player to staggered
                        other.GetComponent<PlayerMovement>().Knock(knockTime, damage); // we call our Knockback coroutine from the Player script, therefore only float needed
                    }
                }
                

            }
        }
       
    }
}
    


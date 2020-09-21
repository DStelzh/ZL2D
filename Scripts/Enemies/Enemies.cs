using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyStates { idle, walk, attack, stagger}
public class Enemies : MonoBehaviour
{
    [Header("State Machine")]
    public enemyStates currentState;

    [Header("Enemy Stats")]
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    [Header("Death Effects")]
    public GameObject deathEffekt;
    public float deathWaitTime;


    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            DeathEffekt();
            this.gameObject.SetActive(false);
        }
    }
    private void DeathEffekt()
    {
        if (deathEffekt != null)
        {
            GameObject effekt = Instantiate(deathEffekt, transform.position, Quaternion.identity);
            Destroy(effekt,deathWaitTime);
        }
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage) //we use this function to get called outside of the script and run our Knockback coroutine
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        TakeDamage(damage);
    }

    public IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime) // our coroutine that requires us to put in a rb component and a float for the knocktime
    {
        if (myRigidbody != null) //if there is an enemy (not ded e.g.) and the enemy sint staggered
        {
            yield return new WaitForSeconds(knockTime); // we wait for our knock time 
            myRigidbody.velocity = Vector2.zero; //then we set the velocity of our enemy to 0
            currentState = enemyStates.idle; // and finally set the state of the Enemy to idle again
            myRigidbody.velocity = Vector2.zero;
        }
    }
}

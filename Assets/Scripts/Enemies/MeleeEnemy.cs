using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Daemonette
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void CheckDistance() //this method is used to check if a) enemy in chase radious b) out of attck radius c) player state is either idle or walk d) an not staggered
    {
        if (Vector3.Distance(transform.position, target.position) <= chaseRadius && Vector3.Distance(transform.position, target.position) > attackRadius) //our if to check the radi (<chase and >attack)
        {
            if (currentState == enemyStates.idle || currentState == enemyStates.walk && currentState != enemyStates.stagger) //state check
            {

                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime); //we are going to save our movement vector as temp
                changeAnim(temp - transform.position); // we call our changeANim method, which is responsible for feeding the right parameters into our Animator for the right movement
                myRigidbody.MovePosition(temp); //then use that vector to move our RB
                ChangeState(enemyStates.walk); // set our state to walk when we start walking

            }
        }
        else if (Vector3.Distance(transform.position, target.position) <= chaseRadius && Vector3.Distance(transform.position, target.position) <= attackRadius)
        {
            if (currentState == enemyStates.idle || currentState == enemyStates.walk && currentState != enemyStates.stagger)
            {
                StartCoroutine(AttackCo());
            }
        }
    }

    public IEnumerator AttackCo()
    {
        currentState = enemyStates.attack;
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(1f);
        currentState = enemyStates.walk;
        anim.SetBool("Attack", false);
    }

}

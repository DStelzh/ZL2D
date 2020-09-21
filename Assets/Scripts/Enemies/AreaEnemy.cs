using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnemy : Daemonette
{
    public Collider2D boundary;

    public override void CheckDistance()
    {
        if (Vector3.Distance(transform.position, target.position) <= chaseRadius && Vector3.Distance(transform.position, target.position) > attackRadius && boundary.bounds.Contains(target.transform.position)) //our if to check the radi (<chase and >attack)
        {
            if (currentState == enemyStates.idle || currentState == enemyStates.walk && currentState != enemyStates.stagger) //state check
            {

                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime); //we are going to save our movement vector as temp
                changeAnim(temp - transform.position); // we call our changeANim method, which is responsible for feeding the right parameters into our Animator for the right movement
                myRigidbody.MovePosition(temp); //then use that vector to move our RB
                ChangeState(enemyStates.walk); // set our state to walk when we start walking
                anim.SetBool("WakeUp", true); //when our player gets within our Radius and our mob wakes up, we are setting our Animator Parameter Bool Wake Up to true, to start our wakeup process
            }
        }
        else if (Vector3.Distance(target.position,transform.position)> chaseRadius ||  !boundary.bounds.Contains(target.transform.position))
        {
            anim.SetBool("WakeUp", false); //we set our bool to false if the player is outside the chase radius
        }
    }
}

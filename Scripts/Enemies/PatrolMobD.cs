using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMobD : Daemonette
{
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingN;

    public override void CheckDistance() //this method is used to check if a) enemy in chase radious b) out of attck radius c) player state is either idle or walk d) an not staggered
    {
        if (Vector3.Distance(transform.position, target.position) <= chaseRadius && Vector3.Distance(transform.position, target.position) > attackRadius) //our if to check the radi (<chase and >attack)
        {
            if (currentState == enemyStates.idle || currentState == enemyStates.walk && currentState != enemyStates.stagger) //state check
            {

                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime); //we are going to save our movement vector as temp
                changeAnim(temp - transform.position); // we call our changeANim method, which is responsible for feeding the right parameters into our Animator for the right movement
                myRigidbody.MovePosition(temp); //then use that vector to move our RB
                //ChangeState(enemyStates.walk); // not going to change in this script
                anim.SetBool("WakeUp", true); //when our player gets within our Radius and our mob wakes up, we are setting our Animator Parameter Bool Wake Up to true, to start our wakeup process
            }
        }
        else if (Vector3.Distance(target.position, transform.position)> chaseRadius)
        {
            if (Vector3.Distance(transform.position, path[currentPoint].position)> roundingN)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position); // we call our changeANim method, which is responsible for feeding the right parameters into our Animator for the right movement
                myRigidbody.MovePosition(temp); //then use that vector to move our RB
            }
            else
            {
                ChangeGoal();
            }

        }
    }

    private void ChangeGoal()
    {
        if(currentPoint == path.Length - 1)
        {
            currentPoint = 0;
            currentGoal = path[0];
        }
        else
        {
            currentPoint++;
            currentGoal = path[currentPoint];
        }
    }
}

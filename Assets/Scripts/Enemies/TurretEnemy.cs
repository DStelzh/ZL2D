using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Daemonette
{
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySec;
    public bool canFire = true;

    private void Update()
    {
        fireDelaySec -= Time.deltaTime;
        if (fireDelaySec<= 0)
        {
            canFire = true;
            fireDelaySec = fireDelay;
        }
    }

    public override void CheckDistance() //this method is used to check if  out of attck radius c) player state is either idle or walk d) an not staggered
    {
        if (Vector3.Distance(transform.position, target.position) <= chaseRadius && Vector3.Distance(transform.position, target.position) > attackRadius) //our if to check the radi (<chase and >attack)
        {
            if (currentState == enemyStates.idle || currentState == enemyStates.walk && currentState != enemyStates.stagger) //state check
            {
                if (canFire == true)
                {
                    Vector3 tempVect = target.transform.position - transform.position;
                    GameObject tempProj = Instantiate(projectile, transform.position, Quaternion.identity);
                    tempProj.GetComponent<BaseProjectile>().Launch(tempVect);
                    canFire = false;
                    ChangeState(enemyStates.idle); // set our state to walk when we start walking
                    anim.SetBool("WakeUp", true); //when our player gets within our Radius and our mob wakes up, we are setting our Animator Parameter Bool Wake Up to true, to start our wakeup process
                }
                
             
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("WakeUp", false); //we set our bool to false if the player is outside the chase radius
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private Animator anim; //open our reference to an animator item
    public Loottable thisLoot;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); //here we get the animator component of the object this script is sitting on (pot)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Smash()
    {
        anim.SetBool("smash", true); //we set our animator bool for smash true, which runs the animation of breaking
        StartCoroutine(BreakCo()); //then we set our coroutine active
        MakeLoot();
    }
    IEnumerator BreakCo()
    {
        yield return new WaitForSeconds(0.33f); //we wait for 0.3 secs
        this.gameObject.SetActive(false); //and disable the object (so the rubble dissappears)
    }
    private void MakeLoot()
    {
        if (thisLoot != null)
        {
            PowerUpObj current = thisLoot.LootPowerup();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
}

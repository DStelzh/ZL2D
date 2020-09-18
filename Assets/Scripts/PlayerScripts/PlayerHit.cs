using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) //if we hit smth that has the tag
    {
        if (other.CompareTag("breakable")) // breakable
        {
            other.GetComponent<Pot>().Smash(); // we run the method smash of the pot
        }
    }
}

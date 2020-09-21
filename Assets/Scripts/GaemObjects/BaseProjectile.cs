using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [Header("TargetingStuff")]
    public float speedOfProjectile;
    public Vector2 directionToMove;

    [Header("Time relevant Stuff")]
    public float lifeTime;
    private float lyfTymSecs;

    [Header("Components")]
    public Rigidbody2D myrb;


    // Start is called before the first frame update
    void Start()
    {
        myrb = GetComponent<Rigidbody2D>();
        lyfTymSecs = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        lyfTymSecs -= Time.deltaTime;
        if (lyfTymSecs <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Launch(Vector2 initialPlaceToGo)
    {
        myrb.velocity = initialPlaceToGo * speedOfProjectile;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }
}

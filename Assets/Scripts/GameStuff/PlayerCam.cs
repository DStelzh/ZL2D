using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("Position Variables")]
    public Transform player; // ref to our player transform
    public float smoothing; //float we set in unity to smoothen the cam movement
    public Vector2 minPos; //a vector that saves our min pos
    public Vector2 maxPos; // a vector that saves our max pos

    [Header("Animator Variables")]
    public Animator anim;

    [Header("Position Reset")]
    public VectorVal camMin;
    public VectorVal camMax;

    public void Start()
    {
        maxPos = camMax.initialValue;
        minPos = camMin.initialValue;
        anim = GetComponent<Animator>();
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
    // LateUpdate is called at the End of the Framecount of Update
    void LateUpdate()
    {
        if (transform.position != player.position) //if the cam is not on the player
        {
            Vector3 targetpos = new Vector3(player.position.x, player.position.y, transform.position.z); //we set our target position 0> the players x,y and z
            targetpos.x = Mathf.Clamp(targetpos.x, minPos.x, maxPos.x); //we clamp our x by our min/max x values
            targetpos.y = Mathf.Clamp(targetpos.y, minPos.y, maxPos.y); // we clamp our y by our min/max y values
            transform.position = Vector3.Lerp(transform.position, targetpos, smoothing); //we move our camera pos with lerpo, which has a startpos, targetpos and a float which decides how fast we move while correcting our pos
        }
    }
    public void BeginKick()
    {
        anim.SetBool("Kickactive", true);
        StartCoroutine(KickCO());
    }

    public IEnumerator KickCO()
    {
        yield return null;
        anim.SetBool("Kickactive", false);
    }
}

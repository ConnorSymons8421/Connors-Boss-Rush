using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float leftAndRightEdge = 8.5f;
    public float upAndDownEdge = 4.6f;
    private int dashCD = 0;
    private int parryCD = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //track dash and parry cooldowns
        dashCD++;
        parryCD++;
        if (dashCD > 300)
        {
            //Display dash ready
        }
        if (parryCD > 300)
        {
            //Display parry ready
        }

        //move player using WASD
        Vector3 pos = this.transform.position;
        if (Input.GetKey(KeyCode.A)) pos.x -= .0025f;
        if (Input.GetKey(KeyCode.D)) pos.x += .0025f;
        if (Input.GetKey(KeyCode.W)) pos.y += .0025f;
        if (Input.GetKey(KeyCode.S)) pos.y -= .0025f;


        //do not move player if position would be out of bounds
        if (pos.x > leftAndRightEdge) pos.x = leftAndRightEdge;
        if (pos.x < -leftAndRightEdge) pos.x = -leftAndRightEdge;
        if (pos.y > upAndDownEdge) pos.y = upAndDownEdge;
        if (pos.y < -upAndDownEdge) pos.y = -upAndDownEdge;


        this.transform.position = pos;
    }


    void FixedUpdate()
    {
        
    }
}

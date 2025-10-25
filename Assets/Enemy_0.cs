using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : MonoBehaviour
{
    public float leftEdge = 6f;
    public float rightEdge = 8f;
    public float upAndDownEdge = 3.2f;

    public int movementCD = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementCD++;
        //move a random amount within range every 1000 frames
        if (movementCD >= 1000)
        {
            float deltaX = (Random.value * 2) - 1;
            float deltaY = ((Random.value * 2) - 1)*2;

            Vector3 pos = this.transform.position;
            pos.x += deltaX;
            pos.y += deltaY;

            //keep values within range
            if (pos.x > rightEdge) pos.x = rightEdge;
            if (pos.x < leftEdge) pos.x = leftEdge;
            if (pos.y > upAndDownEdge) pos.y = upAndDownEdge;
            if (pos.y < -upAndDownEdge) pos.y = -upAndDownEdge;

            this.transform.position = pos;

            //reset movement cooldown
            movementCD = 0;
        }
    }
}

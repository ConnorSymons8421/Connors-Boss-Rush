using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble_Small_Diag_Down : MonoBehaviour
{
    public float edgeScreen_x = -10f;

    private float velocity_x = -0.0035f;
    private float velocity_y = 0.001f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        pos.x += velocity_x;
        pos.y += velocity_y;
        this.transform.position = pos;

        //destroy if offscreen
        if (transform.position.x < edgeScreen_x)
        {
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float leftAndRightEdge = 8.5f;
    public float upAndDownEdge = 4.6f;
    public float movementAmount = .0025f;
    public int abilityCD = 3000;


    private int dashCD = 0;
    private int parryCD = 0;
    private bool dashReady = false;
    private bool parryReady = false;
    private bool dashActive = false;
    private bool parryActive = false;
    

    // Update is called once per frame
    void Update()
    {
        //track dash and parry cooldowns
        UpdateCD();

        //activate dash/parry
        if(Input.GetKey(KeyCode.J) && dashReady == true)
        {
            ActivateDash();
        }
        if (Input.GetKey(KeyCode.L) && parryReady == true)
        {
            ActivateParry();
        }

        //move player using WASD
        Vector3 pos = this.transform.position;
        if (Input.GetKey(KeyCode.A)) pos.x -= movementAmount;
        if (Input.GetKey(KeyCode.D)) pos.x += movementAmount;
        if (Input.GetKey(KeyCode.W)) pos.y += movementAmount;
        if (Input.GetKey(KeyCode.S)) pos.y -= movementAmount;


        //do not move player if position would be out of bounds
        if (pos.x > leftAndRightEdge) pos.x = leftAndRightEdge;
        if (pos.x < -leftAndRightEdge) pos.x = -leftAndRightEdge;
        if (pos.y > upAndDownEdge) pos.y = upAndDownEdge;
        if (pos.y < -upAndDownEdge) pos.y = -upAndDownEdge;


        this.transform.position = pos;
    }

    void UpdateCD()
    {
        dashCD++;
        parryCD++;
        if (dashCD > abilityCD && dashReady == false)
        {
            //Display dash ready

            dashReady = true;
        }
        if (parryCD > abilityCD && parryReady == false)
        {
            //Display parry ready

            parryReady = true;
        }
    }

    void ActivateDash()
    {
        //reset values, increase speed
        dashCD = 0;
        dashReady = false;
        dashActive = true;
        movementAmount *= 3;

        Invoke("DisableDash", 0.2f);
    }

    void DisableDash()
    {
        dashActive = false;
        movementAmount /= 3;
    }

    void ActivateParry()
    {
        //reset values, activate parry
        parryCD = 0;
        parryReady = false;
        parryActive = true;

        //visually display parry

        Invoke("DisableParry", 0.5f);
    }

    void DisableParry()
    {
        parryActive = false;
        
        //reset parry display
    }

}

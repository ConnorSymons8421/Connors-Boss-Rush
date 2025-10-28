using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float leftAndRightEdge = 8.5f;
    public float upAndDownEdge = 4.6f;
    public float movementAmount = .0025f;
    public int attackDelay = 200;
    public int abilityCD = 3000;
    public GameObject dashTracker;
    public GameObject parryTracker;


    private int dashCD = 0;
    private int parryCD = 0;
    private int attackCD = 0;
    private bool dashReady = false;
    private bool parryReady = false;
    private bool attackReady = false;
    private bool parryActive = false;


    void Start()
    {
        dashTracker.SetActive(false);
        parryTracker.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //track dash, parry, and attack cooldowns
        UpdateCD();

        //activate dash/parry/attack
        if(Input.GetKey(KeyCode.J) && dashReady == true)
        {
            ActivateDash();
        }
        if (Input.GetKey(KeyCode.L) && parryReady == true)
        {
            ActivateParry();
        }
        if (Input.GetKey(KeyCode.K) && attackReady == true)
        {
            Attack();
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
        attackCD++;
        if (dashCD > abilityCD && dashReady == false) 
        { 
            dashReady = true;
            dashTracker.SetActive(true);
        }
        if (parryCD > abilityCD && parryReady == false) 
        { 
            parryReady = true;
            parryTracker.SetActive(true);
        }
        if (attackCD > attackDelay && attackReady == false)
        {
            attackReady = true;
        }
    }

    void Attack()
    {

    }

    void ActivateDash()
    {
        //reset values, increase speed
        dashCD = 0;
        dashReady = false;
        movementAmount *= 3;
        dashTracker.SetActive(false);
        

        Invoke("DisableDash", 0.35f);
    }

    void DisableDash()
    {
        movementAmount /= 3;
    }

    void ActivateParry()
    {
        //reset values, activate parry
        parryCD = 0;
        parryReady = false;
        parryActive = true;
        parryTracker.SetActive(false);

        Invoke("DisableParry", 0.5f);
    }

    void DisableParry()
    {
        parryActive = false;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //detect player being hit by enemy projectiles
        GameObject collideWith = coll.gameObject;
        if (collideWith.CompareTag("EnemyProj"))
        {
            Destroy(collideWith);
            //deal player damage if parry is currently inactive
            if (!parryActive)
            {
                BossFight bfScript = Camera.main.GetComponent<BossFight>();
                bfScript.PlayerHit();
            }
        }
        //reset any rotation caused by collisions
        this.transform.rotation = Quaternion.identity;
    }

}

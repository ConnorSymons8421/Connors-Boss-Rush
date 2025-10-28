using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bubble1Prefab;
    public GameObject bubble2Prefab;
    public GameObject bubble3Prefab;
    public GameObject bubble4Prefab;

    public GameObject critHitbox;
    public GameObject normalHitbox;
    public List<GameObject> hitboxList;

    public float leftEdge = 6f;
    public float rightEdge = 8f;
    public float upAndDownEdge = 3.2f;
    public int moveDelay = 1000;
    public int attackDelay = 5000;

    private int movementCD = 0;
    private int attackCD = 0;
    

    void Start()
    {
        //instantiate hitboxes
        GameObject crit = Instantiate<GameObject>(critHitbox);
        GameObject normal = Instantiate<GameObject>(normalHitbox);
        hitboxList = new List<GameObject>();
        hitboxList.Add(crit);
        hitboxList.Add(normal);
    }

    // Update is called once per frame
    void Update()
    {
        attackCD++;
        movementCD++;
        //move a random amount within range every moveDelay frames
        if (movementCD >= moveDelay)
        {
            float deltaX = (Random.value * 2) - 1;
            float deltaY = ((Random.value * 2) - 1)*2;

            Vector3 pos = this.transform.position;
            pos.x += deltaX;
            pos.y += deltaY;

            //keep enemy within specified coordinates
            if (pos.x > rightEdge) pos.x = rightEdge;
            if (pos.x < leftEdge) pos.x = leftEdge;
            if (pos.y > upAndDownEdge) pos.y = upAndDownEdge;
            if (pos.y < -upAndDownEdge) pos.y = -upAndDownEdge;

            //move enemy along with its hitboxes
            this.transform.position = pos;
            hitboxList[1].transform.position = pos;

            //move crit hitbox to correct position
            pos.y += 1.4f;
            hitboxList[0].transform.position = pos;

            //reset movement cooldown
            movementCD = 0;
        }

        //use a random attack every attackDelay frames
        if (attackCD >= attackDelay)
        {
            int attack = Random.Range(0, 3);
            switch (attack)
            {
                case 0:
                    Invoke("Attack1", 0f);
                    Invoke("Attack1", 0.3f);
                    Invoke("Attack1", 0.6f);
                    break;
                case 1:
                    Invoke("Attack2", 0f);
                    break; 
                case 2:
                    Invoke("Attack1", 0f);
                    Invoke("Attack3", 0f);
                    Invoke("Attack4", 0f);
                    break;

            }
            attackCD = 0;
        }
    }

    void Attack1()
    {
        GameObject bubble = Instantiate<GameObject>(bubble1Prefab);
        Vector3 pos = this.transform.position;
        //spawn attacks in front of enemy
        pos.x = pos.x - 1f;
        bubble.transform.position = pos;
    }

    void Attack2()
    {
        GameObject bubble = Instantiate<GameObject>(bubble2Prefab);
        Vector3 pos = this.transform.position;
        //spawn attacks in front of enemy
        pos.x = pos.x - 2f;
        bubble.transform.position = pos;
    }

    void Attack3()
    {
        GameObject bubble = Instantiate<GameObject>(bubble3Prefab);
        Vector3 pos = this.transform.position;
        //spawn attacks in front of enemy
        pos.x = pos.x - 1f;
        bubble.transform.position = pos;
    }

    void Attack4()
    {
        GameObject bubble = Instantiate<GameObject>(bubble4Prefab);
        Vector3 pos = this.transform.position;
        //spawn attacks in front of enemy
        pos.x = pos.x - 1f;
        bubble.transform.position = pos;
    }

    void BigAttack()
    {

    }
}

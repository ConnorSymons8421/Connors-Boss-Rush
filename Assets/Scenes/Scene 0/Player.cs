using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioClip miss;
    public AudioClip normalHit;
    public AudioClip criticalhit;
    public AudioClip dashSound;
    public AudioClip barrierSound;
    public AudioClip hurtSound;

    public float leftAndRightEdge = 8.5f;
    public float upAndDownEdge = 4.6f;
    public float movementAmount = .0025f;
    public int attackDelay = 200;
    public int abilityCD = 3000;
    public GameObject dashTracker;
    public GameObject parryTracker;
    public GameObject swordPrefab;
    public GameObject barrierPrefab;
    public GameObject critPrefab;
    public GameObject hitPrefab;
    public LayerMask hitboxes;


    private int dashCD = 0;
    private int parryCD = 0;
    private int attackCD = 0;
    private bool dashReady = false;
    private bool parryReady = false;
    private bool attackReady = false;
    private bool parryActive = false;
    private bool hurt = false;

    private RaycastHit2D hit;

    [SerializeField] Sprite[] playerSprites;
    private Sprite newSprite;

    void Start()
    {
        critPrefab.SetActive(false);
        hitPrefab.SetActive(false);
        swordPrefab.SetActive(false);
        barrierPrefab.SetActive(false);
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

        //move swordswing and barrier along with player
        pos.z -= 1;
        Vector3 barrierPos = pos;
        barrierPos.x -= 0.1f;
        barrierPos.y -= 0.1f;
        barrierPrefab.transform.position = barrierPos;

        pos.x += 2.5f;
        swordPrefab.transform.position = pos;
        pos.x += 3;
        pos.z -= .1f;
        critPrefab.transform.position = pos;
        hitPrefab.transform.position = pos;
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
        //play sword animation
        SwordSwing();
        attackCD = 0;
        attackReady = false;
        //enemy hitboxes are on layer 3. They are the only things to be detected by this raycast
        hit = Physics2D.Raycast(this.transform.position, Vector2.right, 5f, hitboxes);

        if (hit)
        {
            //get camera bossfight object
            BossFight bfScript = Camera.main.GetComponent<BossFight>();

            //check if hit was on the critical area
            if (hit.collider.name == "CriticalHitbox(Clone)")
            {
                critPrefab.SetActive(true);
                GetComponent<AudioSource>().PlayOneShot(criticalhit, 0.1f);
                bfScript.EnemyHit(3);
            }
            else if (hit.collider.name == "NormalHitbox(Clone)")
            {
                hitPrefab.SetActive(true);
                GetComponent<AudioSource>().PlayOneShot(normalHit, 0.1f);
                bfScript.EnemyHit(1);
            }
        }
        else
        {
            //play miss sound
            GetComponent<AudioSource>().PlayOneShot(miss, 0.1f);
        }
    }

    void SwordSwing()
    {
        swordPrefab.SetActive(true);
        Invoke("EndSwing", 0.5f);
    }

    void EndSwing()
    {
        swordPrefab.SetActive(false);
        critPrefab.SetActive(false);
        hitPrefab.SetActive(false);
    }

    void ActivateDash()
    {
        //reset values, increase speed
        dashCD = 0;
        dashReady = false;
        movementAmount *= 3;
        dashTracker.SetActive(false);
        GetComponent<AudioSource>().PlayOneShot(dashSound, 0.3f);


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
        barrierPrefab.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(barrierSound, 0.3f);

        Invoke("DisableParry", 0.5f);
    }

    void DisableParry()
    {
        parryActive = false;
        barrierPrefab.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //detect player being hit by enemy projectiles
        GameObject collideWith = coll.gameObject;
        if (collideWith.CompareTag("EnemyProj"))
        {
            Destroy(collideWith);
            //deal player damage if parry is currently inactive and has not been hurt recently
            if (!parryActive && !hurt)
            {
                Invoke("Hurt", 0f);
                BossFight bfScript = Camera.main.GetComponent<BossFight>();
                bfScript.PlayerHit();
            }
        }
        //reset any rotation caused by collisions
        this.transform.rotation = Quaternion.identity;
    }

    void Hurt()
    {
        hurt = true;
        GetComponent<AudioSource>().PlayOneShot(hurtSound, 0.3f);
        //update with hurt sprite, then revert to normal
        newSprite = playerSprites[1];
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        Invoke("Unhurt", 3f);
    }

    void Unhurt()
    {
        hurt = false;
        newSprite = playerSprites[0];
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

}

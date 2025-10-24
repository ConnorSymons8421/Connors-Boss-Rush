using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (Input.GetKey(KeyCode.A)) pos.x -= (float).005;
        if (Input.GetKey(KeyCode.D)) pos.x += (float).005;
        this.transform.position = pos;
    }
}

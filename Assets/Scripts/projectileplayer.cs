﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileplayer : MonoBehaviour
{
    public float speed = 50.0f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

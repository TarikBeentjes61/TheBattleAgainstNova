using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MoveTime;
    public GameObject Player;
    public int RoffSet;
    public GameObject Bullet;
    public GameObject ShootingPoint;
    public float ShootCooldown;
    private float _cooldownTimer;
    
    void Update() {
        var angle = (Player.transform.position - transform.position);    
        var calc = Mathf.Atan2 ( angle.y, angle.x );
        transform.rotation = Quaternion.Euler(0f, 0f, calc * Mathf.Rad2Deg - RoffSet);
        Move(calc);
        _cooldownTimer += Time.deltaTime;
        if (!(_cooldownTimer > ShootCooldown)) return;
        Shoot(calc);
        _cooldownTimer = 0;
    }

    private void Shoot(float calc) {
        
        GameObject clone = Bullet;
        Instantiate(clone, ShootingPoint.transform.position, Quaternion.Euler(0f, 0f, calc * Mathf.Rad2Deg - RoffSet));
        clone.transform.GetComponent<Rigidbody2D>().velocity = transform.right * MoveTime;
    }
    
    private void Move(float calc) {
        float singleStep = MoveTime * Time.deltaTime;
        transform.position = Vector2.Lerp(transform.position, Player.transform.position, singleStep);
        transform.rotation = Quaternion.Euler(0f, 0f, calc * Mathf.Rad2Deg - RoffSet);
    }
}

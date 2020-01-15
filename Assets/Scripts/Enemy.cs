using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class Enemy : MonoBehaviour
{
    [Header("Variables")]
    public float MoveTime;
    public int BurstSpread; //Hoe ver de kogels verspreiden
    public int BurstAmount; //Aantal kogels in een burst
    public float TimeBetweenMoves;
    public int RoffSet; //Rotatieoffset
    public float ShootCooldown; //Cooldown van het schieten
    
    [Header("GameObjects")]
    public GameObject Player;
    public GameObject Bullet;
    public GameObject ShootingPoint;
    private float _cooldownTimer;
    private float _moveTimer;

    private void Start() {
        _moveTimer = TimeBetweenMoves;
    }

    private void Update() {
        _moveTimer += Time.deltaTime;
        if (_moveTimer > TimeBetweenMoves) {
            _moveTimer = 0;
        }
        var angle = (Player.transform.position - transform.position);    
        var calc = Mathf.Atan2 ( angle.y, angle.x );
        transform.rotation = Quaternion.Euler(0f, 0f, calc * Mathf.Rad2Deg - RoffSet);
        _cooldownTimer += Time.deltaTime;
        if (_cooldownTimer > ShootCooldown) {
            StartCoroutine(Burst());
            _cooldownTimer = 0;
        }
    }

    private void Shoot(float calc) {
        GameObject clone = Bullet;
        Instantiate(clone, ShootingPoint.transform.position, Quaternion.Euler(0f, 0f, calc * Mathf.Rad2Deg - RoffSet));
        clone.transform.GetComponent<Rigidbody2D>().velocity = transform.right * MoveTime;
    }

    private IEnumerator Burst() {
        var angle = (Player.transform.position - transform.position);    
        var calc = Mathf.Atan2 ( angle.y, angle.x );
        for (int i = 0; i < BurstAmount; i++) {
            GameObject clone = Bullet;
            Random random = new Random();
            int randomNumber = random.Next(-BurstSpread, BurstSpread);
            Instantiate(clone, ShootingPoint.transform.position, Quaternion.Euler(0f, 0f, calc * Mathf.Rad2Deg - RoffSet + randomNumber));
            clone.transform.GetComponent<Rigidbody2D>().velocity = transform.right * MoveTime;
            yield return new WaitForEndOfFrame();
        }
    }
}

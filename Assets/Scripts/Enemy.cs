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
    public float DistanceToKeep;
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
    private bool _moveCompleted;

    private Vector3 _angle;
    private float _calc;

    private void Start() {
        _moveTimer = TimeBetweenMoves;
        _moveCompleted = true;    
    }

    private void Update() {
        _moveTimer += Time.deltaTime;
        if (_moveCompleted) {
            _moveCompleted = false;
            Random random = new Random();
            int randomNumber = random.Next(0, 1);
            if (randomNumber == 0) {
                if (Vector3.Distance(transform.position, Player.transform.position) > DistanceToKeep * 1.5f) {
                    StartCoroutine(Move());
                }
                else {
                    randomNumber = 1;
                }
            }
            if (randomNumber == 1) {
            }
            _moveTimer = 0;
        }
        _angle = Player.transform.position - transform.position;    
        _calc = Mathf.Atan2(_angle.y, _angle.x);
        transform.rotation = Quaternion.Euler(0f, 0f, _calc * Mathf.Rad2Deg - RoffSet);
        _cooldownTimer += Time.deltaTime;
        if (_cooldownTimer > ShootCooldown) {
            Shoot();
            _cooldownTimer = 0;
        }
    }

    private void Shoot() {
        GameObject clone = Bullet;
        Instantiate(clone, ShootingPoint.transform.position, Quaternion.Euler(0f, 0f, _calc * Mathf.Rad2Deg - RoffSet));
        clone.transform.GetComponent<Rigidbody2D>().velocity = transform.right * MoveTime;
        _moveCompleted = true;
    }

    private IEnumerator Move() {
        while (Vector3.Distance(transform.position, Player.transform.position) > DistanceToKeep) {
            transform.position =
                Vector3.MoveTowards(transform.position, Player.transform.position, MoveTime * Time.deltaTime);
            yield return null;
        }

        _moveCompleted = true;

        yield return 0;
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

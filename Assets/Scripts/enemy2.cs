using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2 : MonoBehaviour
{
    [Header("Variables")]
    public float MoveTime;
    public int BurstAmount; //Aantal kogels in een burst
    public float ShootCooldown; //Cooldown van het schieten
    
    [Header("GameObjects")]
    public GameObject Player;
    public GameObject Bullet;
    public GameObject ShootingPoint;
    private bool _moveCompleted;

    private void Start() {
        StartCoroutine(Burst());
    }

    private void Update() {
        transform.Rotate(0, 0, -4);
    }

    private IEnumerator Burst() {
        
        for (int i = 0; i < BurstAmount; i++) {
            GameObject bullet = Instantiate(Bullet, ShootingPoint.transform.position, ShootingPoint.transform.rotation);
            Rigidbody2D rb = bullet.transform.GetComponent<Rigidbody2D>();
            rb.AddForce(ShootingPoint.transform.up * 1);
            yield return new WaitForSeconds(ShootCooldown);
        }
    }
}


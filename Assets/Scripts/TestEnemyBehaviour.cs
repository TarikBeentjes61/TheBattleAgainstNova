using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyBehaviour : MonoBehaviour
{
    [Header("Variables")]
    public int SpecialBulletAmount; //Aantal kogels in een burst
    public float SpecialShootCooldown; //Cooldown van het speciaal schieten
    public float BulletSpeed;
    
    [Header("GameObjects")]
    public GameObject Bullet;
    public GameObject[] ShootingPoint;
    
    private int _timeTillSpecial;
    private float _time;
    private bool _doingSpecial;
    
    private void Update() {
        _time += Time.deltaTime;
        if (!_doingSpecial && _time > _timeTillSpecial) {
            StartCoroutine(Special());
        } 
        transform.Rotate(0,0,-4);
    }

    private IEnumerator Special() {
        _doingSpecial = true;
        
        for (int i = 0; i < SpecialBulletAmount; i++) {
            for (int j = 0; j < ShootingPoint.Length; j++) {
            GameObject bullet = Instantiate(Bullet, ShootingPoint[j].transform.position, ShootingPoint[j].transform.rotation);
            Rigidbody2D rb = bullet.transform.GetComponent<Rigidbody2D>();
            rb.AddForce(ShootingPoint[j].transform.up * BulletSpeed);
            }
            yield return new WaitForSeconds(SpecialShootCooldown);
        }

        _doingSpecial = false;
        _time = 0;
    }
}

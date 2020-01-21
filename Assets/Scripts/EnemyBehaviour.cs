using System.Collections;
using UnityEngine;
using Random = System.Random;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Variables")]
    public int SpecialBulletAmount; //Aantal kogels in een burst
    public float SpecialShootCooldown; //Cooldown van het speciaal schieten
    public float ShootCooldown; //Cooldown van het normaal schieten
    public float BulletSpeed;
    public float MoveSpeed;
    
    [Header("GameObjects")]
    public GameObject Player;
    public GameObject Bullet;
    public GameObject ShootingPoint;
    
    private int _timeTillSpecial;
    private float _time;
    private float _shootTimer;
    private bool _doingSpecial;
    
    private void Start() {
        _timeTillSpecial = new Random().Next(10, 15);
    }

    private void Update() {
        _time += Time.deltaTime;
        _shootTimer += Time.deltaTime;
        if (!_doingSpecial && _time > _timeTillSpecial) {
            StartCoroutine(Special());
        } else if (!_doingSpecial) {
            MakeMove();
            LookAt();
            if (_shootTimer > ShootCooldown) {
                Shoot();
            }
        } else {
            transform.Rotate(0,0,-4);
        }
    }

    private bool _avoidingCollision;
    
    private void MakeMove() {
        if (CheckNearby() && !_avoidingCollision) {
            _avoidingCollision = true;
            StartCoroutine(MoveAway(CheckNearby()));
        }
        else if (!_avoidingCollision) {
            Debug.Log("Moving To Player");
            MoveTowardsPlayer();  
        }
    }

    private Collider2D CheckNearby() {
        LayerMask mask = LayerMask.GetMask("Enemy");
        Collider2D col = new Collider2D();
        col = Physics2D.OverlapCircle(transform.position, 1f, mask);
        if (col.transform != transform) {
            return col;
        }
        return null;
    }

    private IEnumerator MoveAway(Collider2D col) {
        while (Vector3.Distance(transform.position, col.transform.position) < 2 && _avoidingCollision) {
            if (Vector3.Distance(transform.position, Player.transform.position) < 2.6f) {
                _avoidingCollision = true;
            }
            Debug.Log("Moving away");
            transform.position = Vector3.Lerp(transform.position, transform.position - col.transform.position, MoveSpeed * Time.deltaTime);
            yield return null;
        }
        _avoidingCollision = false;
    }

    
    private void MoveTowardsPlayer() {
        if (Vector3.Distance(transform.position, Player.transform.position) > 2.5f) {
            transform.position = Vector3.Lerp(transform.position, Player.transform.position, MoveSpeed * Time.deltaTime);
        }
    }

    private void LookAt() {
        Vector2 dir = Player.transform.position - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90), Time.deltaTime * 2);
    }

    

    private void Shoot() {
        GameObject bullet = Instantiate(Bullet, ShootingPoint.transform.position, ShootingPoint.transform.rotation);
        Rigidbody2D rb = bullet.transform.GetComponent<Rigidbody2D>();
        rb.AddForce(ShootingPoint.transform.up * BulletSpeed);        
        _shootTimer = 0;
    }
    
    private IEnumerator Special() {
        _doingSpecial = true;
        for (int i = 0; i < SpecialBulletAmount; i++) {
            GameObject bullet = Instantiate(Bullet, ShootingPoint.transform.position, ShootingPoint.transform.rotation);
            Rigidbody2D rb = bullet.transform.GetComponent<Rigidbody2D>();
            rb.AddForce(ShootingPoint.transform.up * BulletSpeed);
            yield return new WaitForSeconds(SpecialShootCooldown);
        }

        _doingSpecial = false;
        _time = 0;
    }
}


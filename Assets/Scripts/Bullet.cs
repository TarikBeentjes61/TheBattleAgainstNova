using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public Rigidbody2D rb;
    public int Damage;
    public GameObject Explosion;
    private GameObject _explosionToDestroy;
    public float Speed;
    
    void Start() {
        rb.velocity = transform.up * Speed;
    }

    private void Update() {
        Destroy(_explosionToDestroy, 1f);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (!other.transform.CompareTag("Enemy")    ) {
            _explosionToDestroy = Explosion;
            Instantiate(_explosionToDestroy, other.GetContact(0).point, Quaternion.identity);
            Destroy(gameObject);
        }
        if (other.transform.CompareTag("Player")) {
            //other.transform.GetComponent<ScriptWithHealth>.Health - Damage;
        }
    }
}

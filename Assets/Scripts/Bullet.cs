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
        if (other.transform.CompareTag("Enemy")) {
        }
        else {
            _explosionToDestroy = Explosion;

            Instantiate(_explosionToDestroy, other.GetContact(0).point, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.transform.CompareTag("Player")) {
            //Zoek naar het object Eventsystem en pak het script die de levens besturen, geef dan een min getal mee.
            GameObject.Find("EventSystem").GetComponent<GameControlScript>().ChangeLife(-1);
        }
    }
}

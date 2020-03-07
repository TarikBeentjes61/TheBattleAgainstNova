using UnityEngine;

public class Bullet : MonoBehaviour {
    public Rigidbody2D Rb;
    public int Damage;
    public GameObject Explosion;
    public float Speed;
    public string Shooter;
    public GameObject SpawnPowerUp;
    public Vector3 position;



    void Start() {

        Rb = GetComponent<Rigidbody2D>();
        Rb.velocity = transform.up * Speed;
        if (Shooter == "Player") {
            gameObject.layer = LayerMask.NameToLayer("PlayerBullets");
        }
        else {
            gameObject.layer = LayerMask.NameToLayer("EnemyBullets");
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.CompareTag("Enemy")) {
            if (Shooter == "Player") {
                other.gameObject.GetComponent<Health>().Damage(Damage);

            }
            int powerup = 1;

            ScoreScript.ScoreValue += 5;
            if (Random.Range(0, 50) == powerup)
            {
                position = new Vector3(Random.Range(100.0F, 1000.0F), 70, Random.Range(100.0F, 1000.0F));

                Instantiate(SpawnPowerUp, position, Quaternion.identity);
            }

        }

        if (other.transform.CompareTag("Player")) {
            if (Shooter == "Enemy") {
                //Zoek naar het object Eventsystem en pak het script die de levens besturen, geef dan een min getal mee.
                GameObject.Find("EventSystem").GetComponent<GameControlScript>().ChangeLife(-Damage);

            }
        }

        Explode();
    }

    private void Explode() {
        Debug.Log("Explode");
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }

}
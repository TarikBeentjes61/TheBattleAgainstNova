using UnityEngine;

public class Health : MonoBehaviour {
    public int HealthPoints;
    public GameObject Explosion;
    
    public void ChangeHealth(int damage) {
        HealthPoints -= damage;
        if (HealthPoints <= 0) {
            Explode();
        }
    }

    public void Explode() {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}

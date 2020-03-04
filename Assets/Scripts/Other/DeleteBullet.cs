using UnityEngine;

public class DeleteBullet: MonoBehaviour {
    //Verwijderd kogels als ze in het gebied komen, zorgt ervoor dat de kogels niet voor altijd blijven bestaan.
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            Destroy(other);
        }
    }
}
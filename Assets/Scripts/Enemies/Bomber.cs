using UnityEngine;

namespace Enemies {
    [RequireComponent(typeof(Health))]
    public class Bomber : MonoBehaviour
    {
        [Header("Variables")]
        public float MoveSpeed = 1f;
        public float ExplosionRange = 1f;
        public float DistanceToExplode = 0.7f;
        public int ExplosionDamage = 1;
    
        [Header("GameObjects")]
        public GameObject Player;
        public GameObject SelfExplosion;

        private void Start() {
            if (Player == null) {
                Player = GameObject.Find("Player");
            }
        }

        private void Update() {
            MoveTowardsPlayer();
            LookAt();
            if (Vector2.Distance(transform.position, Player.transform.position) < DistanceToExplode) {
                Explode();
            }            
        }

        private void MoveTowardsPlayer() {
            transform.position = Vector3.Lerp(transform.position, Player.transform.position, MoveSpeed * Time.deltaTime);
        }

        private void LookAt() {
            Vector2 dir = Player.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90), 1);
        }

        private void Explode() {
            var col = Physics2D.OverlapCircleAll(transform.position, ExplosionRange);
            foreach (var hit in col) {
                if (hit.CompareTag("Player")) {
                    GameObject.Find("EventSystem").GetComponent<GameControlScript>().ChangeLife(-1);               
                }

                if (hit.GetComponent<Health>()) {
                    if (hit.gameObject != gameObject) {
                        hit.GetComponent<Health>().Damage(1);
                    }
                }
            }

            Instantiate(SelfExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}

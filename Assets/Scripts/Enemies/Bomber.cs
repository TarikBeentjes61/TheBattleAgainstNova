using System.Collections;
using UnityEngine;

namespace Enemies {
    [RequireComponent(typeof(Health))]
    public class Bomber : MonoBehaviour
    {
        [Header("Variables")]
        public float MoveSpeed = 1;
    
        [Header("GameObjects")]
        public GameObject Player;
    
        private void Update() {
            MakeMove();
            LookAt();
        }

        private bool _avoidingCollision;
    
        private void MakeMove() {
            if (CheckNearby() && !_avoidingCollision) {
                _avoidingCollision = true;
                StartCoroutine(MoveAway(CheckNearby()));
            }
            if (CheckNearby() == null && !_avoidingCollision) {
                Debug.Log("Moving To Player");
                MoveTowardsPlayer();  
            }
            if (Vector3.Distance(transform.position, Player.transform.position)  < 0.7f) {
                GetComponent<Health>().Explode();
            }
        }

        private Collider2D CheckNearby() {
            LayerMask mask = LayerMask.GetMask("Enemy");
            var col = Physics2D.OverlapCircle(transform.position, 1f, mask);
            return col.transform != transform ? col : null;
        }

        private IEnumerator MoveAway(Collider2D col) {
            while (Vector3.Distance(transform.position, col.transform.position) < 1f && _avoidingCollision) {
                if (Vector3.Distance(transform.position, Player.transform.position) < 1f) {
                    _avoidingCollision = true;
                }
                Debug.Log("Moving away");
                transform.position = Vector3.Lerp(transform.position, transform.position - col.transform.position, MoveSpeed * Time.deltaTime);
                yield return null;
            }
            _avoidingCollision = false;
        }

        private void MoveTowardsPlayer() {
            transform.position = Vector3.Lerp(transform.position, Player.transform.position, MoveSpeed * Time.deltaTime);
        }

        private void LookAt() {
            Vector2 dir = Player.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90), 1);
        }

    }
}

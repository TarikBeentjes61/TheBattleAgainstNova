using System.Collections;
using UnityEngine;

namespace Enemies {
    [RequireComponent(typeof(Health))]
    public class BossEnemyBehaviour : MonoBehaviour
    {
        [Header("Variables")]
        public int SpecialBulletAmount = 5;
        public float SpecialShootCooldown = 0.25f; 
        public float BulletSpeed = 1f;
        public float Speed = 0.6f;
    
        [Header("GameObjects")]
        public GameObject Bullet;
        public GameObject[] ShootingPoint;
        public Transform BossPosition;
    
        private int _timeTillSpecial;
        private float _time;
        private bool _doingSpecial;

        private void Start() {
            if (BossPosition == null) {
                BossPosition = GameObject.Find("BossPosition").transform;
            }
        }
        //Ga eerst naar de BossPositie en begin dan de special method uit. 
        private void Update() {
            if (_moved) {
                _time += Time.deltaTime;
                if (!_doingSpecial && _time > _timeTillSpecial) {
                    StartCoroutine(Special());
                } 
                transform.Rotate(0,0,-2.5f); 
            }
            else {
                MoveToStartPosition();
            }
        }

        private bool _moved;
        private float _timer;
        //Beweegt 2 seconden lang naar de bosspositie zodat hij niet altijd op dezelfde plek stopt.
        private void MoveToStartPosition() {
            _timer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, BossPosition.position, Speed * Time.deltaTime);
            if (_timer > 3) {
                _moved = true;
            }
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
}

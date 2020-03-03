using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

namespace Enemies {
    [RequireComponent(typeof(Health))]
    public class Bomber : MonoBehaviour
    {
        [Header("Variables")]
        public float MoveSpeed = 1f;
        public float ExplosionRange = 1f;
        public float DistanceToExplode = 0.7f;
        public float DetonationSpeed = 2f;
        public int ExplosionDamage = 1;
    
        [Header("GameObjects")]
        public GameObject Player;
        public GameObject SelfExplosion;
        public GameObject DamagePool;

        private void Start() {
            _light2D = GetComponent<Light2D>();
            _originalIntensity = _light2D.intensity;
            if (Player == null) {
                Player = GameObject.Find("Player");
            }
        }

        private bool _flashing;
        private void Update() {
            if (!_flashing) {
                MoveTowardsPlayer();
                LookAt();
                if (Vector2.Distance(transform.position, Player.transform.position) < DistanceToExplode) {
                    StartCoroutine(Flash());
                }
            }
        }

        private void MoveTowardsPlayer() {
            transform.position = Vector3.Lerp(transform.position, Player.transform.position, MoveSpeed * Time.deltaTime);
        }

        private void LookAt() {
            Vector2 dir = Player.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90), 1);
        }

        private Light2D _light2D;
        private float _intensity;
        private float _originalIntensity;
        private bool _coroutineComplete;
    
        private IEnumerator Flash() {
            _flashing = true;
            _intensity = _originalIntensity;
            while (!_coroutineComplete) {
                    _intensity = Mathf.Lerp(_intensity, 3f, DetonationSpeed * Time.deltaTime);
                    _light2D.intensity = _intensity;
                    if (_intensity >= 2.5f) {
                        _coroutineComplete = true;
                        Explode();
                } 
                yield return null;
            }
            _coroutineComplete = false;

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
            Destroy(gameObject);
            Instantiate(SelfExplosion, transform.position, Quaternion.identity);        
            Instantiate(DamagePool, transform.position, Quaternion.identity);
            
        }

    }
}

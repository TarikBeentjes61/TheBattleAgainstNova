using UnityEngine;

namespace Enemies {
    [RequireComponent(typeof(Health))]
    public class Enemy2Behaviour : MonoBehaviour
    {
        [Header("Variables")]
        public float ShootCooldown = 2f; //Cooldown van het normaal schieten
        public float BulletSpeed = 1f;
        public float MoveSpeed = 0.5f;
        public float MoveTime = 2.2f;
        public float ScoreValue = 10;
    
        [Header("GameObjects")]
        public GameObject Player;
        public GameObject Bullet;
        public GameObject ShootingPoint;
    
        private float _shootTimer;
        private bool _avoidingCollision;

        private void Start() {
            if (Player == null) {
                Player = GameObject.FindWithTag("Player");
            }

            _originalShootCooldown = ShootCooldown;
        }

        
        private void Update() {
            if (!Player) {return;}
            _shootTimer += Time.deltaTime;
            if (!_movedTowards) {
                MoveTowardsPlayer();
            }
            else {
                RandomMovement();            
            }            LookAt();
            if (_shootTimer > ShootCooldown) {
                Shoot();
            }
        }

        //Random positie in een cirkel om het object heen.
        private bool _pointSet;
        private Vector3 _randomPosition;
        private void RandomMovement() {
            if (!_pointSet) {
                _randomPosition = Random.insideUnitCircle * 2f;
                _pointSet = true;
            }
            transform.position = Vector2.MoveTowards(transform.position, _randomPosition, MoveSpeed * Time.deltaTime * 2);
            if (transform.position == _randomPosition) {
                _pointSet = false;
            }
        }

        private bool _movedTowards;
        private float _moveTimer;
        //Eerste paar seconden beweeg naar de speler.
        private void MoveTowardsPlayer() {
            _moveTimer += Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, Player.transform.position,
                MoveSpeed * Time.deltaTime);
            if (_moveTimer > MoveTime) {
                _movedTowards = true;
                Shoot();
            }
        }

        //Simpel script om naar de speler te kijken.
        private void LookAt() {
            Vector2 dir = Player.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90), Time.deltaTime * 3);
        }

        private float _originalShootCooldown;

        private void Shoot() {
            if (_movedTowards) {
                GameObject bullet = Bullet;
                Rigidbody2D rb = bullet.transform.GetComponent<Rigidbody2D>();
                rb.GetComponent<CurvingBullet>().Shooter = tag;
                Instantiate(bullet, ShootingPoint.transform.position, ShootingPoint.transform.rotation);
                GameObject bullet2 = Bullet;
                Rigidbody2D rb2 = bullet.transform.GetComponent<Rigidbody2D>();
                rb2.GetComponent<CurvingBullet>().Shooter = tag;
                Instantiate(bullet2, ShootingPoint.transform.position, ShootingPoint.transform.rotation);
            }

            //Randomize de schiet tijden zodat alle enemies niet op hetzelfde moment schieten.
            ShootCooldown = _originalShootCooldown * new System.Random().Next(80, 140) / 100;
            _shootTimer = 0;
        }
    }
}


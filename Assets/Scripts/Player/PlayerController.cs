using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed;
    public GameObject bulletPrefab;
    public float shootCooldown = 0.3f;
    public Transform barreltip;
    public Transform player;
    
    private Rigidbody2D rb;
    private Vector2 moves;
    private Vector2 lookDirection;
    private float lookAngle;
    private Vector3 amount;
    private float shootTimer;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moves = moveInput.normalized * speed;
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);
        shootTimer += Time.deltaTime;
        if (Input.GetKeyDown("space")) {
            if (shootTimer > shootCooldown) {
                shootBullet();
            }

        }
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(0f,0f);
        rb.angularVelocity = 0f;
        transform.rotation = Quaternion.Euler(new Vector2(0f,0f));
        rb.MovePosition(rb.position + moves * Time.fixedDeltaTime);
    }

    public void shootBullet() {
        GameObject firedbullet = Instantiate(bulletPrefab, barreltip.position, barreltip.rotation);
        firedbullet.GetComponent<Bullet>().Shooter = "Player";
        firedbullet.GetComponent<Rigidbody2D>().velocity = barreltip.up * 10f;
        shootTimer = 0f;
    }
}

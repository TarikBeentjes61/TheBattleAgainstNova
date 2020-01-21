using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 moves;

    
    public GameObject bulletPrefab;
    public Transform player;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moves = moveInput.normalized * speed;


        if (Input.GetKeyDown("space"))
        {
            shootBullet();
        }




      
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moves * Time.fixedDeltaTime);


    }

    public void shootBullet()
    {
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.position = player.transform.position;
    }
}

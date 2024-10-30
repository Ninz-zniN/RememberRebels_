using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 15f;
    bool isGrounded = false;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
            Move();
        if (Input.GetButtonDown("Jump")&&isGrounded)
            Jump();
    }
    private void Move()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector2.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        spriteRenderer.flipX = dir.x < 0f;
    }
    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    private void CheckGround()
    {
        Vector3 vector = transform.position - transform.forward * spriteRenderer.size.y / 2;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(vector, 0.8f);
        isGrounded = colliders.Length > 1;
    }
}

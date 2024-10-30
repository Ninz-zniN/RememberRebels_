using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float patrolRadius = 200f;
    [SerializeField] private float viewRadius = 10f;
    [SerializeField] private float viewBackRadius = 3f;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    private float startX;
    private int dir=-1;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        startX = rb.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
        if ((rb.position.x <= startX) || (rb.position.x >= startX + patrolRadius))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            dir *= -1;
        }
        PlayerIsVisible();
        //Debug.Log(dir);
        //Debug.Log(PlayerIsVisible());
        transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.right * dir, speed * Time.deltaTime);
    }
    private bool PlayerIsVisible()
    {
        var tp = transform.position;
        float width = spriteRenderer.size.x;
        float height = spriteRenderer.size.y;
        //Debug.Log(dir);
        //var ray = Physics2D.OverlapBoxAll(new Vector2(tp.x - (width / 2 + viewBackRadius) * dir, tp.y - height / 2), new Vector2(tp.x + (width / 2 + viewRadius) * dir, tp.y + height / 2), LayerMask.NameToLayer("Hero"),-2,2);
        var ray = Physics2D.BoxCastAll(new Vector2(tp.x - (width / 2 + viewBackRadius) * dir, tp.y), new Vector2(viewBackRadius + viewRadius, height), 0f, Vector2.right*dir);
        //var ray = Physics2D.BoxCastAll(new Vector2(transform.position.x - (height / 2 + viewBackRadius) * dir, transform.position.y - width / 2), new Vector2(viewRadius * dir, height), 0f, transform.right * dir, 0, 3);
        Debug.Log(ray.Length);
        return ray.Length>1;
    }
    private void OnDrawGizmos()
    {
        var tp = transform.position;
        float width = 1f;
        float height = 1f;
        Vector3 vc = new Vector3(tp.x - (width / 2 + viewBackRadius) * dir, tp.y, 0) + new Vector3(tp.x - (width / 2 + viewBackRadius) * dir + viewBackRadius + viewRadius, tp.y, 0);
        vc = vc / 2;
        Gizmos.DrawCube(vc, new Vector3((viewBackRadius + viewRadius), height, 0));
    }
}

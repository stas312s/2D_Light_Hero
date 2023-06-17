using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonstr : Entity
{
    public float speed = 10f;
    public LayerMask obstacleLayer;
    private Vector3 direction;
    private SpriteRenderer sprite;
    private bool movingRight = true;
    public Vector3 size = Vector3.one;
    public float distance = 0.1f;

    public LayerMask enemy;
    // Start is called before the first frame update
    void Start()
    {
        
        Lives = 3;
        sprite = GetComponentInChildren<SpriteRenderer>();
        direction = transform.right;
    }

    // Update is called once per frame
    private void Update()
    {
       /* if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        // Виконуємо перевірку на зіткнення з колайдером
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movingRight ? Vector2.right : Vector2.left, 0.5f, obstacleLayer);
        if (hit.collider != null)
        {
            // Якщо зіткнення сталося, змінюємо напрямок руху
            movingRight = !movingRight;
        }
        */
       Move();
       
    }
    private void Move()
    {
        Vector2 raycastOrigin = transform.position + transform.right * direction.x;
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, direction, distance, ~enemy);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("TileMap"))
            {
               
                direction *= -1f;
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
               
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction , Time.deltaTime * speed);
        transform.localScale = new Vector3(direction.x > 0 ? size.x : -size.x, size.y, size.z);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player_Behaviour.Instance.gameObject)
        {
            Player_Behaviour.Instance.GetDamage();
            Lives --; 
            Debug.Log("Worm have" + Lives);
        }
    
        if (Lives < 1)
            Die();
    }
}

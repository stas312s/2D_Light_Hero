using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMonster : Entity
{
    [SerializeField]private float speed = 2f;
    public LayerMask player;
    public LayerMask enemy;
    private Vector3 direction;
    private SpriteRenderer sprite;
    public int startlive = 5;
    public Vector3 size = Vector3.one;
    public float distance = 0.1f; 
    void Start()
    {

        Lives = startlive;
        sprite = GetComponentInChildren<SpriteRenderer>();
        direction = transform.right;
    }
      private void Awake()
    {
        //sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
   /* private void Move()
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 1f + transform.right * direction.x * 2f, 0.1f);

    bool shouldFlip = false;
        foreach (Collider2D collider in colliders)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            shouldFlip = true;
            break;
        }
        else 
            shouldFlip = false;
            break;
    }
        if(colliders.Length > 0 && shouldFlip == false ) direction *= -1f;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction * speed, Time.deltaTime);
         transform.localScale = new Vector3(direction.x > 0 ? 1 : -1,1,1); 
    }
    */
   
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

       transform.position = Vector3.MoveTowards(transform.position, transform.position + direction * speed, Time.deltaTime);
       transform.localScale = new Vector3(direction.x > 0 ? size.x : -size.x, size.y, size.z);
   }
  /*  private void Move()
{
    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 1f + transform.right * direction.x * 2f, 0.1f);

    bool shouldFlip = false;

    foreach (Collider2D collider in colliders)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            shouldFlip = true;
            break;
        }
    }

    if (shouldFlip)
    {
        direction *= -1f;
        transform.localScale = new Vector3(direction.x > 0 ? 1 : -1, 1, 1);
    }
    

    transform.position = Vector3.MoveTowards(transform.position, transform.position + direction * speed, Time.deltaTime);
}
*/
   /* private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1f + transform.right * direction.x * 0.7f, 0.1f);
        if(colliders.Length >= 1) direction *= -1f;
       // transform.position = Vector3.MoveTowards(transform.position, transform.position + direction * speed, Time.deltaTime);
       Debug.Log(direction+" "+ speed);
        transform.Translate(direction * speed * Time.deltaTime);
    }
    */
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

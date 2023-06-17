using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Behaviour : Entity
{
    public float speed  = 5.0f; // скорость 
    public float maxSpeed = 4.0f;
   // public float maxJump = 1.0f;
    public float jumpForce = 5.0f; // сила прыжка
    public float pushForce = 50f; // Сила отталкивания
    
    //[SerializeField] private int health;
    [SerializeField] private Image[] hearts; //  общак хп
    [SerializeField] private Sprite aliveHeart; //фулхп
    [SerializeField] private Sprite deadHeart; // лоухп
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float fallMultiplier = 2.0f; //покашто хз, но вряд-ли
    private bool healthPotionCalled = false;

    public bool HealthPotionCalled
    {
        get { return healthPotionCalled; }
        set { healthPotionCalled = value; }
    }
    private bool IsGrounded = false;
    public bool isAttacking = false;
    public bool isRecharged = false;
    private float dir;
    public Transform attackPosition;
    public float attackRange;
    public bool IsDead = false;
    public LayerMask enemy;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    private static Player_Behaviour instance; // Синглтон

    // Получить экземпляр класса Player_Behaviour
    public static Player_Behaviour Instance
    {
        get { return instance; }
    }

    // Инициализация синглтона
    public void Awake()
    {
        Lives = 5;
       // health = Lives;
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        isRecharged = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    private  States State
    {
        get {return (States) animator.GetInteger("state");}
        set {animator?.SetInteger("state", (int)value);}
    }
    void FixedUpdate()
    {
        CheckGround();
    }
    void LateUpdate()   //  нормализацимя магнитуді
    {
        if(rb.velocity.magnitude >= maxSpeed)
        {
            rb.velocity = new Vector2 (rb.velocity.normalized.x * maxSpeed, rb.velocity.y);
        }
    }

    // Update is called once per frame
    void Update()
    {   
        for(int i = 0; i< hearts.Length; i++)
        {
            if(i<Lives)
                hearts[i].sprite = aliveHeart;
            else
                hearts[i].sprite = deadHeart;
            if(i<Lives)
                hearts[i].enabled = true;
            // else
            //   hearts[i].enabled = false;
        }
        if(IsDead) return;
        if(IsGrounded && !isAttacking) State = States.idle;
        if (!isAttacking && Input.GetButton("Horizontal")) Run();
        if (!isAttacking && IsGrounded && Input.GetButtonDown("Jump") && Time.timeScale != 0) Jump();
        if(!IsGrounded && rb.velocity.y >0 && isAttacking) State = States.jump;
        if(!IsGrounded && rb.velocity.y <0 && isAttacking) State = States.fall;
        if(Input.GetButtonDown("Fire1") && (IsGrounded || !IsGrounded)) Attack();
        //if(health != Lives) health = Lives;

      
        if( !IsDead && transform.position.y <-50)
        {
            IsDead = true;
            
            //Die();
            UIManager.Instance.Lose();
        }
    }
    private void Run()
 {
    if(IsGrounded) State = States.run;
     Vector3 direction = transform.right * Input.GetAxis("Horizontal");
    //transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);// фуфло через координаты
    //if(rb.velocity.magnitude < maxSpeed)
    rb.AddForce(direction * speed * Time.deltaTime, ForceMode2D.Impulse); // с импульсом идет лучше чем с форсом. форс фуфло. не так багает коллайдеры как трансформ позишн.
    sprite.flipX = direction.x < 0.0f;
    dir = direction.x;
 }

 private void Jump()
 {  
    rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
   // State = States.takeoff;
           /*  if (!IsGrounded && rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            } */
}
public void HealthPotion()
{
    Lives+= 1;
    healthPotionCalled = true;
}
 
private void CheckGround()
{
    Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.3f);
    bool wasGrounded = IsGrounded;
    IsGrounded = false;
    foreach (Collider2D collider in colliders)
    {
        if (collider.CompareTag("Ground"))
        {
            IsGrounded = true;
            break;
        }
    }
    if (IsGrounded && !wasGrounded) State = States.land;
    else if (!IsGrounded) State = States.jump;
}
    private void OnCollisionEnter2D(Collision2D collision)
    {
         Debug.Log(LayerMask.GetMask("Enemy"));
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("SUCTION");
            // Получаем направление отталкивания
           /*  Vector3 pushDirection = transform.position - collision.gameObject.transform.position;
            pushDirection.Normalize();

            // Применяем силу отталкивания к персонажу
            rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            */
        
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.right * 10f * (sprite.flipX ? 1f : -1f),  ForceMode2D.Impulse);
            //rb.AddForce(transform.up * 10f, ForceMode2D.Impulse);
        }
    }


    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);

    }
    public void Attack()
    {
        //if (IsGrounded && isRecharged) 
        if (isRecharged)
        {
            State = States.attack;
            isAttacking = true;
            isRecharged = false;

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCooldown());
        }
    }

    private void OnAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, enemy);
        for (int i = 0; i < colliders.Length; i++)
        {
           colliders[i].GetComponent<Entity>().GetDamage();
            StartCoroutine(EnemyOnAttack(colliders[i]));
        }

    }
 /*   void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.tag == "Ground")
    {
        Vector3 dir = collision.contacts[0].point - transform.position;
        dir = -dir.normalized;
        GetComponent<Rigidbody>().AddForce(dir * 100, ForceMode.Impulse);
    }
}
*/
 public enum States
 {
    idle,
    run,
    jump,
    fall,
    land,
    takeoff,
    attack,
    death
 }
   public override void GetDamage()
    {
        Lives--;
        Debug.Log("Player health: " + Lives);
        if (Lives <= 0 && !IsDead)
        {
            IsDead = true;
            Debug.Log("Player is dead.");
            State = States.death;
            if (Application.isPlaying) StartCoroutine(DieAnimation());
            
            // Действия при смерти игрока
        }
    }
     private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.25f);
        isAttacking = false;
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        isRecharged = true;
    }
    private IEnumerator EnemyOnAttack(Collider2D enemy)
    {
        SpriteRenderer enemyColor = enemy.GetComponentInChildren<SpriteRenderer>();
        enemyColor.color = new Color(1f, 0.4375f, 0.4375f);
        yield return new WaitForSeconds(0.2f);
        enemyColor.color = new Color(1,1,1);
    }
    private IEnumerator DieAnimation()
    {   yield return new WaitForSeconds(1.25f);
        Die();
        UIManager.Instance.Lose();
    }
    private IEnumerator MoveToTarget(Vector2 targetPosition)
    {
        float elapsedTime = 0f;
        float moveDuration = 0.5f; // Длительность движения

        Vector2 startPosition = transform.position;

        while (elapsedTime < moveDuration)
        {
            // Вычисляем текущую позицию с использованием линейной интерполяции
            float t = elapsedTime / moveDuration;
            transform.position = Vector2.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Устанавливаем точную позицию, чтобы избежать погрешности интерполяции
        transform.position = targetPosition;
    }
}

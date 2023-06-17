using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : Entity
{
   // private SpriteRenderer sprite;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    public override void GetDamage()
    {
        Lives--;
        Debug.Log("Monstre a have"+ Lives + " Lives");
        if(Lives < 1) 
        {
            if(StateSkeleton != StatesSkeleton.death)      StateSkeleton = StatesSkeleton.death;
            StartCoroutine(DieAnimation());
        }
    }
    private void Start()
    {
        Lives = 5;
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
       // sprite = GetComponentInChildren<SpriteRenderer>();
    }
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player_Behaviour.Instance.gameObject)
        {
            Player_Behaviour.Instance.GetDamage();
            Lives --; 
            Debug.Log("Worm have" + Lives);
        }
    
    if (Lives < 1)
        {
            if(StateSkeleton != StatesSkeleton.death)      StateSkeleton = StatesSkeleton.death;
            StartCoroutine(DieAnimation());
        }
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
     public  StatesSkeleton StateSkeleton
    {
        get {return (StatesSkeleton) animator.GetInteger("StateSkeleton");}
        set {animator.SetInteger("StateSkeleton", (int)value);}
    }
    public enum StatesSkeleton
    {
        idle,
        death
    }
     private IEnumerator DieAnimation()
    {   boxCollider2D.enabled = false;
        yield return new WaitForSeconds(1.15f);
         Die();
    }
}

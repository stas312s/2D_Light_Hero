using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    // Start is called before the first frame update
   private void OnCollisionEnter2D(Collision2D collision)
   {
    if (collision.gameObject == Player_Behaviour.Instance.gameObject)
    {
        Player_Behaviour.Instance.GetDamage();
    }
   }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

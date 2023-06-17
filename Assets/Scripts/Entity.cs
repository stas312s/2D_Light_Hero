using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]public int Lives { protected set;  get; }
    
    public virtual void GetDamage()
    {
        Lives--;    
        if(Lives < 1)
            Die();
        Debug.Log("Monstre a have"+ Lives + " Lives");
    }
    void Start()
    {
        
    }
    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
   
}

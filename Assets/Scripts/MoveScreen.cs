using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScreen : MonoBehaviour
{
    public Transform player; // тут объект игрока
    [SerializeField]private Vector2 offset;  

    void Start () 
    {        
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate () 
    {        
        if(player != null)
        {
        Vector3 temp = transform.position;
        temp.x = player.position.x + offset.x;
        temp.y = player.position.y + offset.y;
     
        transform.position = temp;
        }
    }
}

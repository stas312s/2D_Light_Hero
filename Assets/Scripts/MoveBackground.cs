using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    public float speed = 2f;
public float smoothTime = 0.1f;
public Vector2 offset = Vector2.zero;
private Transform player;
private Vector3 velocity = Vector3.zero;

void Start()
{
    player = GameObject.FindGameObjectWithTag("Player").transform;
}

void FixedUpdate()
{
    Vector3 temp = transform.position;
    temp.x = player.position.x + offset.x;
    temp.y = player.position.y + offset.y;
    Vector3 smoothPosition = Vector3.Lerp(transform.position, temp, speed * Time.fixedDeltaTime);
    transform.position = smoothPosition;
}
}

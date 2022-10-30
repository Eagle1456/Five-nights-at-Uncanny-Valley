using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    GameObject player;
    public float fov = .7f;
    public float speed = 1.0f;
    void Start()
    {
        player = GameObject.Find("Player"); 
    }

    void Update()
    {
        //move if player is looking away
        if (Vector3.Dot(player.transform.forward, transform.position) < 1 - fov)
        {
            transform.LookAt(player.transform);
            transform.Translate(-transform.forward * speed * Time.deltaTime); 
        }
    }
}

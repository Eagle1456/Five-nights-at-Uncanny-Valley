using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    GameObject player;
    public float fov = .7f;
    public float speed = 1.0f;
    float currentSpeed = 0;
    [SerializeField] float accelerationTime = 1;
    Rigidbody rb;

    void Start()
    {
        player = GameObject.Find("Player"); 
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(currentSpeed != 0)
        {
            rb.transform.Translate(-transform.forward * currentSpeed * Time.deltaTime);
            //Debug.Log(currentSpeed);
        }

        //move if player is looking away
        if (!(Vector3.Dot(player.transform.forward, transform.position - player.transform.position) > 1 - fov) && currentSpeed <= 0)
        {
            rb.transform.LookAt(player.transform);
            StopCoroutine("MovementDeccelerate");
            StartCoroutine("MovementAccelerate");
        }
        else if(Vector3.Dot(player.transform.forward, transform.position) > 1 - fov && currentSpeed >= speed)
        {
            StopCoroutine("MovementAccelerate");
            StartCoroutine("MovementDeccelerate");
        }
    }

    IEnumerator MovementAccelerate()
    {
        float t = 0f;
        while (t < 1.0f)
        {
            currentSpeed = Mathf.Lerp(0, speed, t);
            t += Time.deltaTime / accelerationTime;
            yield return null;
        }
        currentSpeed = speed;
        yield return null;
    }

    IEnumerator MovementDeccelerate()
    {
        float t = 0f;
        while (t < 1.0f)
        {
            currentSpeed = speed - Mathf.Lerp(0, speed, t);
            t += Time.deltaTime / accelerationTime;
            yield return null;
        }
        currentSpeed = 0;
        yield return null;
    }

    public void OnPlayerHit()
    {
        Destroy(gameObject);
    }
}

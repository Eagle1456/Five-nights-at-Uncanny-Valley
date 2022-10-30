using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 1.0f;

    void Awake()
    {
    }

    void Update()
    {
        Vector3 vec_in = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        vec_in = transform.TransformDirection(vec_in);
        transform.position += vec_in * playerSpeed * Time.deltaTime;


        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X"));
        GameObject.Find("Main Camera").transform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y"));
    }
}

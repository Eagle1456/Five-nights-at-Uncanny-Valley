using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 1.0f;
    public float mouseSensitivity = 2.0f;
    [SerializeField] float meleeRange = 1.0f;
    bool hasMelee;
    GameObject mainCam;
    Rigidbody rb;
    
    void Awake()
    {
        mainCam = GameObject.Find("Main Camera");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 vec_in = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        vec_in = rb.transform.TransformDirection(vec_in);
        rb.transform.position += vec_in * playerSpeed * Time.deltaTime;
         
        rb.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * mouseSensitivity);
        mainCam.transform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y") * mouseSensitivity);

        //melee attack. raycast for enemy, if hit, destroy weapon and enemy.
        Debug.DrawRay(mainCam.transform.position, mainCam.transform.forward * 5, Color.red);
        if (hasMelee && Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            Debug.Log("destroy??");
            RaycastHit hit;
            //Debug.DrawRay(mainCam.transform.position, mainCam.transform.forward);
            if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, meleeRange, LayerMask.GetMask("Enemies")))
            {
                Debug.Log("kill?");
                hit.collider.gameObject.GetComponent<EnemyBehavior>().OnPlayerHit();
                MeleeDestroy();
            }
        }
    }

    void MeleeEquip()
    {
        hasMelee = true;
    }

    void MeleeDestroy()
    {
        hasMelee = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Enemy":
                //guess i'll die
                break;

            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "Melee":
                MeleeEquip();
                Destroy(other.gameObject);
                break;

            default:
                break;
        }
    }
}

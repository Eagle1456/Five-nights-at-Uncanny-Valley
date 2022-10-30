using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTimer : MonoBehaviour
{
    [SerializeField] float activeDelay = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ActivateEnemy", activeDelay);
    }

    void ActivateEnemy()
    {
        GetComponent<EnemyBehavior>().enabled = true;
    }
}

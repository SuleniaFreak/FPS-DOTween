using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementObjectCoroutine : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform target;
    public float speed;

    void Start()
    {
        StartCoroutine("Move");
    }

    IEnumerator Move()
    {// mientras el objeto esté a una distancia mayor del target de 0.1f
        while(Vector3.Distance(transform.position, target.position) > 0.1f)
        {
            // muevo el objeto hacia el target
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            
            //no hagas tiempo de espera y continúa al frame siguiente
            yield return null;
        }
    }

    void Update()
    {
        
    }
}

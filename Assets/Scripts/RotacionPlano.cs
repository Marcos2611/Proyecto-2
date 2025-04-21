using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionPlano : MonoBehaviour
{
    public Transform player; // Aqui se añade la camara que utilizamos como si fuera el "player"

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Ignora la rotación en el eje Y para conseguir que solo gire de manera horizontal

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }

    void Start()
    {
        player= GameObject.Find("Main Camera").transform; 

    }
}
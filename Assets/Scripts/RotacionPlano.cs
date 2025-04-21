using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotacionPlano : MonoBehaviour
{
    public Transform player; // Aqui se añade la camara que utilizamos como si fuera el "player"

    void Awake()
    {
        PlaneTextureManager.UiCaller += DestroyNPC;
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;

  
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
           
            transform.rotation = Quaternion.Euler(90, lookRotation.eulerAngles.y, 0);
        }
    }

    void Start()
    {
        player= GameObject.Find("Main Camera").transform;
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    private void DestroyNPC()
    {
        Destroy(this.gameObject);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTextureManager : MonoBehaviour
{
    bool vertical;

    MeshRenderer mr;

    [field: Header("Pared1")]
    [field: SerializeField] public Material Pared1 { get; private set; }

    [field: Header("Suelo1")]
    [field: SerializeField] public Material Suelo1 { get; private set; }


    void Start()
    {
        mr = this.transform.GetComponent<MeshRenderer>();
        CheckNormals();
        DetermineTexture();
    }

    void CheckNormals()
    {
       if(transform.up.normalized == Vector3.up)
        {
            vertical = false;
        }
        else
        {
            vertical = true;
        }
    }

    void DetermineTexture()
    {
        if(vertical)
        {
            mr.material = Pared1;
        }
        else
        {
            mr.material = Suelo1;
        }
    }
}

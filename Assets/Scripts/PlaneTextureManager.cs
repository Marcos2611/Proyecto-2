using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTextureManager : MonoBehaviour
{
   

    [field: Header("Pared1")]
    [field: SerializeField] public Material Pared1 { get; private set; }

    [field: Header("Suelo1")]
    [field: SerializeField] public Material Suelo1 { get; private set; }

    [field: Header("Pared2")]
    [field: SerializeField] public Material Pared2 { get; private set; }

    [field: Header("Suelo2")]
    [field: SerializeField] public Material Suelo2 { get; private set; }

    [field: Header("Pared3")]
    [field: SerializeField] public Material Pared3 { get; private set; }

    [field: Header("Suelo3")]
    [field: SerializeField] public Material Suelo3 { get; private set; }

    [field: Header("Pared4")]
    [field: SerializeField] public Material Pared4 { get; private set; }

    [field: Header("Suelo4")]
    [field: SerializeField] public Material Suelo4 { get; private set; }

    [field: Header("Pared5")]
    [field: SerializeField] public Material Pared5 { get; private set; }

    [field: Header("Suelo5")]
    [field: SerializeField] public Material Suelo5 { get; private set; }

    [field: Header("Pared6")]
    [field: SerializeField] public Material Pared6 { get; private set; }

    [field: Header("Suelo6")]
    [field: SerializeField] public Material Suelo6 { get; private set; }

    [field: Header("Pared7")]
    [field: SerializeField] public Material Pared7 { get; private set; }

    [field: Header("Suelo7")]
    [field: SerializeField] public Material Suelo7 { get; private set; }

    private Material[] Pared;
    private Material[] Suelo;


    bool vertical;

    MeshRenderer mr;


    void Start()
    {
        Pared = new Material[] { Pared1, Pared2, Pared3, Pared4, Pared5, Pared6, Pared7 };
        Suelo = new Material[] { Suelo1, Suelo2, Suelo3, Suelo4, Suelo5, Suelo6, Suelo7 };

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

using System;
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

    public static event Action<int> OnTextureChange;

    public static int mundo { get; private set; } = 0;

    bool vertical;

    MeshRenderer mr;

    private int mundoPrevio = 0;

    bool canCollide = true;

    float collisionCD = 2f;

    void Awake()
    {
        int seed = System.Environment.TickCount;
        UnityEngine.Random.InitState(seed);
        OnTextureChange += DetermineTexture;
    }

    void OnDestroy()
    {
        OnTextureChange -= DetermineTexture;
    }


    void Start()
    {
        Pared = new Material[] { Pared1, Pared2, Pared3, Pared4, Pared5, Pared6, Pared7 };
        Suelo = new Material[] { Suelo1, Suelo2, Suelo3, Suelo4, Suelo5, Suelo6, Suelo7 };

        mr = this.transform.GetComponent<MeshRenderer>();
        CheckNormals();
        DetermineTexture(mundo);
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

    void DetermineTexture(int mundo)
    { 
        if(vertical)
        {
            mr.material = Pared[mundo];
        }
        else
        {
            mr.material = Suelo[mundo];
        }
    }


    void MundoRandom()
    {
        mundoPrevio = mundo;
        mundo = UnityEngine.Random.Range(0, 6);
        if (mundoPrevio == mundo)
        {
            MundoRandom();
        }
        else
        {
            OnTextureChange?.Invoke(mundo);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (canCollide == true)
        {
            MundoRandom();
        }

        canCollide = false;
        Invoke(nameof(ResetCollision), collisionCD);
      
    }

    private void ResetCollision()
    {
        canCollide = true;
    }

    // (HECHO) hay que hacer una funci�n que cuando sea un random() que de un numero del 0 al 6 para determinar que 
    //"mundo" est�s, osea que posicion del array de textura se va a usar y pasarlo a la funcion DetermineTexture
    // (HECHO) y tambien una funci�n que cuando toques la pared llame a la de randomizar textura.

    //Estas dos funciones a lo mejor va mejor ponerlas en un empty (gameObject) aparte y relacionarlas con este script, porque
    // si haces la funci�n de random() cada vez que inicias el plano se mezclaran los "mundos"

    // (HECHO) si no lo que tambi�n se puede hacer es iniciar en el mismo mundo cada que inicias la aplicaci�n y que el primer randomizado sea con
    // el toque a la pared. Esto ser� m�s f�cil de programar, ser� todo en este mismo script y tampoco es gran sacrificio.
    //Osea que a groso modo seria hacer una variable INT mundo o algo asi que guarde un numero del 0 al 6 y ya.
}

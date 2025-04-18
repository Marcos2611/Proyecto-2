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

    private int mundo = 0;

    void Start()
    {
        Pared = new Material[] { Pared1, Pared2, Pared3, Pared4, Pared5, Pared6, Pared7 };
        Suelo = new Material[] { Suelo1, Suelo2, Suelo3, Suelo4, Suelo5, Suelo6, Suelo7 };

        mr = this.transform.GetComponent<MeshRenderer>();
        CheckNormals();

        //el mundo inicia en 0
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

    void DetermineTexture()
    {
        if (index < 0 || index >= Pared.Length) return; //se usa pared.length para que no se pase del numero maximo y podamos agregar mas sin tener que preocuparnos por este numero
        //este if con el "index" se agrego por seguridad, creo que no pasaria nada si no esta pero por si acaso
        
        if(vertical)
        {
            mr.material = Pared1;
        }
        else
        {
            mr.material = Suelo1;
        }

        void MundoRandom()
        {
        // Genera nuevo mundo aleatorio
        mundo = Random.Range(0, 7);
        DetermineTexture(mundo);
        }

        void OnCollisionEnter(Collision collision)
        {
        if (collision.gameObject.CompareTag("Player"))
        {
            MundoRandom();
        }
        }
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

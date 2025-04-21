using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

    [field: Header("PrefabNPC")]
    [field: SerializeField] public GameObject HatMan { get; private set; }

    private GameObject EmptyRaycast;

    private GameObject npcInstanciado;

    private Material[] Pared;
    private Material[] Suelo;

    public static event Action<int> OnTextureChange;

    public static event Action UiCaller;

    public static int mundo { get; private set; } = 0;

    bool vertical;

    MeshRenderer mr;

    private int mundoPrevio = 0;

    bool canCollide = true;

    float collisionCD = 3f;

    float Contador = 0f;

    public static bool Instanciado = false;

    void Awake()
    {
        EmptyRaycast = GameObject.Find("Racas");
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
        StartCoroutine(InstantiateNPCRoutine());
    }

    private void Update()
    {


    }

    void CheckNormals()
    {
        if (transform.up.normalized == Vector3.up)
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
        if (vertical)
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
        mundo = UnityEngine.Random.Range(0, 7);
        if (mundoPrevio == mundo)
        {
            MundoRandom();
        }
        else
        {
            Contador = 0f;
            StartCoroutine(EsperarTransicion());

        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (canCollide == true)
        {
            if(collision.collider.CompareTag("MainCamera"))
            {
                MundoRandom();
                UiCaller?.Invoke();
            }
           
        }

        canCollide = false;
        Invoke(nameof(ResetCollision), collisionCD);

    }

    private void ResetCollision()
    {
        canCollide = true;
    }

    private IEnumerator EsperarTransicion()
    {
        while (Contador < 1.2f)
        {

            Contador += Time.deltaTime;
            yield return null;
        }
        OnTextureChange?.Invoke(mundo);
    }

    private IEnumerator InstantiateNPCRoutine()
    {
        while (true)
        {
            if (EmptyRaycast != null && !Instanciado)
            {
                RaycastHit hit;
                if (Physics.Raycast(EmptyRaycast.transform.position, Vector3.down, out hit))
                {
                    if (hit.collider.CompareTag("Plano"))
                    {
                        
                        Instanciado = true;
                        Instantiate(HatMan, EmptyRaycast.transform.position, Quaternion.identity);
                    }
                }
            }

            yield return new WaitForSeconds(0.6f);
        }
    }
}
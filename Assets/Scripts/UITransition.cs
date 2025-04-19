using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class UITransition : MonoBehaviour
{
    private Image imageComponent;
    UnityEngine.Color startColor = UnityEngine.Color.white;
    UnityEngine.Color endColor = new UnityEngine.Color(1f, 1f, 1f, 0f);
    float Contador = 0;
    float transitionDuration = 2f;

    void Awake()
    {
        PlaneTextureManager.UiCaller += Transicion;
    }

    void OnDestroy()
    {
        PlaneTextureManager.UiCaller -= Transicion;
    }

    void Start()
    {
        imageComponent = gameObject.GetComponent<Image>();
        imageComponent.color = endColor;
    }

    private void Transicion() 
    {
        StartCoroutine(FadeToWhite());      
    }

    private IEnumerator FadeToTransparent()
    {
        Contador = 0f;
        while (Contador < transitionDuration)
        {
            imageComponent.color = startColor;
            Contador += Time.deltaTime;
            float t = Mathf.Clamp01(Contador / transitionDuration);  
            imageComponent.color = UnityEngine.Color.Lerp(startColor, endColor, t); 
            yield return null; 
        }

        imageComponent.color = endColor;
    }
    private IEnumerator FadeToWhite()
    {
        Contador = 0f;
        while (Contador < 1f)
        {
            imageComponent.color = endColor;
            Contador += Time.deltaTime;
            float t = Mathf.Clamp01(Contador / 1f);
            imageComponent.color = UnityEngine.Color.Lerp(endColor, startColor, t);
            yield return null;
        }

        imageComponent.color = startColor;
       
        StartCoroutine(FadeToTransparent());
    }
}

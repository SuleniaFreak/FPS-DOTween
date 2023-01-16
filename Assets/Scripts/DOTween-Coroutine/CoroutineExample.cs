using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
    public GameObject[] pointLights;
    public float time;

    bool check;
    private void Start()
    {
       /* Debug.Log("Estoy estresada");
        yield return new WaitForSeconds(2);
        Debug.Log("Por que unity me trolea");*/

        //llama a una coroutine que tiene otra dentro
        StartCoroutine("CoroutineA");
    }
    
    //manera especial para poder llamar a la coroutine (StopCoroutine para pararla(stopall las para todas)
    //StartCoroutine("WaitSeconds");
    
    //las coroutine permiten meter tiempos de espera
   //declaración de la coroutine
   IEnumerator WaitSeconds()
    {
        Debug.Log("Pasa A");

        //tiempo de espera:
        yield return new WaitForSeconds(time);
        Debug.Log("Pasa B");
    }
    private void Update()
    { //si pulsamos el ratón, activamos la coroutine
        if (Input.GetMouseButtonDown(0) && check == false)
        {
            StartCoroutine("ActivateLightsWithDelay");
        }
    }
    IEnumerator ActivateLightsWithDelay()
    { //metemos un bucle for dentro de la corutina para recorrer la array e ir encendiendo las luces que están dentro
        check = true;
        if (pointLights[0].activeInHierarchy)
        {
            for(int i = 0; i < pointLights.Length; i++)
            {
                pointLights[i].SetActive(false);
                yield return new WaitForSeconds(1f);
            }
        }
        else
        {
            for (int i = 0; i < pointLights.Length; i++)
            {
                pointLights[i].SetActive(true);
                yield return new WaitForSeconds(1f);
            }
        }
        check = false;
        
    }

    IEnumerator CoroutineA()
    {
        Debug.Log("1");
        // para que A continue, esperará a que la llamada a la segunda coroutine termine
        StartCoroutine("CoroutineB");
        yield return new WaitForSeconds(1);
        Debug.Log("4");
    }
    IEnumerator CoroutineB()
    {
        Debug.Log("2");
        yield return new WaitForSeconds(1);
        Debug.Log("3");
    }

}

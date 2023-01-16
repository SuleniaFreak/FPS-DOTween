using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragObject : MonoBehaviour
{//para que el script funcione, tiene que estar en un gameobject que tenga un collider

    Vector3 offset;
    private void OnMouseDown()
    {
        //posici�n del rat�n en coordenadas de pantalla (posici�n por pixel)
        Vector2 mousePos = Input.mousePosition;
        
        //variable que guarda la distancia inicial que hay desde el gameObject a la camara
        float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, distance));
        // guarda la posicion del gameobject (cubo) y donde estamos pinchando en el mismo
        offset = transform.position - worldPos;
    }
    //m�todo que se usa para cuando pinchamos con el click izquierdo y arrastramos algo
    private void OnMouseDrag()
    {
        Vector2 mousePos = Input.mousePosition;
        float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        //cambiamos la posici�n del cubo y le decimos que se mueva hacia donde est� el rat�n
        //y le sumamos el offset calculado en el m�todo anterior
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, distance)) + offset;
    }

    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Debug.Log("el cubo est� a " + screenPos.x + " pixeles a la izq de la pantalla");
        Debug.Log("ScreenPos" + screenPos);//esto es la posicion de pixeles pasado a pantalla
    }
}

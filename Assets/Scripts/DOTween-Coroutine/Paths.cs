using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Paths : MonoBehaviour
{
    //se crean caminos para que el gameobject vaya de un punto especifico a otro
    public PathType pathType;

    public Vector3[] wayPoints = new[]
    {
        new Vector3(4,2,6),
        new Vector3(8, 6,14),
        new Vector3(4,6,14),
        new Vector3(0,6,6),
        new Vector3(-3,0,0)
    };
    void Start()
    {
        //setoptions cierra el path (une el primer punto con el último)
        //setlookat mira siempre hacia el punto siguiente al que se dirige
        //setease establece el tipo de movimiento
        //setloop el nº de veces que lo va a repetir (en este caso infinito por poner -1)
        transform.DOPath(wayPoints, 4, pathType).SetOptions(true).SetLookAt(0.001f).SetEase(Ease.Linear).SetLoops(-1);
    }

    void Update()
    {
        
    }
}

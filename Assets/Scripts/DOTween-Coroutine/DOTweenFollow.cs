using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DOTweenFollow : MonoBehaviour
{
    public Transform target;

    Vector3 targetLastPos;
    Tweener tween;
    void Start()
    { //setautokill en false desactiva que el tween se destruya
        tween = transform.DOMove(targetLastPos, 2).SetAutoKill(false);
        targetLastPos = target.position;
    }

    void Update()
    {
        //si el target no se ha movido, me salgo del update
        //no se ha movido porque la posicion del target en el frame anterior es igual a la actual
        if(targetLastPos == target.position)
        {
            return;
        }
        //cambia la posicion a la que tiene que moverse el gameObject y mantiene activo el tween 
        //reempezando el movimiendo recalculando la posicion del target
        tween.ChangeEndValue(target.position, true).Restart();
        //guarda la posición del target antes de salir del update
        targetLastPos = target.position;
    }
}

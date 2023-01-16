using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;//libreria para usar DOTween

public class DOTweenBasics : MonoBehaviour
{
    public Transform target;

    Tween t;
    void Start()
    {//el nº representa el tiempo que va a tardar en moverse
        //la línea solo necesita leerse una vez
       t = transform.DOMove(target.position, 5).SetEase(Ease.InBounce).OnComplete(DoSomething);
         
    //modifica la escala del objeto
       transform.DOScale(new Vector3(3, 3, 3), 5).SetEase(Ease.InBounce);
        //cambiar el color del objeto
        //se le pueden poner loops, el -1 indica loops infinitos y el tipo como va a funcionar (3 tipos)
        GetComponent<Renderer>().material.DOColor(Color.yellow, 5).SetLoops(-1, LoopType.Yoyo);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            t.Pause();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            t.Play();
        }
    }


    void DoSomething()
    {
        Debug.Log("Ellieeee que el domingo sale the last of us!!!!");
    }
}

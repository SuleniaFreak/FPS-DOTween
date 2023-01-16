using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Gun Settings")]
    public float damage;
    public float range;
    public float impactForce;
    public float timeBetweenBullets; //cadencia del arma
    public ParticleSystem muzzleEffects;

    Camera cam;
    AudioSource audioS;
    Ray ray;
    RaycastHit hit;
    float timer;

    private void Awake()
    {
        cam = Camera.main;
        audioS = GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        SelectTarget();
        timer += Time.deltaTime;
        if(Input.GetMouseButtonDown(0) && timer >= timeBetweenBullets)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        timer = 0;
        muzzleEffects.Play();
        audioS.Play();

        ray.origin = cam.transform.position;
        ray.direction = cam.transform.forward;

        if(Physics.Raycast(ray, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            if(target != null)
            {
               
                target.TakeDamage(damage);
                target.hitEffect.transform.position = hit.point;
                target.hitEffect.Play();

                if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
            }
        }
    }

    void SelectTarget()
    {
        ray.origin = cam.transform.position;
        ray.direction = cam.transform.forward;

        if (Physics.Raycast(ray, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    target.arrow.SetActive(!target.arrow.activeSelf);
                }
            }
        }
    }
}

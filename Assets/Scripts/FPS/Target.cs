using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public ParticleSystem hitEffect;
    public Color colorTarget;
    public Image healthImage;
    public GameObject arrow;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthImage.fillAmount = currentHealth / maxHealth;
        if(currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}

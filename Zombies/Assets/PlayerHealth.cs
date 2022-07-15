using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth singleton;
    public static float currentHealth; 
    public float maxHealth = 100f; 
    public bool isDead = false; 


    [Header ("Damage Screen")]
    public Color damageColor;
    public Image damageImage;
    float colorSmoothing = 6f;
    bool isTakingDamages = false;

    private void Awake()
    {
        singleton = this;
    }


    // Start is called before the first frame update
    void Start()
    {   
        
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < 0) 
        {
            currentHealth = 0;
            
        }

        if(isTakingDamages)
        {
            damageImage.color = damageColor;
        } else 
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, colorSmoothing * Time.deltaTime);
        }
        if (isDead == true)
        {
            isTakingDamages = true;
        }
        else {
            isTakingDamages = false;
        }
    }

    public void DamagePlayer(float damage)
    {
        if(currentHealth > 0)
        {
            currentHealth -= damage;
            isTakingDamages = true;
        } else 
        {
            Dead();
        }
    }

    void Dead()
    {
        currentHealth = 0; 
        isDead = true; 
        isTakingDamages = true;
    }
}

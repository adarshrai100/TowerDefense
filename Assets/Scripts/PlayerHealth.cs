using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    private float health;
    private float lerpTimer;
    [Header("Health Bar")]
    public float maxHealth = 100f;
    public float chipspeed = 2f;
    public Image frontHealthbar;
    public Image backHealthbar;

    [Header("Damage Overlay")]
    public Image overlay; // our damage overlay gameobject
    public float duration; // how long the image stays fully opaque
    public float fadeSpeed; // how quickly the imgage fades
    private float durationTimer; // timer to check against duration

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        if(overlay.color.a > 0)
        {
            if (health < 30)
                return;
            durationTimer += Time.deltaTime;
            if(durationTimer>duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

    public void UpdateHealthUI()
    {
       // Debug.Log(health);
        float fillF = frontHealthbar.fillAmount;
        float fillB = backHealthbar.fillAmount;
        float hFraction = health / maxHealth;
        if(fillB>hFraction)
        {
            frontHealthbar.fillAmount = hFraction;
            backHealthbar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipspeed;
            percentComplete = percentComplete * percentComplete;
            backHealthbar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if(fillF<hFraction)
        {
            backHealthbar.color = Color.green;
            backHealthbar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipspeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthbar.fillAmount = Mathf.Lerp(fillF, backHealthbar.fillAmount, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0f;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }
     
    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth;

    public float damageInvicLength = 1f;

    private float invicCount;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(invicCount > 0)
        {
            invicCount -= Time.deltaTime;

            if(invicCount <= 0)
            {
                PlayerController.instance.bodySprite.color = new Color
                (
                    PlayerController.instance.bodySprite.color.r,
                    PlayerController.instance.bodySprite.color.g,
                    PlayerController.instance.bodySprite.color.b,
                    1f
                );
            }
        }
    }

    public void DamagePlayer()
    {
        if (invicCount <= 0)
        {
            currentHealth--;
            invicCount = damageInvicLength;

            PlayerController.instance.bodySprite.color = new Color
                (
                    PlayerController.instance.bodySprite.color.r,
                    PlayerController.instance.bodySprite.color.g,
                    PlayerController.instance.bodySprite.color.b,
                    .5f
                );

            if (currentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.DeathScreen.SetActive(true);
            }


            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();

        }
    }

    public void MakeInvicible(float length)
    {
        invicCount = length;
        PlayerController.instance.bodySprite.color = new Color
                (
                    PlayerController.instance.bodySprite.color.r,
                    PlayerController.instance.bodySprite.color.g,
                    PlayerController.instance.bodySprite.color.b,
                    .5f
                );
    }
}

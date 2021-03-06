using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public GameObject buyMessage;
    private bool inBuyZone;

    public bool isHealthRestore;
    public bool isHealthUpgrade;
    public int healthUpgradeAmount;
    public bool isWeapon;

    public int ItemCost;

    public Gun[] potentialGuns;
    public Gun theGun;
    public SpriteRenderer gunSprite;
    public Text infoText;

    // Start is called before the first frame update
    void Start()
    {
        if (isWeapon)
        {
            int selectedGun = Random.Range(0, potentialGuns.Length);
            theGun = potentialGuns[selectedGun];
            gunSprite.sprite = theGun.gunShopSprite;
            ItemCost = theGun.itemCost;
            infoText.text = "Buy " + theGun.weaponName + "\n" + ItemCost + " Gold";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inBuyZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(LevelManager.instance.currentCoins >= ItemCost)
                {
                    LevelManager.instance.SpendCoins(ItemCost);
                    if (isHealthRestore)
                    {
                        PlayerHealthController.instance.HealPlayer(PlayerHealthController.instance.maxHealth);
                    }

                    if (isHealthUpgrade)
                    {
                        PlayerHealthController.instance.IncreaseMaxHealth(healthUpgradeAmount);
                    }

                    if (isWeapon)
                    {
                        Gun gunClone = Instantiate(theGun);

                        gunClone.transform.parent = PlayerController.instance.gunfire;
                        gunClone.transform.position = PlayerController.instance.gunfire.position;
                        gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        gunClone.transform.localScale = Vector3.one;

                        PlayerController.instance.availableGuns.Add(gunClone);
                        PlayerController.instance.currentGun = PlayerController.instance.availableGuns.Count - 1;
                        PlayerController.instance.SwitchGun();
                    }

                    gameObject.SetActive(false);
                    inBuyZone = false;
                    AudioManager.instance.PlaySFX(18);
                }
                else
                {
                    AudioManager.instance.PlaySFX(19);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            buyMessage.SetActive(true);
            inBuyZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            buyMessage.SetActive(false);
            inBuyZone = false;
        }
    }
}

using System.Collections;
using Obvious.Soap;
using UnityEngine;
using TMPro;

public class GunShop : MonoBehaviour
{
    public TextMeshProUGUI GunMoneyText;
    public TextMeshProUGUI KeyText;
    public IntVariable moneyCount;
    public int Weapon_Price;
    public GameObject Lock;
    public GameObject Unlock;
    public GameObject Weaponobject;
    public Transform SpawnWeaponS;
    public BoxCollider BX;

    private bool canBuy = true; // To handle cooldown

    private void Start()
    {
        KeyText.enabled = false;
        Lock.SetActive(false);
        Unlock.SetActive(false);
    }

    private void Update()
    {
        GunMoneyText.text = Weapon_Price.ToString() + " $";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            KeyText.enabled = true;

            if (moneyCount.Value >= Weapon_Price)
            {
                Lock.SetActive(false);
                Unlock.SetActive(true);

                if (Input.GetKey(KeyCode.E) && canBuy)
                {
                    moneyCount.Value -= Weapon_Price;
                    Instantiate(Weaponobject, SpawnWeaponS.position, SpawnWeaponS.rotation);
                    StartCoroutine(Cooldown());
                }
            }
            else
            {
                Lock.SetActive(true);
                Unlock.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            KeyText.enabled = false;
        }
    }

    IEnumerator Cooldown()
    {
        canBuy = false;
        yield return new WaitForSeconds(0.5f); // Cooldown time
        canBuy = true;
    }
}
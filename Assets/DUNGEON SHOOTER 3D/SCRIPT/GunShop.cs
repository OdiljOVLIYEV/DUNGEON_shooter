using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using TMPro;

[System.Serializable]
public class ShopItem
{
    public string itemID; // Unique ID for each item
    public string itemName;
    public int price;
    public GameObject itemPrefab;
    public Transform spawnPoint;
    public bool canBuyMultiple;
}

public class GunShop : MonoBehaviour
{
    public TextMeshProUGUI GunMoneyText;
    public TextMeshProUGUI KeyText;
    public IntVariable moneyCount;
    public GameObject Lock;
    public GameObject Unlock;
    public List<ShopItem> shopItems;
    private bool canBuy = true; // To handle cooldown
    private Dictionary<string, bool> purchasedItems;
    private ShopItem selectedItem;
    private PlayerData playerData; // Player data reference

    private void Start()
    {
        KeyText.enabled = false;
        Lock.SetActive(false);
        Unlock.SetActive(false);

        playerData = SaveManager.instance.LoadPlayerData(); // Load the player data

        purchasedItems = new Dictionary<string, bool>();

        foreach (var item in shopItems)
        {
            purchasedItems[item.itemName] = playerData.purchasedWeaponIDs.Contains(item.itemName);
        }

        // Set the first item as the default selected item
        if (shopItems.Count > 0)
        {
            selectedItem = shopItems[0];
            GunMoneyText.text = selectedItem.price.ToString() + " $";
        }
    }

    private void Update()
    {
        if (selectedItem != null)
        {
            GunMoneyText.text = selectedItem.price.ToString() + " $";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            KeyText.enabled = true;

            if (selectedItem != null && moneyCount.Value >= selectedItem.price)
            {
                Lock.SetActive(false);
                Unlock.SetActive(true);

                if (Input.GetKey(KeyCode.E) && canBuy)
                {
                    if (!purchasedItems[selectedItem.itemName] || selectedItem.canBuyMultiple)
                    {
                        moneyCount.Value -= selectedItem.price;
                        Instantiate(selectedItem.itemPrefab, selectedItem.spawnPoint.position, selectedItem.spawnPoint.rotation);

                        if (!selectedItem.canBuyMultiple)
                        {
                            purchasedItems[selectedItem.itemName] = true;
                            playerData.purchasedWeaponIDs.Add(selectedItem.itemName); // Sotib olingan qurol ID sini qo'shish
                            SaveManager.instance.SavePlayerData(playerData); // Save after purchase
                        }

                        
                        StartCoroutine(Cooldown());
                    }
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
            StartCoroutine(UIoff());
        }
    }

    IEnumerator Cooldown()
    {
        canBuy = false;
        yield return new WaitForSeconds(0.5f); // Cooldown time
        canBuy = true;
    }

    IEnumerator UIoff()
    {
        yield return new WaitForSeconds(1.5f); // Cooldown time
        Lock.SetActive(false);
        Unlock.SetActive(false);
    }

    public void SelectItem(int index)
    {
        if (index >= 0 && index < shopItems.Count)
        {
            selectedItem = shopItems[index];
            GunMoneyText.text = selectedItem.price.ToString() + " $";
        }
    }


    
}


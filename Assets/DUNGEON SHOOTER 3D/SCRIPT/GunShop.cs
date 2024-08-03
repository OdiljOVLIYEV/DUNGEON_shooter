using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using TMPro;

[System.Serializable]
public class ShopItem
{
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

    private void Start()
    {
        KeyText.enabled = false;
        Lock.SetActive(false);
        Unlock.SetActive(false);
        purchasedItems = new Dictionary<string, bool>();

        foreach (var item in shopItems)
        {
            purchasedItems[item.itemName] = false;
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
        // Optional: If you want to update the UI continuously, otherwise it can be moved inside OnTriggerStay
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

    // Optionally, add a method to select different items, e.g., via UI buttons
    public void SelectItem(int index)
    {
        if (index >= 0 && index < shopItems.Count)
        {
            selectedItem = shopItems[index];
            GunMoneyText.text = selectedItem.price.ToString() + " $";
        }
    }
}

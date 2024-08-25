using System;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    public Color hoverColor = Color.yellow; // Hover holatida rang
    public Color originalColor= Color.blue; // Asl rang
    [SerializeField] private BoolVariable WeaponUI_Open;

    private void Start()
    {
        button = GetComponent<Button>();
        //button.interactable = false;
       // button=GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("Button is not assigned.");
            return;
        }

        // Butonning original rangini saqlash
        originalColor = button.image.color;

        // Buton uchun sizning rang o'zgartirish funksiyalarini qo'shish
       
    }
    private void Update()
    {
     
            if (WeaponUI_Open == null)
            {
                Debug.LogError("WeaponUI_Open is not assigned.");
                return;
            }

            if (WeaponUI_Open.Value==false)
            {
                if (button != null)
                    button.interactable = false;
            }
            else
            {
                if (button != null)
                    button.interactable = true;
            }
            // Check the state of WeaponUI_Open to update button interactability
           
               
        
                // If button is not interactable, reset its color to the original
                if (!button.interactable==false)
                {
                    button.image.color = originalColor;
                }
            
        

    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button != null && button.interactable)
        {
            button.image.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (button != null && button.interactable)
        {
            button.image.color = originalColor;
        }
    }

    
   
}
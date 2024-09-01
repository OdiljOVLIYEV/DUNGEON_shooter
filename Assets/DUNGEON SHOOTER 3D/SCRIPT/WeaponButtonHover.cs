using System;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource audioSource;
    private Button button;
    public bool soundEnabled;
    [SerializeField] private BoolVariable WeaponUI_Open;
    void Start()
    {
        button = GetComponent<Button>();
        
        // Tugma ustiga tıklanadigan ovozni qo'shish
        if (soundEnabled && audioSource != null)
        {
            if(button!=null)
            button.onClick.AddListener(PlaySound);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Kursor tugmaga tekkanda ovoz chiqarish
        if (soundEnabled && audioSource != null)
        {
            PlaySound();
            
        }
        else
        {
            if (!WeaponUI_Open.Value)
            {
                button.onClick.Invoke();
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (soundEnabled==true && audioSource != null)
        {
           
        }
        else
        {
            if (WeaponUI_Open == false)
            {
                button.onClick.Invoke();
            }
        }
    }
    
    void PlaySound()
    {
        // Agar ovoz chiqarish yoqilgan bo'lsa, ovoz chiqishini amalga oshirish
        
            audioSource.Play();
        
    }
}
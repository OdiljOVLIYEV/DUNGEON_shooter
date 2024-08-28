using System;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    [SerializeField] private BoolVariable WeaponUI_Open;
    void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (WeaponUI_Open == false)
        {
            button.onClick.Invoke();
        }
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (WeaponUI_Open == false)
        {
            button.onClick.Invoke();
        }
    }
    
   
}
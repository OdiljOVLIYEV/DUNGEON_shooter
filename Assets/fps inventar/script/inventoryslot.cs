using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class inventoryslot : MonoBehaviour,IDropHandler
{

    private inventor inventar;
    public int number;
    
    private void Start()
    {

        inventar = GameObject.FindGameObjectWithTag("Player").GetComponent<inventor>();

    }

    private void Update()
    {
        if (transform.childCount == 0)
        {
            Debug.Log("0 qiymat");
            

                 inventar.isfull[number] = false;

             
        }
            if (transform.childCount == 1)
            {
                Debug.Log("1 qiymat");
                

                    inventar.isfull[number] = true;

                
            }

       

    }
    public void OnDrop(PointerEventData eventData)
    { 

        if (transform.childCount == 0)
        {


            GameObject dropped = eventData.pointerDrag;
            drag Drag = dropped.GetComponent<drag>();
            Drag.parentAfterDrag = transform;
           
           
        }
        

    }
   
}

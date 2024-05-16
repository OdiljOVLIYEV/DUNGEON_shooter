using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class drag : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler

{
    public Image image;

    [HideInInspector]
    public Transform parentAfterDrag;
    public bool qoyvordi;


    public GameObject btn1;
    public GameObject btn2;
   


    private void Start()
    {
       
    }
    public void Update()
    {
       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        PlayerHealt player = FindObjectOfType<PlayerHealt>();
        player.alarmOff();

        btn1.SetActive(false);
        btn2.SetActive(false);

        Debug.Log("Begin Dray");
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;

        
    }

    public void OnDrag(PointerEventData eventData)
    {


        btn1.SetActive(false);
        btn2.SetActive(false);

        transform.position = Input.mousePosition;
        Debug.Log("HANDLER Dray");
       
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {


        btn1.SetActive(true);
        btn2.SetActive(true);


        Debug.Log("END Dray");
        
            transform.SetParent(parentAfterDrag);


            image.raycastTarget = true;
        
    }

    public void Healtadd()
    {
        /* PlayerHealt player = FindObjectOfType<PlayerHealt>();
         player.Healt += 50f;*/

        PlayerHealt player = FindObjectOfType<PlayerHealt>();

        Debug.Log(player.Healt);
        if (player.Healt == 100)
        {
            player.alarmOn();
            
            
        }else
        if (player.Healt <= 50)
        {
            Debug.Log("jon kam");
            player.Healt += 50f;
            Destroy(gameObject);
        }
        else if (player.Healt > 50)
        {
            Debug.Log("jon sal ko'proq");
            player.Healt = 100f;
            Destroy(gameObject);
        }
       



    }


   





}
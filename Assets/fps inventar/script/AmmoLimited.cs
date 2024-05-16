using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoLimited : MonoBehaviour
{

    public Text text;
 
   
   // private inventor inventar;
    //public GameObject itembutton;
   
    // Start is called before the first frame update
    void Start()
    {
       // inventar = GameObject.FindGameObjectWithTag("Player").GetComponent<inventor>();

    }

    // Update is called once per frame
    void Update()
    {
        magazinpistolet Magazinpistolet = FindObjectOfType<magazinpistolet>();
       // if (Magazinpistolet != null)
          
      text.text=Magazinpistolet.Magazinammo.ToString(); ;




        if (Magazinpistolet.Magazinammo <= 0)
        {
            ammopickup ammo = FindObjectOfType<ammopickup>();
            ammo.ammoqutibor = false;
            Destroy(gameObject);
        }   


      


    }
}

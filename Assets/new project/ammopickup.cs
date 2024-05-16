using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class ammopickup : MonoBehaviour

{

    //PISTOLET QIYMAT
    private magazinpistolet picAmmo;
    public int pictolet_ammo;
    public bool ammoqutibor = false;
    public GameObject pistoletprefab;
    //SHOOTGUN QIYMAT
    private magazinshotgun shooter;

    //AVTOMAT QIYMAT

    //HEALT_BAG QIYMAT
    public float Healtbag=50;
    public GameObject healtprefab;

	public float range =4f;
	public Camera cam;
    public GameObject tmp;
    private inventor inventar;
	
    

    // Start is called before the first frame update
    void Start()
	    {

        shooter = FindObjectOfType<magazinshotgun>();
		picAmmo = FindObjectOfType<magazinpistolet>();
        inventar = GameObject.FindGameObjectWithTag("Player").GetComponent<inventor>();



    }

    // Update is called once per frame
    void Update()
	{

 
        RaycastHit hit;
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
		{



			if (hit.transform.tag == "pistolet_ammo")
			{

				tmp.SetActive(true);


				if (Input.GetKeyDown(KeyCode.E))
				{
                    if(ammoqutibor==false)
                       
                        {

                            for (int i = 0; i < inventar.slots.Length; i++)
                            {

                                if (inventar.isfull[i] == false)
                                {
                                  picAmmo.Magazinammo += pictolet_ammo;
                                  Destroy(hit.transform.gameObject);
                                  ammoqutibor = true;
                                  inventar.isfull[i] = true;

                                    Instantiate(pistoletprefab, inventar.slots[i].transform, false);


                                    break;

                                }

                            }

                    }
                    else
                    {

                        picAmmo.Magazinammo += pictolet_ammo;
                        Destroy(hit.transform.gameObject);
                    }
                   

  

				}


			}else

            if (hit.transform.tag == "pic_One_bullet")
            {
                tmp.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (ammoqutibor == false)

                    {

                        for (int i = 0; i < inventar.slots.Length; i++)
                        {

                            if (inventar.isfull[i] == false)
                            {
                                picAmmo.Magazinammo += 1;
                                Destroy(hit.transform.gameObject);
                                ammoqutibor = true;
                                inventar.isfull[i] = true;

                                Instantiate(pistoletprefab, inventar.slots[i].transform, false);


                                break;

                            }

                        }

                    }
                    else
                    {

                        picAmmo.Magazinammo += 1;
                        Destroy(hit.transform.gameObject);
                    }




                }

            }else

            if (hit.transform.tag == "healt_bag")
            {
                tmp.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                   

                        for (int i = 0; i < inventar.slots.Length; i++)
                        {

                            if (inventar.isfull[i] == false)
                            {
                                
                                Destroy(hit.transform.gameObject);
                                
                                

                                Instantiate(healtprefab, inventar.slots[i].transform, false);
                                inventar.isfull[i] = true;

                                break;

                            }

                        }

                    



                }

            }
           

            else
			{
				tmp.SetActive(false);
			}
  

        }
       
	}

   

	
}
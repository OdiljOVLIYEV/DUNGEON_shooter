using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpawnEnemy : MonoBehaviour
{
    
    public Transform[] spawnPoints;
    public GameObject enemy;
    public float minus_zombie;
    public float timespawn;
    
    
    void Start()
    {

	    StartCoroutine(Enemydrop());

    }
    
	
    
	
   public IEnumerator Enemydrop()
    {
	    
	    while (minus_zombie > 0)
        {
            
            Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            Instantiate(enemy, spawnPosition, Quaternion.identity);
		    minus_zombie --;
		    yield return new WaitForSeconds(timespawn);
        }
       

    }

    // Update is called once per frame
	public void Update()
	{

		

		   
	           
               
	           
			   
            
        

	    

        /* if(PhotonNetwork.IsMasterClient==false || PhotonNetwork.CurrentRoom.PlayerCount !=2)
         {
             return;

         }*/

    }

	
		
    
}

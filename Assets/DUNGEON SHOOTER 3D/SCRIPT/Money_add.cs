using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
public class Money_add : MonoBehaviour
{
    private Transform player; // Oyuncunun transformu
    public float magnetRange = 5f; // Tanganın oyuncuya doğru çekileceği mesafe
    public float moveSpeed = 5f; 
    private Rigidbody rb;
    public IntVariable moneyCount;
    public int moneynumber;
    public AudioSource sound;
    public Image white;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Oyuncuyu tag'ine göre bul
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object with tag 'Player' not found.");
        }
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Eğer mesafe belirli bir aralıkta ise tangayı oyuncuya doğru çek
        if (distanceToPlayer <= magnetRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<SphereCollider>().enabled = false;
            sound.Play();
            white.enabled = true;
            Invoke("whiteoff",0.1f);
            moneyCount.Value+=moneynumber;
            
        }
    }
    
    public void whiteoff()
    {
        white.enabled = false;
        Destroy(gameObject,0.34f);
    }
}

using System;
using Obvious.Soap;
using UnityEngine;

public class UnlockWeapon : MonoBehaviour
{
    [SerializeField] private IntVariable UnlockedWeapon;
    private WeaponSwitcher weaponSwitcher;
    [SerializeField] private int unlockweaponnumber;
    public LayerMask groundLayer;
    public static Action KatanaUnlock;
    [SerializeField] private ScriptableEventNoParam SaveEvent;
    public bool RotationObject;
    [SerializeField] private float rotationSpeed = 90f;
    //public WeaponSwitcher weaponSwitcher;
    // Start is called before the first frame update
    void Start()
    {
        weaponSwitcher = FindObjectOfType<WeaponSwitcher>(); 
        
    }

    private void Update()
    {
        if (RotationObject)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<BoxCollider>().isTrigger = true;
            RotateObject();
            
        }
        else
        {
            if (IsGrounded())
            {
            
                GetComponent<Rigidbody>().useGravity = false;
                Invoke("triggerOn",0.3f);
		 
            }
            else
            {
                GetComponent<Rigidbody>().useGravity = true;
                Invoke("triggeroff",0.3f);
            }
        }
        
        
       
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.tag == "Katana")
            {
                KatanaUnlock?.Invoke();
                
            }
            // UnlockedWeapons[2].Value = true;
            // weaponSwitcher.UnlockWeapon(0);
            // Debug.Log("Unlock weapon at index: " + unlockWeaponIndex);
            //UnlockedWeapon.Value = unlockWeaponIndex;
            // weaponSwitcher.CheckAndSetActiveWeapon(unlockWeaponIndex); // Qurolni almashtirishni chaqirish
            
            UnlockedWeapon.Value = unlockweaponnumber;
            weaponSwitcher.UnlockWeapon(UnlockedWeapon.Value);
            //SaveEvent.Raise();
            weaponSwitcher.CheckAndSetActiveWeapon(UnlockedWeapon.Value);
           
            Destroy(gameObject);
           
        }
      
    }
    
    bool IsGrounded()
    {
        // Trigger boxning pozitsiyasi
        Vector3 position = transform.position;
       

        // CheckSphere natijasi
        bool grounded = Physics.CheckSphere(position, 0.5f, groundLayer);
		
        

        return grounded;
		
    }
	
    
    public void triggerOn()
    {
        GetComponent<BoxCollider>().isTrigger = true;
		
		
    }
    
    public void triggeroff()
    {
        GetComponent<BoxCollider>().isTrigger = false;
		
		
    }
     private void RotateObject()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime); // Obyektni yuqoriga qarab aylantirish
    }
}
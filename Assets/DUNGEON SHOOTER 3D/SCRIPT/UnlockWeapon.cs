using System;
using Obvious.Soap;
using UnityEngine;

public class UnlockWeapon : MonoBehaviour
{
    [SerializeField] private IntVariable UnlockedWeapon;
    private WeaponSwitcher weaponSwitcher;
    [SerializeField] private int unlockweaponnumber;
    public LayerMask groundLayer;
    //public WeaponSwitcher weaponSwitcher;
    // Start is called before the first frame update
    void Start()
    {
        weaponSwitcher = FindObjectOfType<WeaponSwitcher>(); 
    }

    private void Update()
    {
        if (IsGrounded())
        {
            Debug.Log("yerda");
            GetComponent<Rigidbody>().useGravity = false;
            Invoke("triggerOn",0.3f);
		 
        }
        else
        {
            GetComponent<Rigidbody>().useGravity = true;
            Invoke("triggeroff",0.3f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // UnlockedWeapons[2].Value = true;
            // weaponSwitcher.UnlockWeapon(0);
            // Debug.Log("Unlock weapon at index: " + unlockWeaponIndex);
            //UnlockedWeapon.Value = unlockWeaponIndex;
            // weaponSwitcher.CheckAndSetActiveWeapon(unlockWeaponIndex); // Qurolni almashtirishni chaqirish
            UnlockedWeapon.Value = unlockweaponnumber;
            weaponSwitcher.UnlockWeapon(UnlockedWeapon.Value);
            weaponSwitcher.CheckAndSetActiveWeapon(UnlockedWeapon.Value);
            Destroy(gameObject);
        }
    }
    
    bool IsGrounded()
    {
        // Trigger boxning pozitsiyasi
        Vector3 position = transform.position;
        Debug.Log("Trigger Box Position: " + position);

        // CheckSphere natijasi
        bool grounded = Physics.CheckSphere(position, 0.5f, groundLayer);
		
        Debug.Log("CheckSphere Grounded: " + grounded);

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
}
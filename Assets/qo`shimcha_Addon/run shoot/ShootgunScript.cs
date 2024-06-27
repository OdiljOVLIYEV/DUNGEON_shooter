using System.Collections;
using Obvious.Soap;
using UnityEngine;

public class ShotgunScript : MonoBehaviour
{
    public int damage = 10; // Zarar miqdori
    public float range = 100f; // Raycastning masofasi
    public int pellets = 4; // Bir o'q bilan nechta raycast amalga oshirilishi
    public float spreadAngle = 10f; // Sochilma burchagini belgilash

    public Camera fpsCam; // O'yinchining kamerasini belgilash
    public AudioSource sound;

    private Vector3[] directions; // Yo'nalishlar massivi
    private bool isShooting; // Raycast chizish uchun flag
    public Animator anim;
    [SerializeField] private IntVariable  ammo_UI;
    [SerializeField] private IntVariable shotgun_ammo_add;
    [SerializeField] private ScriptableEventInt UI_AMMO_UPDATE;
    void Start()
    {
        
        directions = new Vector3[pellets]; // Yo'nalishlar massivini initsializatsiya qilish
    }

    private void Update()
    {
        UI_AMMO_UPDATE.Raise(shotgun_ammo_add.Value);
        ammo_UI.Value = shotgun_ammo_add.Value;
        PlayerMovment mov=FindObjectOfType<PlayerMovment>();
		
        if(mov.x<0||mov.x>0||mov.z>-0||mov.z<0){
            anim.SetBool("walk",true);
			
        }else if(mov.x>-1||mov.x<1||mov.z>-1||mov.z<1){
			
            anim.SetBool("walk",false);	
			
			
        }

       

        if (Input.GetKey("left shift"))
        {
			
				
				
            anim.SetBool("Run",true);	
			
            if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("Run",false);
                if(shotgun_ammo_add.Value>0){
                    if(Input.GetButtonDown("Fire1"))
                    {
						

                        Shoot();
			
                    }
		
                }
            }
        }
        else
        {
            anim.SetBool("Run",false);
            if ( shotgun_ammo_add.Value > 0)
            {
                if (Input.GetButtonDown("Fire1")) // Chap sichqoncha tugmasi bosilganda
                {
                    Shoot();
                }
            }
        } 
        
    }

    void Shoot()
    {
        shotgun_ammo_add.Value--;
        sound.Play();
        UI_AMMO_UPDATE.Raise(shotgun_ammo_add.Value);
        RaycastHit hit1;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit1, range))
        {
            IDamageable damageable=hit1.transform.GetComponent<IDamageable>();
            if (damageable != null)
            {
                //Instantiate(Blood,hit.point,Quaternion.FromToRotation(Vector3.right,hit.normal));
                damageable.TakeDamage(damage,hit1.point,hit1.normal);
            }

               
        }
        anim.SetBool("shoot",true);
        StartCoroutine(gunanim());
        isShooting = true;
        for (int i = 0; i < pellets; i++)
        {
            // Oldinga yo'nalishni olamiz
            Vector3 direction = fpsCam.transform.forward;

            // Sochilma effektini yaratish uchun tasodifiy burchaklar qo'shamiz
            direction = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0) * direction;

            directions[i] = direction; // Yo'nalishni saqlash

            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range))
            {
                IDamageable damageable=hit.transform.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    //Instantiate(Blood,hit.point,Quaternion.FromToRotation(Vector3.right,hit.normal));
                    damageable.TakeDamage(damage,hit.point,hit.normal);
                }

               
            }
        }
        // Bir oz kutib, raycast chizish tugashidan keyin flagni o'chirish
        Invoke(nameof(ResetShootingFlag), 0.1f);
    }

    void ResetShootingFlag()
    {
        isShooting = false;
    }

    IEnumerator gunanim()
    {
        yield return new WaitForSeconds(0.01f);
        anim.SetBool("shoot",false);
    }
    void OnDrawGizmos()
    {
        if (!isShooting || directions == null || fpsCam == null) return;

        Gizmos.color = Color.red; // Gizmos rangini qizil qilib qo'yamiz

        for (int i = 0; i < directions.Length; i++)
        {
            // Har bir raycast yo'nalishini kameradan boshlab tasvirlash
            Gizmos.DrawRay(fpsCam.transform.position, directions[i] * range);
        }
    }
    
    
}

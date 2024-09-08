using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class SuperShotgunscript : MonoBehaviour
{
    public int damage = 10; // Zarar miqdori
    public float range = 100f; // Raycastning masofasi
    public int pellets = 4; // Bir o'q bilan nechta raycast amalga oshirilishi
    public float spreadAngle = 10f; // Sochilma burchagini belgilash
    public int bullets=2;
    public float fireRate = 0.1f;
    private float nextFireTime = 0f;
    public Camera fpsCam; // O'yinchining kamerasini belgilash
    public AudioSource sound;

    private Vector3[] directions; // Yo'nalishlar massivi
    private bool isShooting;
    public Animator anim;
    [SerializeField] private IntVariable ammo_UI;
    [SerializeField] private IntVariable shotgun_ammo_add;
    [SerializeField] private ScriptableEventInt UI_AMMO_UPDATE;
    [SerializeField] private FloatVariable speed;
    [SerializeField] private BoolVariable canShoot;
    [SerializeField] private BoolVariable WeaponUI_Open;
    [SerializeField] private ScriptableEventNoParam SaveEvent;
    [SerializeField] private ScriptableEventNoParam LoadEvent;
    public LayerMask enemyLayer;
    public ParticleSystem bullet;

    #region MyRegion

    public GameObject bulletCasingPrefab; // Gilza prefabini joylashtiring
    public Transform casingEjectionPoint; // Gilza chiqariladigan nuqta

    public float casingEjectionForce = 2f; // Gilza chiqarish kuchi

    #endregion

    public GameObject bulletPrefab; // Tracer prefabini joylashtiring
    public Transform bulletSpawnPoint; // Tracer uchun chiqish nuqtasi
    public float bulletSpeed = 1000f; // Tracer tezligi

    void Start()
    {
        LoadData();
        directions = new Vector3[pellets]; // Yo'nalishlar massivini initsializatsiya qilish
    }

    private void Update()
    {
        UI_AMMO_UPDATE.Raise(shotgun_ammo_add.Value);
        ammo_UI.Value = shotgun_ammo_add.Value;
        PlayerMovment mov = FindObjectOfType<PlayerMovment>();

        if (anim.GetBool("shoot") == false)
        {
            if (mov.x != 0 || mov.z != 0)
            {
                anim.SetBool("walk", true);
            }
            else
            {
                speed.Value = 0;
                anim.SetBool("walk", false);
            }

            if (Input.GetKey("left shift") && speed.Value > 0)
            {
                anim.SetBool("Run", true);
            }
            else
            {
                anim.SetBool("Run", false);
            }
        }

        if (shotgun_ammo_add.Value > 0 && Input.GetButtonDown("Fire1") && canShoot.Value == true && WeaponUI_Open.Value == false)
        {
            if (Time.time >= nextFireTime)
            {
                anim.SetBool("shoot", true);
                canShoot.Value = false;
                Shoot();
                nextFireTime = Time.time + fireRate; // Set the next fire time 1 second from now
            }
        }
        else
        {
            canShoot.Value = true;
            anim.SetBool("shoot", false);
        }
    }

    void Shoot()
    {
        SaveData();
        sound.Play();
        UI_AMMO_UPDATE.Raise(shotgun_ammo_add.Value);
        isShooting = true;
        bullet.Play();
       int currentDamage = damage;
      if (shotgun_ammo_add.Value == 1)
      {
        currentDamage = damage / 2; // Reduce damage to 50%
        shotgun_ammo_add.Value -= 1; // Reduce ammo by 1
      }
      else
      {
          shotgun_ammo_add.Value -= bullets; // Reduce ammo by the regular amount
      } 
        for (int i = 0; i < pellets; i++)
        {
            Vector3 direction = fpsCam.transform.forward;
            direction = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0) * direction;
            directions[i] = direction;

            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range, ~(1 << LayerMask.NameToLayer("Player"))))
            {
                ProcessHit(hit, direction);
            }

            FireBullet(fpsCam.transform.position, direction);
        }

        ResetShootingFlag();
    }



    private void ProcessHit(RaycastHit hit, Vector3 direction)
    {
        IDamageable damageable = hit.transform.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage, hit.point, hit.normal);
            Debug.Log(hit.transform.gameObject.name);
        }

        RaycastHit[] hits = Physics.RaycastAll(hit.point + direction * 0.1f, direction, range - hit.distance, ~(1 << LayerMask.NameToLayer("Player")));
        foreach (RaycastHit raycastHit in hits)
        {
            if (raycastHit.collider != hit.collider)
            {
                IDamageable damageableBehind = raycastHit.transform.GetComponent<IDamageable>();
                if (damageableBehind != null)
                {
                    damageableBehind.TakeDamage(damage, raycastHit.point, raycastHit.normal);
                    Debug.Log(raycastHit.transform.gameObject.name);
                }
            }
        }
    }



    void FireBullet(Vector3 origin, Vector3 direction)
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direction * bulletSpeed, ForceMode.Impulse);
        }
    }

    void EjectCasing()
    {
        GameObject casing = Instantiate(bulletCasingPrefab, casingEjectionPoint.position, casingEjectionPoint.rotation);
        Rigidbody rb = casing.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 ejectionDirection = casingEjectionPoint.right;
            rb.AddForce(ejectionDirection * casingEjectionForce, ForceMode.Impulse);
        }
    }

    void ResetShootingFlag()
    {
        isShooting = false;
    }

    

    void OnDrawGizmos()
    {
        if (!isShooting || directions == null || fpsCam == null) return;

        Gizmos.color = Color.red;

        for (int i = 0; i < directions.Length; i++)
        {
            Gizmos.DrawRay(fpsCam.transform.position, directions[i] * range);
        }
    }
    
    private void OnEnable()
    {
        SaveEvent.OnRaised += SaveData;
       
    }

    private void OnDisable()
    {
        SaveEvent.OnRaised -= SaveData;
        
    }

    public void SaveData()
    {
        PlayerData data = SaveManager.instance.LoadPlayerData();
        data.AmmoSuperShotgun = shotgun_ammo_add.Value;
        SaveManager.instance.SavePlayerData(data);
      
        
    }


    public void LoadData()
    {
        PlayerData data = SaveManager.instance.LoadPlayerData();
        shotgun_ammo_add.Value = data.AmmoSuperShotgun;
      
    }
}

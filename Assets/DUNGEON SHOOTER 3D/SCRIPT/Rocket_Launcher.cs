using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class Rocket_Launcher : MonoBehaviour
{
   public float damage = 10f; // O'qning zarar miqdori
    public float fireRate = 0.1f;
    private float nextFireTime = 0f;
    public float shootRadius = 20f;
    [SerializeField] private IntVariable ammo_UI;
    [SerializeField] private IntVariable rocket_launcher_ammo_add;
    [SerializeField] private ScriptableEventInt UI_AMMO_UPDATE;
    [SerializeField] private FloatVariable speed;
    [SerializeField] private BoolVariable WeaponUI_Open;
    public Camera cam;
    public Animator anim;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 1000f;

    public AudioSource sound;
    public LayerMask enemyLayer;
    public ParticleSystem bullet;

    protected void Start()
    {
    }

    public void Use()
    {
        Debug.Log("Gun is used");
    }

    private void Update()
    {
        UI_AMMO_UPDATE.Raise(rocket_launcher_ammo_add.Value);
        ammo_UI.Value = rocket_launcher_ammo_add.Value;
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

        if (rocket_launcher_ammo_add.Value > 0)
        {
            if (Input.GetButtonDown("Fire1") && Time.time > nextFireTime&&WeaponUI_Open.Value==false)
            {
                nextFireTime = Time.time + fireRate;
                Shoot();
            }
            
        }
        
    }

    private void Shoot()
    {
        anim.SetBool("shoot", true);
        StartCoroutine(GunAnim());
        sound.Play();
        bullet.Play();
        rocket_launcher_ammo_add.Value--;
        UI_AMMO_UPDATE.Raise(rocket_launcher_ammo_add.Value);

        Vector3 shootDirection = cam.transform.forward;
        Vector3 shootOrigin = bulletSpawnPoint.position;

        FireBullet(shootOrigin, shootDirection);
    }

    private void FireBullet(Vector3 start, Vector3 direction)
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, start, Quaternion.LookRotation(direction));
        Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = damage; // O'qning zararini belgilash
        }
        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direction * bulletSpeed, ForceMode.Impulse);
        }
    }

    private IEnumerator GunAnim()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("shoot", false);
    }
}

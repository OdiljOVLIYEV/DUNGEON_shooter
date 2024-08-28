using System.Collections;
using Obvious.Soap;
using UnityEngine;

public class Plasma : MonoBehaviour
{
    public float damage = 10f; // O'qning zarar miqdori
    public float fireRate = 0.1f;
    private float nextFireTime = 0f;
    public float shootRadius = 20f;
    [SerializeField] private IntVariable ammo_UI;
    [SerializeField] private IntVariable plasma_ammo_add;
    [SerializeField] private ScriptableEventInt UI_AMMO_UPDATE;
    [SerializeField] private FloatVariable speed;
    [SerializeField] private BoolVariable WeaponUI_Open;
    [SerializeField] private BoolVariable Main_menu;
    public Camera cam;
    public Animator anim;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 1000f;

    public AudioSource sound;
    public LayerMask enemyLayer;
    public ParticleSystem bullet;

    private void Update()
    {
        // Amunitsiyani UI ga yangilash
        UI_AMMO_UPDATE.Raise(plasma_ammo_add.Value);
        ammo_UI.Value = plasma_ammo_add.Value;

        PlayerMovment mov = FindObjectOfType<PlayerMovment>();

        // Harakat animatsiyalarini boshqarish
        if (!anim.GetBool("shoot"))
        {
            anim.SetBool("walk", mov.x != 0 || mov.z != 0);
            anim.SetBool("Run", Input.GetKey("left shift") && speed.Value > 0);
        }

        // O'q otish amaliyoti
        if (plasma_ammo_add.Value > 0 && Input.GetButton("Fire1") && Time.time > nextFireTime&&WeaponUI_Open.Value==false&&Main_menu==false)
        {
            anim.SetBool("shoot", true);
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
        else
        {
            anim.SetBool("shoot", false);
        }
    }

    private void Shoot()
    {
        // Animatsiyani yoqish
        

        // Ovozni va zarracha effektini o'ynatish
        sound.Play();
        bullet.Play();

        // Amunitsiyani kamaytirish va UI ni yangilash
        plasma_ammo_add.Value--;
        UI_AMMO_UPDATE.Raise(plasma_ammo_add.Value);

        // O'q otishni boshlash
        StartCoroutine(GunAnim());
    }

    private IEnumerator GunAnim()
    {
        // O'q otish yo'nalishi va boshlang'ich joyi
        Vector3 shootDirection = cam.transform.forward;
        Vector3 shootOrigin = bulletSpawnPoint.position;

        // O'qni otish
        FireBullet(shootOrigin, shootDirection);

        // Animatsiyaning davomiyligini kutish
        yield return new WaitForSeconds(0.1f);

        // Animatsiyani o'chirish
        anim.SetBool("shoot", false);
    }

    private void FireBullet(Vector3 start, Vector3 direction)
    {
        // O'q ob'ektini yaratish
        GameObject bulletInstance = Instantiate(bulletPrefab, start, Quaternion.LookRotation(direction));

        // O'qning zarar miqdorini belgilash
        Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = damage;
        }

        // O'qning yo'nalishi va tezligini o'rnatish
        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direction * bulletSpeed, ForceMode.Impulse);
        }
    }
}

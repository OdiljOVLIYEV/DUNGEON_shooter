using System.Collections;
using UnityEngine;
using Obvious.Soap;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float shootRadius = 20f;
    [SerializeField] private IntVariable ammo_UI;
    [SerializeField] private IntVariable gun_ammo_add;
    [SerializeField] private ScriptableEventInt UI_AMMO_UPDATE;
    [SerializeField] private FloatVariable speed;
    [SerializeField] private BoolVariable WeaponUI_Open;
    public Camera cam;
    public Animator anim;

    #region MyRegion

    public GameObject bulletCasingPrefab; // Gilza prefabini joylashtiring
    public Transform casingEjectionPoint; // Gilza chiqariladigan nuqta
    public float casingEjectionForce = 2f; // Gilza chiqarish kuchi

    #endregion

    public AudioSource sound;
    public LayerMask enemyLayer;
 
    public ParticleSystem bullet;

    // Tracer bullet properties
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 1000f;

    protected void Start()
    {
    }

    public void Use()
    {
        Debug.Log("Gun is used");
    }

    private void Update()
    {
        UI_AMMO_UPDATE.Raise(gun_ammo_add.Value);
        ammo_UI.Value = gun_ammo_add.Value;
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

        if (gun_ammo_add.Value > 0)
        {
            if (Input.GetButtonDown("Fire1")&& WeaponUI_Open.Value==false)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        anim.SetBool("shoot", true);
        StartCoroutine(gunanim());
        sound.Play();
        bullet.Play();
        gun_ammo_add.Value--;
       // UI_AMMO_UPDATE.Raise(gun_ammo_add.Value);
        EjectCasing();

        RaycastHit hit;
        int playerLayer = LayerMask.NameToLayer("Player");
        int layerMask = ~(1 << playerLayer);

        Vector3 shootDirection = cam.transform.forward;
        Vector3 shootOrigin = cam.transform.position;

        if (Physics.Raycast(shootOrigin, shootDirection, out hit, range, layerMask))
        {
            ProcessHit(hit);
            FireBullet(shootOrigin, hit.point);
        }
        else
        {
            FireBullet(shootOrigin, shootOrigin + shootDirection * range);
        }
    }

    private void ProcessHit(RaycastHit hit)
    {
        IDamageable damageable = hit.transform.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage, hit.point, hit.normal);
            Debug.Log(hit.transform.gameObject.name);
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, shootRadius, enemyLayer);
        foreach (Collider hitCollider in hitColliders)
        {
            AIController aiController = hitCollider.GetComponent<AIController>();
            if (aiController != null)
            {
                aiController.HeardSound(transform.position);
            }
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

    private void FireBullet(Vector3 start, Vector3 end)
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (end - start).normalized;
            rb.AddForce(direction * bulletSpeed, ForceMode.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(cam.transform.position, cam.transform.forward * range);
    }

    IEnumerator gunanim()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("shoot", false);
    }
}

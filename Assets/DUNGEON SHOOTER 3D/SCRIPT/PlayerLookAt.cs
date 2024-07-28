using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
    private Transform player; // Oyuncu GameObject'inin Transform bileşeni

    void Start()
    {
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

    void Update()
    {
        if (player != null)
        {
            // Nesnenin oyuncuya bakmasını sağlayacak dönüşü hesapla
            Vector3 direction = player.position - transform.position;
            direction.y = 0; // Y ekseninde dönmesini istemiyorsanız bu satır gereklidir
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
        }
    }
}
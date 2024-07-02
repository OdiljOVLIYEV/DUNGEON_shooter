using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MiniRadar : MonoBehaviour
{
    public Transform player;
    public RectTransform radarImage;
    public GameObject blipPrefab;
    public float radarScale = 1.0f;
    public float maxRadarDistance = 50.0f;

    private List<GameObject> blips = new List<GameObject>();

    void Update()
    {
        UpdateBlips();
    }

    void UpdateBlips()
    {
        // Eski bliplarni tozalash
        foreach (GameObject blip in blips)
        {
            Destroy(blip);
        }
        blips.Clear();

        // Yangi bliplarni yaratish
        foreach (Transform target in FindTargets())
        {
            Vector3 playerPos = player.position;
            Vector3 targetPos = target.position;

            Vector3 relativePos = targetPos - playerPos;
            relativePos.y = 0; // y koordinatasini inkor qilish, chunki biz faqat 2D radarda ko'rsatmoqdamiz

            if (relativePos.magnitude <= maxRadarDistance)
            {
                Vector2 radarPos = new Vector2(relativePos.x, relativePos.z) * radarScale;
                radarPos = RotatePointAroundPivot(radarPos, Vector2.zero, player.eulerAngles.y); // Aylanish burchagini o'ngga (nega) aylantirish
                GameObject blip = Instantiate(blipPrefab, radarImage);
                blip.GetComponent<RectTransform>().anchoredPosition = radarPos;
                blips.Add(blip);
            }
        }
    }

    Vector2 RotatePointAroundPivot(Vector2 point, Vector2 pivot, float angle)
    {
        Vector2 dir = point - pivot;
        float radAngle = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(radAngle);
        float sin = Mathf.Sin(radAngle);
        Vector2 newDir = new Vector2(dir.x * cos - dir.y * sin, dir.x * sin + dir.y * cos);
        return newDir + pivot;
    }

    Transform[] FindTargets()
    {
        // O'yindagi barcha dushmanlarni topish uchun
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform[] enemyTransforms = new Transform[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyTransforms[i] = enemies[i].transform;
        }
        return enemyTransforms;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{

    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();

        DetectEnemiesInZone();
    }

    void DetectEnemiesInZone()
    {

        int layerMask = 1 << 6;
        Collider2D[] enemies = Physics2D.OverlapBoxAll(transform.position, renderer.bounds.size, 0f, layerMask);

        foreach (var item in enemies)
        {
            Enemy enemy = item.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.Dance();
            }
        }
    }
}

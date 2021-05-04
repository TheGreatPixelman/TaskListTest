using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Coroutine coDance;
    SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Dance()
    {
        if (coDance == null)
            coDance = StartCoroutine(DanceCoroutine());
    }

    IEnumerator DanceCoroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            sprite.flipX = !sprite.flipX;
            yield return new WaitForSeconds(0.5f);
        }

        yield return null;
    }
}

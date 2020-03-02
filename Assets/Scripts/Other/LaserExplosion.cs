﻿using System.Collections;
using UnityEngine;

public class LaserExplosion : MonoBehaviour {
    void Update() {
        StartCoroutine(FadeTo(0.0f, 0.5f));
    }
    private IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = transform.GetComponent<SpriteRenderer>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
            transform.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }
        Destroy(gameObject);
    }
}

using System.Collections;
using Photon;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviourPun
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f)*magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(transform.position.x + x, transform.position.y + y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        //transform.position = originalPos;
    }
}

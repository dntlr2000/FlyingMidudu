using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScaler : MonoBehaviour
{
    private Vector3 originScale;
    private Vector3 StartScale;
    // Start is called before the first frame update
    void Start()
    {
        originScale = new Vector3(gameObject.transform.localScale.x, transform.localScale.y, transform.localScale.z);
        StartScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.localScale = StartScale;
        StartCoroutine(ScaleToOrigin());
    }

    private IEnumerator ScaleToOrigin(float duration = 0.5f)
    {
        float elapsedTime = 0f;
        transform.localScale = StartScale;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            transform.localScale = Vector3.Lerp(StartScale, originScale, t);
            yield return null;
        }

        transform.localScale = originScale; // 정확하게 맞추기
    }
}

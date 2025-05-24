using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTimer : MonoBehaviour
{
    public float lifetime = 2.0f;
    private Camera targetCamera;

    void Start()
    {
        targetCamera = Camera.main; // fallback
        if (targetCamera == null)
        {
            Debug.LogWarning("카메라가 비어있습니다.");
        }

        Destroy(gameObject, lifetime);
        
    }

    

    void LateUpdate()
    {
        // 카메라를 정면으로 향하게 (Z 축 기준 회전)
        transform.forward = targetCamera.transform.forward;
    }
}

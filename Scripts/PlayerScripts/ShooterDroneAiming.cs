using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterDroneAiming : MonoBehaviour
{
    public Transform target; // 따라갈 대상 오브젝트
    private float speed = 0.03f; // 따라가는 속도

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed);
            transform.position = smoothedPosition;
        }
    }
}

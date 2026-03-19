using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterDroneAiming : MonoBehaviour
{
    public Transform target;
    private float speed = 6.0f;

    void Update()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position;

            //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed); //Time.deltaTime ¹ÌÀû¿ë
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);

            transform.position = smoothedPosition;
        }
    }
}
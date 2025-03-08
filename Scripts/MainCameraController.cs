using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class MainCameraController : MonoBehaviour
{
    // 주석: 실패의 흔적들
    public GameObject cameraObj_p;

    private Transform camera_p;
    public Transform camera_e;

    private bool ifCutScene = false;

    /*
    void Start()
    {
        
        //시작 시에는 항상 웁의 카메라를 따라가도록

        isCutScene = false;
        transform.position = camera_p.position;
        transform.rotation = camera_p.rotation;


        
        ConstraintSource source = new ConstraintSource
        {
            sourceTransform = camera_p,
            weight = 1.0f
        };

        // PositionConstraint 설정
        //positionConstraint.SetSource(0, source);
        //positionConstraint.constraintActive = true;

        // RotationConstraint 설정
        //rotationConstraint.SetSource(0, source);
        //rotationConstraint.constraintActive = true;
        

    }
    */

    private void Awake()
    {
        camera_p = cameraObj_p.GetComponent<Transform>();
        transform.position = camera_p.position;
        transform.rotation = camera_p.rotation;
    }

    private void Update()
    {
        if (ifCutScene)
        {
            transform.position = camera_e.position;
            transform.rotation = camera_e.rotation;
        }
    }


    public IEnumerator BossCutScene(float cutSceneTime)
    {
        
        transform.position = camera_p.position;
        transform.rotation = camera_p.rotation;

        cameraObj_p.SetActive(false);
        float duration = 0.5f;
        float elapsed = 0f;

        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;


        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            transform.position = Vector3.Lerp(startPosition, camera_e.position, t);
            transform.rotation = Quaternion.Slerp(startRotation, camera_e.rotation, t);

            yield return null;
        }
        ifCutScene = true; //카메라 계속 따라가도록
        yield return new WaitForSeconds(cutSceneTime);
        ifCutScene = false;
        StartCoroutine(ToPlayerCamera());
    }

    private IEnumerator ToPlayerCamera()
    {
        float duration = 0.5f;
        float elapsed = 0f;

        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;


        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            transform.position = Vector3.Lerp(startPosition, camera_p.position, t);
            transform.rotation = Quaternion.Slerp(startRotation, camera_p.rotation, t);

            yield return null;


        }
        //isCutScene = false;
        cameraObj_p.SetActive(true);
        gameObject.SetActive(false);

    }

   
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; //카메라 회전점
    protected float distance = 10f; //카메라로부터의 거리
    public Vector2 rotationSpeed = new Vector2(240.0f, 240.0f); //감도
    private float minY = -80f;
    private float maxY = 80f;

    private float angleX = 0;
    private float angleY = 0;

    private bool farMode = false;

    private Animator animator;

    //대칭점에 조준점 위치
    public Transform aimCenter;

    private PlayerSetting PlayerSettingScript;

    // Start is called before the first frame update
    //웁의 경우 CameraWithBone에 있다
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        angleX = angles.y;
        angleY = maxY;
        UpdateCameraPosition();

        animator = GetComponent<Animator>();
        PlayerSettingScript = FindObjectOfType<PlayerSetting>();

        if (PlayerSettingScript != null) SetRotationSpeed(PlayerSettingScript.MouseSpeed, PlayerSettingScript.MouseSpeed);
    }

    private void Update()
    {
        CameraModeChanger();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            //마우스로부터 움직임 받기
            angleX += Input.GetAxis("Mouse X") * rotationSpeed.x * Time.deltaTime;
            angleY -= Input.GetAxis("Mouse Y") * rotationSpeed.y * Time.deltaTime;
            //Y축 최대 높낮이 설정
            angleY = Mathf.Clamp(angleY, minY, maxY);

            UpdateCameraPosition();
        }
    }

    private void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(angleY, angleX, 0); //회전 생성
        Vector3 position = rotation * new Vector3(0, 0, -distance) + target.position; //위치 계산

        transform.position = position;
        transform.rotation= rotation;

        Vector3 aimPosition = rotation * new Vector3(0, 0, 100f) + target.position;
        aimCenter.position = aimPosition;
        aimCenter.rotation = rotation;


    }

    private void CameraModeChanger()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            farMode = !farMode;
            float targetDistance = farMode ? 30f : 10f; 

            StartCoroutine(ChangeCameraDistance(targetDistance, 0.5f));
        }
    }
    private IEnumerator ChangeCameraDistance(float targetDistance, float duration)
    {
        float elapsedTime = 0f;
        float startDistance = distance;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            distance = Mathf.Lerp(startDistance, targetDistance, elapsedTime / duration);

            UpdateCameraPosition(); 
            yield return null;
        }

        distance = targetDistance;
        UpdateCameraPosition();
    }

    public void CameraShake(int strength)
    {
        Debug.Log($"CameraShake : {strength}");
        StartCoroutine(ShakeCoroutine(strength)); 
    }

    private IEnumerator ShakeCoroutine(int strength) {
        animator.SetInteger("ShakeStrength", strength);
        yield return new WaitForSeconds(0.2f);
        animator.SetInteger("ShakeStrength", 0);

    }

    public void SetRotationSpeed(float x, float y)
    {
        rotationSpeed = new Vector2(x, y);
    }
}

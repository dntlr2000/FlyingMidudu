using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform baseCamera; //이동방향의 중심이 될 카메라
    private float h_speed = 10f; //수평 이동
    private float v_speed = 1f; //수직 이동
    private float dashMul = 2f; //대시 속도(배수)

    public Vector3 minBounds;
    public Vector3 maxBounds;

    public Player PlayerScript;

    private bool usingUlt = false;
    public GameObject UltPrefab;

    //private PlayerSetting PlayerSettingScript;


    // Start is called before the first frame update
    void Start()
    {
        //PlayerSettingScript = FindObjectOfType<PlayerSetting>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (!baseCamera)
        {
            return;
        }

        Vector3 forward = baseCamera.forward;
        Vector3 right = baseCamera.right;
        forward.y = 0; // 수평 이동에만 집중
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) //조작을 통해 방향 갱신
        {
            direction += forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction -= forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction -= right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += right;
        }

        float currentSpeed = h_speed; //기본 이동속도로 현재 이동속도 설정
        if (Input.GetKey(KeyCode.LeftShift)) //shift를 누르는 동안 현재 이동속도의 n배수
        {
            currentSpeed *= dashMul;
        }

        //캐릭터 이동 방향으로 회전(수평)
        if (direction != Vector3.zero) //방향이 없을 때를 제외하고
        {
            // 캐릭터 방향을 이동 방향으로 회전시키기
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.05f);
            //방향이 바뀌었을 때 프레임마다 15% 돌도록 하여 비교적 자연스럽게 회전하도록 함
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                direction += Vector3.down * v_speed; // space와 LeftControl이 동시에 입력이 들어올 경우 아래로 이동이 우선됨
            }
            else
            {
                direction += Vector3.up * v_speed; // 위로 이동
            }
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            direction += Vector3.down * v_speed; // 아래로 이동
        }


        //transform.Translate(direction * currentSpeed * Time.deltaTime, Space.World);
        Vector3 newPosition = transform.position + direction * currentSpeed * Time.deltaTime; //위치 갱신

        // x, y, z 축의 위치를 제한
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);
        newPosition.z = Mathf.Clamp(newPosition.z, minBounds.z, maxBounds.z);

        transform.position = newPosition;

        if (Input.GetKey(KeyCode.Q))
        {
            if (usingUlt == false && PlayerScript.Bomb > 0)
            {
                usingUlt = true;
                PlayerScript.Bomb -= 1;
                StartCoroutine(useUlt());
            }
        }
    }

    public IEnumerator useUlt()
    {
        StartCoroutine(PlayerScript.UltInvincible());
        for (int i = 0; i < 10; i++)
        {
            Vector3 forward = baseCamera.forward;
            Quaternion rotation = baseCamera.rotation;
            GameObject redBall = Instantiate(UltPrefab, transform.position + forward * 2f, rotation);
            Rigidbody rb = redBall.GetComponent<Rigidbody>();
            rb.AddForce(forward * 100f, ForceMode.Impulse);
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(2.1f);
        usingUlt= false;
    }

    
    
}

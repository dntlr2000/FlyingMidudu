using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetting : MonoBehaviour
{
    private int playerStock = 3;
    private int playerBomb = 3;
    private float mouseSpeed = 240f;

    public int PlayerStock
    {
        get { return playerStock; } 
        set { playerStock = value; }
    }

    public int PlayerBomb
    {
        get { return playerBomb; }
        set { playerBomb = value; }
    }

    public float MouseSpeed
    {
        get { return mouseSpeed; }
        set { mouseSpeed = value; }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static PlayerSetting instance; // 싱글톤 인스턴스

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // 씬 이동 시 삭제되지 않음
    }

}

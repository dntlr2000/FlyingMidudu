using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WoopLifeUI : MonoBehaviour
{
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI bombText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLife(int life)
    {
        lifeText.text = life.ToString();
    }

    public void SetBomb(int bomb)
    {
        bombText.text = bomb.ToString();
    }
}

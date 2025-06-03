using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverHint : MonoBehaviour
{
    public TextMeshPro Text;
    public string[] Texts;
    void Start()
    {
        int randomIndex = Random.Range(0, Texts.Length);
        Text.text = Texts[randomIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

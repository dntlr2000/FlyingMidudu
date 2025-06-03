using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MenuParent
{
    // Start is called before the first frame update
    public GameObject overMenu;
    public Player playerScript;

    //public GameObject TextObject;
    public TextMeshProUGUI Text;
    public string[] Texts;



    protected override void Start()
    {
        BGM_Script = FindObjectOfType<BGMController>();
        overMenu.SetActive(false);
        CursorSwitch(true);
    }

    // Update is called once per frame

    public void PauseGame()
    {
        overMenu.SetActive(true);
        Time.timeScale = 0f;
        //AudioListener.pause = true;
        BGM_Script.StopBGM();
        CursorSwitch(false);
        //Text = TextObject.GetComponent<TextMesh>
        int randomIndex = Random.Range(0, Texts.Length);
        Text.text = "ÈùÆ®: " + Texts[randomIndex];
    }

    public void ResumeGame()
    {
        overMenu.SetActive(false);
        Time.timeScale = 1f;
        //AudioListener.pause = false;

        playerScript.gameObject.SetActive(true);
        playerScript.Life = 3;
        playerScript.Bomb = 2;
        StartCoroutine(playerScript.Respawn());
        CursorSwitch(true);
    }

    public void ToMain()
    {
        overMenu.SetActive(false);
        Time.timeScale = 1f;
        StageLoader("MainMenu");
        CursorSwitch(true);
    }
    
    public void RestartGame(string sceneName)
    {
        overMenu.SetActive(false);
        Time.timeScale = 1f;
        StageLoader(sceneName);
        CursorSwitch(true);
    }
}

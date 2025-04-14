using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MenuParent
{
    public GameObject pauseMenu;
    private bool isPause = false;

    protected override void Start()
    {
        BGM_Script = FindObjectOfType<BGMController>();
        pauseMenu.SetActive(false);
        CursorSwitch(true);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
        //AudioListener.pause = true;
        if (BGM_Script != null) BGM_Script.PauseBGM();
        CursorSwitch(false);

    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
        //AudioListener.pause = false;
        if (BGM_Script != null)  BGM_Script.PlayBGM();
        CursorSwitch(true);
    }

    public void ToMain()
    {
        if (BGM_Script != null) BGM_Script.PlaySFX(0);
        Time.timeScale = 1f;
        StageLoader("MainMenu");
        pauseMenu.SetActive(false);
    }

    public void RestartGame(string sceneName)
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        StageLoader(sceneName);
        
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    
    public AudioSource bgmSource;
    public AudioSource SFXSource;

    public AudioClip[] bgmClips;
    public AudioClip[] SFXClips;
    //public AudioClip[] ShootClips;


    protected int bgmIndex = 0;
    private float settedVolume = 0.5f;


    private float volume_SFX = 0.5f;
    //private int index_SFX = 0;
    private string[] BgmArtist;

    void Start()
    {
        //bgmSource= GetComponent<AudioSource>();
        bgmSource.clip = bgmClips[bgmIndex];
        bgmSource.loop = true; // 반복 재생
        //Debug.Log("Bgm On");
        PlayBGM();

        //SFXSource.clip = SFXClips[Index_SFX];
        SFXSource.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int BgmIndex
    {
        get { return bgmIndex; }
        set { bgmIndex = value; }
    }

    public float SettedVolume
    {
        get { return settedVolume; }
        set { settedVolume = value; }
    }


    public void PlayBGM(float fadeDuration = 1f)
    {

        if (bgmClips[BgmIndex] == null) { return; }

        if (bgmSource.isPlaying)
        {
            Debug.Log("Already Playing");
            return;
        }
        else
        {
            Debug.Log($"Playing Track : {BgmIndex}");
            FadeInBGM(fadeDuration);
            bgmSource.Play();
            
        }
    }

    public void PauseBGM()
    {
        if (!bgmSource.isPlaying) { return; }
        bgmSource.Pause();
        //ChangeBGM(1);
    }

    public void StopBGM(bool isfadeOut = false)
    {
        Debug.Log($"Stopped Track : {BgmIndex}");
        if (!isfadeOut)
        {
            bgmSource.Stop();
        }
        else
        {
            FadeOutBGM();
        }
        
    }

    public void ChangeBGM(int newIndex, bool ifLoop = true)
    {
        if (bgmClips[BgmIndex] == null) { return; }

        BgmIndex = newIndex;

        bgmSource.clip = bgmClips[BgmIndex];

        if (bgmSource.clip == null || bgmClips[BgmIndex] == null) { Debug.Log($"No Musics Available in Index {BgmIndex}!"); }

        if (ifLoop)
        {
            bgmSource.loop = true; // 반복 재생
        }
        else
        {
            bgmSource.loop = false;
        }

    }

    /// ********************************
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public void FadeOutBGM(float fadeDuration = 2f)
    {
        StartCoroutine(FadeOutCoroutine(fadeDuration));
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        float startVolume = bgmSource.volume;

        while (bgmSource.volume > 0)
        {
            bgmSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        bgmSource.Stop();
        bgmSource.volume = startVolume;
    }


    public void FadeInBGM(float fadeDuration = 1f)
    {
        StartCoroutine(FadeInCoroutine(fadeDuration, SettedVolume));
    }

    private IEnumerator FadeInCoroutine(float duration, float targetVolume)
    {
        bgmSource.volume = 0f; 
        //bgmSource.Play(); 

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            bgmSource.volume = Mathf.Lerp(0f, targetVolume, elapsedTime / duration);
            yield return null;
        }

        bgmSource.volume = targetVolume; // 최종 볼륨 설정
    }


    private static BGMController instance; // 싱글톤 인스턴스

    void Awake()
    {
        // 기존 인스턴스가 있으면 현재 오브젝트 삭제 (중복 방지)
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // 씬 이동 시 삭제되지 않음
    }

    // 이 위는 bgm
    //*************************************************************************************************************************
    // 이 밑은 효과음

   

    public float Volume_SFX
    {
        get { return volume_SFX; }
        set { volume_SFX = value; }
    }

    /*
    public int Index_SFX
    {
        get { return index_SFX; }
        set { index_SFX = value; }
    }
    */

    public void PlaySFX(int index = 0)
    {
        if (SFXClips[index] == null) { return; }

        SFXSource.PlayOneShot(SFXClips[index]);
    }

    public void SetSFXVolume(float volume)
    {
        SFXSource.volume = volume;
    }
}

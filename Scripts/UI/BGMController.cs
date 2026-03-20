using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    
    public AudioSource bgmSource;
    //public AudioSource SFXSource; //구버전 방식
    public int sfxPoolSize = 10;
    private List <AudioSource> sfxPool = new List <AudioSource>();

    public AudioClip[] bgmClips;
    public AudioClip[] SFXClips;

    public int poolSize = 10;
    protected int bgmIndex = 0;
    private float settedVolume = 0.5f;

    private float volume_SFX = 0.5f;
    //private int index_SFX = 0;
    //private string[] BgmArtist;

    private static BGMController instance; // 싱글톤 인스턴스
    public static BGMController Instance => instance;

    private void InitializePool()
    {
        for (int i = 0; i < sfxPoolSize; i++)
        {
            CreateNewAudioSource();
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        InitializePool(); // 풀 초기화
    }

    private AudioSource CreateNewAudioSource()
    {
        // 새로운 오디오 소스 오브젝트 생성 및 설정
        GameObject go = new GameObject("SFX_Pool_Object");
        go.transform.SetParent(this.transform);
        AudioSource source = go.AddComponent<AudioSource>();
        source.playOnAwake = false;

        sfxPool.Add(source);
        return source;
    }

    private AudioSource GetAvailableSource()
    {
        // 현재 재생 중이지 않은 소스를 찾아서 반환
        for (int i = 0; i < sfxPool.Count; i++)
        {
            if (!sfxPool[i].isPlaying)
            {
                return sfxPool[i];
            }
        }

        // 만약 모든 소스가 사용 중이라면 새로 하나 생성해서 반환 (유동적 확장)
        return CreateNewAudioSource();
    }
    /*
    void Start() //구버전
    {
        bgmSource.clip = bgmClips[bgmIndex];
        bgmSource.loop = true; // 반복 재생

        SFXSource.loop = false;
    }
    */

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


    public void PlayBGM(float fadeDuration = 0.5f)
    {

        if (bgmClips[BgmIndex] == null) { return; }

        if (bgmSource.isPlaying)
        {
            Debug.Log("Already Playing");
            return;
        }
        else
        {
            Debug.Log($"Playing Track : {BgmIndex}, Volume = {settedVolume}");
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

    public void PlaySFX(int index, float pitch = 1.0f)
    {
        if (index < 0 || index >= SFXClips.Length || SFXClips[index] == null) return;

        AudioSource source = GetAvailableSource();
        source.clip = SFXClips[index];
        source.volume = volume_SFX; //현재 설정된 SFX 볼륨 적용
        source.pitch = pitch;       //피치 조절 기능 추가
        source.Play();
    }

    public void SetSFXVolume(float volume)
    {
        volume_SFX = volume;
        // 현재 재생 중인 모든 SFX 소스의 볼륨도 즉시 변경
        foreach (var source in sfxPool)
        {
            source.volume = volume;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    public bool playBGM;
    public int bgmIndex;

    [Range(0f, 1f)]
    private float masterVolume = 1f;
    [Range(0f, 1f)]
    private float bgmVolume = 1f;
    public void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
    private void Update()
    {
        if (!playBGM)
            StopAllBGM();
        else
        {
            if (!bgm[bgmIndex].isPlaying)
            {
                PlayBGM(bgmIndex);
            }
        }
    }
    public void PlaySFX(int _sfxIndex)
    {
        if(_sfxIndex < sfx.Length)
        {
            sfx[_sfxIndex].pitch = Random.Range(.85f, 1.5f);
            sfx[_sfxIndex].Play();
        }
    }

    public void StopSFX(int _index) => sfx[_index].Stop(); 

    public void PlayRandomBGM()
    {
        bgmIndex = Random.Range(0, bgm.Length);
        PlayBGM(bgmIndex);
    }
    public void PlayBGM(int _bgmIndex)
    {
        bgmIndex = _bgmIndex;

        StopAllBGM();
        bgm[bgmIndex].volume = bgmVolume * masterVolume;
        bgm[bgmIndex].Play();
    }
    public void StopAllBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
       
    }
    // 设置主音量
    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        ApplyAllVolumes();
    }

    // 设置BGM音量
    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        
    }
    private void ApplyAllVolumes()
    {
        ApplyBGMVolumes();
        //这里预留SFX
        
    }
    private void ApplyBGMVolumes()
    {
        foreach (AudioSource audio in bgm)
        {
            if (audio != null)
                audio.volume = bgmVolume * masterVolume;
        }
    }
    public float GetMasterVolume() => masterVolume;
    public float GetBGMVolume() => bgmVolume;


}

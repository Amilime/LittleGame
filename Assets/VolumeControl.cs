using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
 

   

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;

        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found!");
            return;
        }

        // 初始化Slider的值
        SetupSliders();

        // 添加监听事件
        AddListeners();
    }

    private void SetupSliders()
    {
        // 设置主音量Slider
        if (masterSlider != null)
        {
            masterSlider.minValue = 0f;
            masterSlider.maxValue = 1f;
            masterSlider.value = audioManager.GetMasterVolume();
          
        }

        // 设置BGM音量Slider
        if (bgmSlider != null)
        {
            bgmSlider.minValue = 0f;
            bgmSlider.maxValue = 1f;
            bgmSlider.value = audioManager.GetBGMVolume();
           
        }

        
    }

    private void AddListeners()
    {
        if (masterSlider != null)
            masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);

        if (bgmSlider != null)
            bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);

       
    }

    // 主音量变化回调
    private void OnMasterVolumeChanged(float value)
    {
        if (audioManager != null)
            audioManager.SetMasterVolume(value);
      
    }

    // BGM音量变化回调
    private void OnBGMVolumeChanged(float value)
    {
        if (audioManager != null)
            audioManager.SetBGMVolume(value);
       
    }

   

   
  

   
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [Header("오디오 소스")]
    [SerializeField] public AudioSource bgmSource = null;
    [SerializeField] public AudioSource sfxSource = null;

    [Header("소리 조절 슬라이더")]
    [SerializeField] public Slider masterSlider = null;
    [SerializeField] public Slider bgmSlider = null;
    [SerializeField] public Slider sfxSlider = null;

    [Header("소리 초기값")]
    private float defaultmasterVol = 1f;
    private float defaultbgmVol = 1f;
    private float defaultsfxVol = 1f;

    void Start()
    {
        try
        {
            //Master 초기값 설정
            PlayerPrefs.SetFloat("MasterVolSize", 1f);
            defaultmasterVol = PlayerPrefs.GetFloat("MasterVolSize");
            masterSlider.value = defaultmasterVol;
            AudioListener.volume = masterSlider.value;

            //BGM 초기값 설정
            PlayerPrefs.SetFloat("BgmVolSize", 1f);
            defaultbgmVol = PlayerPrefs.GetFloat("BgmVolSize");
            bgmSlider.value = defaultbgmVol;
            bgmSource.volume = bgmSlider.value;

            //SFX 초기값 설정
            PlayerPrefs.SetFloat("SfxVolSize", 1f);
            defaultsfxVol = PlayerPrefs.GetFloat("SfxVolSize");
            sfxSlider.value = defaultsfxVol;
            sfxSource.volume = sfxSlider.value;

            PlayerPrefs.Save();
        }
        catch
        {
            Debug.Log("SoundController.Start Error");
        }
    }

    //마스터 볼륨 조절
    public void SetMasterVolume()
    {
        try
        {
            AudioListener.volume = masterSlider.value;

            defaultmasterVol = masterSlider.value;
            PlayerPrefs.SetFloat("MasterVolSize", defaultmasterVol);
            PlayerPrefs.Save();
            Debug.Log("변경된 Master Vol 값 : " + masterSlider.value);
        }
        catch
        {
            Debug.Log("SoundController.SetMasterVolume Error");
        }
    }

    //BGM 볼륨 조절
    public void SetBGMVolume()
    {
        try
        {
            bgmSource.volume = bgmSlider.value;

            defaultbgmVol = bgmSlider.value;
            PlayerPrefs.SetFloat("BgmVolSize", defaultbgmVol);
            PlayerPrefs.Save();
            Debug.Log("변경된 BGM 값 : " + bgmSlider.value);
        }
        catch
        {
            Debug.Log("SoundController.SetBGMVolume Error");
        }
    }

    //SFX 볼륨 조절
    public void SetSFXVolume()
    {
        try
        {
            sfxSource.volume = sfxSlider.value;

            defaultsfxVol = sfxSlider.value;
            PlayerPrefs.SetFloat("SfxVolSize", defaultsfxVol);
            PlayerPrefs.Save();
            Debug.Log("변경된 SFX 값 : " + bgmSlider.value);
        }
        catch
        {
            Debug.Log("SoundController.SetSFXVolume Error");
        }
    }

    //초기화 버튼 클릭 시 뒤바뀐 볼륨값들 전부 초기화
    public void SetResetVolume()
    {
        try
        {
            PlayerPrefs.DeleteKey("MasterVolSize");
            PlayerPrefs.DeleteKey("BgmVolSize");
            PlayerPrefs.DeleteKey("SfxVolSize");

            Start();
        }
        catch
        {
            Debug.Log("SoundController.SetResetVolume Error");
        }
    }
}

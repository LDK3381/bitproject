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

    [Header("소리 초기값(여기서 수정 안함)")]  
    private float defaultmasterVol = 0f;
    private float defaultbgmVol = 0f;
    private float defaultsfxVol = 0f;

    void Awake()
    {
        try
        {
            if (!PlayerPrefs.HasKey("MasterVolSize"))
            {
                //Master 볼륨값이 존재하지 않으면, 초기값 설정
                PlayerPrefs.SetFloat("MasterVolSize", 0.7f);
                masterSlider.value = 0.7f;
                AudioListener.volume = masterSlider.value;
            }
            else
            {
                //존재하면, 이전 Master값 가져오기
                masterSlider.value = PlayerPrefs.GetFloat("MasterVolSize");
                AudioListener.volume = masterSlider.value;
            }

            if (!PlayerPrefs.HasKey("BgmVolSize"))
            {

                //BGM 볼륨값이 존재하지 않으면, 초기값 설정
                PlayerPrefs.SetFloat("BgmVolSize", 0.7f);
                bgmSlider.value = 0.7f;
                bgmSource.volume = bgmSlider.value;
            }
            else
            {
                //존재하면, 이전 BGM값 가져오기
                bgmSlider.value = PlayerPrefs.GetFloat("BgmVolSize");
                bgmSource.volume = bgmSlider.value;
            }

            if (!PlayerPrefs.HasKey("SfxVolSize"))
            {
                //SFX 볼륨값이 존재하지 않으면, 초기값 설정
                PlayerPrefs.SetFloat("SfxVolSize", 0.7f);
                sfxSlider.value = 0.7f;
                sfxSource.volume = sfxSlider.value;
            }
            else
            {
                //존재하면, 이전 SFX값 가져오기
                sfxSlider.value = PlayerPrefs.GetFloat("SfxVolSize");
                sfxSource.volume = sfxSlider.value;
            }

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

            //Master 초기값 설정
            PlayerPrefs.SetFloat("MasterVolSize", 0.7f);
            masterSlider.value = 0.7f;
            AudioListener.volume = masterSlider.value;

            //BGM 초기값 설정
            PlayerPrefs.SetFloat("BgmVolSize", 0.7f);
            bgmSlider.value = 0.7f;
            bgmSource.volume = bgmSlider.value;

            //SFX 초기값 설정
            PlayerPrefs.SetFloat("SfxVolSize", 0.7f);
            sfxSlider.value = 0.7f;
            sfxSource.volume = sfxSlider.value;

            PlayerPrefs.Save();
        }
        catch
        {
            Debug.Log("SoundController.SetResetVolume Error");
        }
    }
}


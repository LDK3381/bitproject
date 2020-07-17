using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public class Sound
{
    public string soundName = "";
    public AudioClip clip = null;  // 사운드를 담는 클립
}

public class SoundManager : MonoBehaviour
{
    //클래스를 쉽게 접근하도록 static으로 공유
    public static SoundManager instance;

    [Header("사운드 등록")]
    [SerializeField] Sound[] bgmSounds = null; // 브금 사운드
    [SerializeField] Sound[] sfxSounds = null; // 효과음 사운드

    [Header("브금 플레이어")]
    [SerializeField] public AudioSource bgmPlayer;

    [Header("효과음 플레이어")]
    [SerializeField] public AudioSource[] sfxPlayer;

    void Start()
    {
        instance = this;
    }

    //효과음 플레이 함수
    public void PlaySE(string _soundName)
    {
        for (int i = 0; i < sfxSounds.Length; i++)
        {
            //일치되는 효과음 찾기
            if (_soundName == sfxSounds[i].soundName)
            {
                //재생되지 않는 효과음 찾기
                for (int x = 0; x < sfxPlayer.Length; x++)
                {
                    if (!sfxPlayer[x].isPlaying)
                    {
                        sfxPlayer[x].clip = sfxSounds[i].clip;
                        sfxPlayer[x].volume = PlayerPrefs.GetFloat("SfxVolSize");   //변경된 BGM 음량 적용
                        sfxPlayer[x].Play();
                        return;
                    }
                }
                Debug.Log("모든 효과음 플레이어가 사용중입니다!!!");
                return;
            }
        }
        Debug.Log("등록된 효과음이 없습니다");
    }

    // 브금 랜덤 플레이 함수
    public void PlayRandomBGM()
    {
        int random = Random.Range(0, 2);
        bgmPlayer.clip = bgmSounds[random].clip;
        bgmPlayer.volume = PlayerPrefs.GetFloat("BgmVolSize");  //변경된 SFX 음량 적용
        bgmPlayer.Play();

        if(!bgmPlayer.isPlaying)
        {
            Debug.Log("bgm이 나오질 않습니다!");
        }
    }
}
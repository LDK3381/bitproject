using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{

    [Header("볼륨 환경설정")]
    [SerializeField] AudioMixer masterMixer = null;
    [SerializeField] Slider masterSlider = null;  

    //음량조절(환경설정)
    public void VolumeControl()
    {
        float sound_master = masterSlider.value;

        if (sound_master == -20f)
            masterMixer.SetFloat("MyMaster", -80);
        else
            masterMixer.SetFloat("MyMaster", sound_master);
    }

    //화질 설정
    public void QualityControl(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //전체화면
    public void FullScreenControl(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

}

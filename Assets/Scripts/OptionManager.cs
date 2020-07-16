using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public Dropdown resolutionDropdown = null;
    public Toggle fullscreenToggle = null;
    Resolution[] resolutions = null;
    public Text resolutionText = null;

    void OnEnable()
    {
        //값이 변할때마다 AddListener을 통해 해당 함수 발동(인자가 없으면 그냥 함수명만, 있으면 delegate 활용)
        fullscreenToggle.onValueChanged.AddListener(delegate { SetFullScreen(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(); });
        //textureQualityDropdown.onValueChanged.AddListener(delegate { SetTextureQuality(); });

        //선택 가능한 해상도가 리스트에 추가
        resolutions = Screen.resolutions;
        foreach(Resolution res in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(res.ToString()));
        }
        resolutionText.text = resolutions.ToString();
    }

    //처음 킬 때 최근에 바꾼 해상도 값 유지
    void Start()
    {
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution", 0);
    }

    //해상도 설정
    public void SetResolution()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, 
            resolutions[resolutionDropdown.value].height, Screen.fullScreen);

        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.Save();
    }

    //전체화면 설정
    public void SetFullScreen()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
    }
}

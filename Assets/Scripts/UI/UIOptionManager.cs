using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIOptionManager : MonoBehaviour
{
    public Dropdown resolutionDropdown = null;
    public Toggle fullscreenToggle = null;
    Resolution[] resolutions = null;
    public Text resolutionText = null;

    void OnEnable()
    {
        try
        {
            //값이 변할때마다 AddListener을 통해 해당 함수 발동(인자가 없으면 그냥 함수명만, 있으면 delegate 활용)
            fullscreenToggle.onValueChanged.AddListener(delegate { SetFullScreen(); });
            resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(); });
            //textureQualityDropdown.onValueChanged.AddListener(delegate { SetTextureQuality(); });

            //선택 가능한 해상도가 리스트에 추가
            resolutions = Screen.resolutions;
            foreach (Resolution res in resolutions)
            {
                resolutionDropdown.options.Add(new Dropdown.OptionData(res.ToString()));
            }
            resolutionText.text = resolutions.ToString();
        }
        catch
        {
            Debug.Log("UIOptionManager.OnEnable Error");
        }
    }

    void Start()
    {
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution", 0);
    }

    //해상도 설정
    public void SetResolution()
    {
        try
        {
            Screen.SetResolution(resolutions[resolutionDropdown.value].width,
            resolutions[resolutionDropdown.value].height, Screen.fullScreen);

            PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
            PlayerPrefs.Save();
        }
        catch
        {
            Debug.Log("UIOptionManager.SetResolution Error");
        }
    }

    //전체화면 설정
    public void SetFullScreen()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
    }
}

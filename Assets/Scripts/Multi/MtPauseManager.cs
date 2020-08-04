using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class MtPauseManager : MonoBehaviourPun
{
    [Header("일시정지")]
    [SerializeField] GameObject pausePanel  = null;      //일시정지 화면
    [SerializeField] GameObject pauseButton = null;     //일시정지 버튼(톱니바퀴)

    [Header("화면 옵션")]
    [SerializeField] Toggle fullscreenToggle = null;

    [Header("소리 옵션")]
    [SerializeField] Slider      masterSlider = null;
    [SerializeField] Slider      bgmSlider    = null;
    [SerializeField] Slider      sfxSlider    = null;
    [SerializeField] AudioSource bgmSource    = null;
    [SerializeField] AudioSource sfxSource    = null;

    [Header("현재 소리값")]
    private float curmasterVol = 1f;
    private float curbgmVol    = 1f;
    private float cursfxVol    = 1f;

    void OnEnable()
    {
        try
        {
            //값이 변할때마다 AddListener을 통해 해당 함수 발동(인자가 없으면 그냥 함수명만, 있으면 delegate 활용)
            fullscreenToggle.onValueChanged.AddListener(delegate { SetFullScreen(); });

            pausePanel.SetActive(false);
            pauseButton.SetActive(true);
        }
        catch
        {
            Debug.Log("MtPauseManager.OnEnable Error");
        }
    }

    void Start()
    {
        try
        {
            //일시정지 화면 내 소리 슬라이더 값 초기설정
            curmasterVol = PlayerPrefs.GetFloat("MasterVolSize");
            masterSlider.value = curmasterVol;
            AudioListener.volume = masterSlider.value;

            curbgmVol = PlayerPrefs.GetFloat("BgmVolSize");
            bgmSlider.value = curbgmVol;
            bgmSource.volume = bgmSlider.value;

            cursfxVol = PlayerPrefs.GetFloat("SfxVolSize");
            sfxSlider.value = cursfxVol;
            sfxSource.volume = sfxSlider.value;
        }
        catch
        {
            Debug.Log("MtPauseManager.Start Error");
        }
    }

    void Update()
    {
        try
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pausePanel.SetActive(!pausePanel.activeSelf);
                pauseButton.SetActive(!pauseButton.activeSelf);
            }
        }
        catch
        {
            Debug.Log("MtPauseManager.Update Error");
        }
    }

    #region 버튼들
    //일시정지 버튼
    public void PauseGame()
    {
        try
        {
            pausePanel.SetActive(true);
            pauseButton.SetActive(false);
        }
        catch
        {
            Debug.Log("MtPauseManager.PauseGame Error");
        }
    }

    //뒤로가기 버튼
    public void OnBack()
    {
        try
        {
            pausePanel.SetActive(false);
            pauseButton.SetActive(true);
        }
        catch
        {
            Debug.Log("MtPauseManager.OnBack Error");
        }
    }

    //게임 나가기 버튼
    public void ExitGame()
    {
        try
        {
            Debug.Log("게임 나가기");
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("UI");
        }
        catch
        {
            Debug.Log("MtPauseManager.ExitGame Error");
        }
    }
    #endregion

    #region 옵션 기능들
    //전체화면 설정
    public void SetFullScreen()
    {
        try
        {
            Screen.fullScreen = fullscreenToggle.isOn;
        }
        catch
        {
            Debug.Log("MtPauseManager.SetFullScreen Error");
        }
    }

    //마스터 볼륨 조절
    public void SetMasterVolume()
    {
        try
        {
            AudioListener.volume = masterSlider.value;

            curmasterVol = masterSlider.value;
            PlayerPrefs.SetFloat("MasterVolSize", curmasterVol);
            PlayerPrefs.Save();
            Debug.Log("변경된 Master Vol 값 : " + masterSlider.value);
        }
        catch
        {
            Debug.Log("MtPauseManager.SetMasterVolume Error");
        }  
    }

    //BGM 볼륨 조절
    public void SetBGMVolume()
    {
        try
        {
            bgmSource.volume = bgmSlider.value;

            PlayerPrefs.SetFloat("BgmVolSize", bgmSlider.value);
            PlayerPrefs.Save();
            Debug.Log("변경된 BGM 값 : " + bgmSlider.value);
        }
        catch
        {
            Debug.Log("MtPauseManager.SetBGMVolume Error");
        }
    }

    //SFX 볼륨 조절
    public void SetSFXVolume()
    {
        try
        {
            sfxSource.volume = sfxSlider.value;

            cursfxVol = sfxSlider.value;
            PlayerPrefs.SetFloat("SfxVolSize", cursfxVol);
            PlayerPrefs.Save();
            Debug.Log("변경된 SFX 값 : " + sfxSlider.value);
        }
        catch
        {
            Debug.Log("MtPauseManager.SetSFXVolume Error");
        }
    }
    #endregion
}

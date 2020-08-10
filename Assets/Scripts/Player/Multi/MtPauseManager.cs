using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class MtPauseManager : MonoBehaviourPun
{
    [Header("일시정지")]
    [SerializeField] GameObject pausePanel = null;      //일시정지 화면
    [SerializeField] GameObject pauseButton = null;     //일시정지 버튼(톱니바퀴)

    [Header("화면 옵션")]
    [SerializeField] Toggle fullscreenToggle = null;

    [Header("소리 옵션")]
    [SerializeField] Slider masterSlider = null;
    [SerializeField] Slider bgmSlider = null;
    [SerializeField] Slider sfxSlider = null;
    [SerializeField] AudioSource bgmSource = null;
    [SerializeField] AudioSource sfxSource = null;

    [Header("현재 소리값")]
    private float curmasterVol = 1f;
    private float curbgmVol = 1f;
    private float cursfxVol = 1f;

    void OnEnable()
    {
        //값이 변할때마다 AddListener을 통해 해당 함수 발동(인자가 없으면 그냥 함수명만, 있으면 delegate 활용)
        fullscreenToggle.onValueChanged.AddListener(delegate { SetFullScreen(); });

        pausePanel.SetActive(false);
    }

    void Start()
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
    }

    #region 버튼들
    //일시정지 버튼
    public void PauseGame()
    {
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    //뒤로가기 버튼
    public void OnBack()
    {
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }
    #endregion

    #region 옵션 기능들
    //전체화면 설정
    public void SetFullScreen()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
    }

    //마스터 볼륨 조절
    public void SetMasterVolume()
    {
        AudioListener.volume = masterSlider.value;

        curmasterVol = masterSlider.value;
        PlayerPrefs.SetFloat("MasterVolSize", curmasterVol);
        PlayerPrefs.Save();
        Debug.Log("변경된 Master Vol 값 : " + masterSlider.value);
    }

    //BGM 볼륨 조절
    public void SetBGMVolume()
    {
        bgmSource.volume = bgmSlider.value;

        PlayerPrefs.SetFloat("BgmVolSize", bgmSlider.value);
        PlayerPrefs.Save();
        Debug.Log("변경된 BGM 값 : " + bgmSlider.value);
    }

    //SFX 볼륨 조절
    public void SetSFXVolume()
    {
        sfxSource.volume = sfxSlider.value;

        cursfxVol = sfxSlider.value;
        PlayerPrefs.SetFloat("SfxVolSize", cursfxVol);
        PlayerPrefs.Save();
        Debug.Log("변경된 SFX 값 : " + sfxSlider.value);
    }
    #endregion
}

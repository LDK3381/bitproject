using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SgPauseManager : MonoBehaviour
{
    [Header("일시정지")]
    [SerializeField] GameObject pausePanel  = null;      //일시정지 화면
    [SerializeField] GameObject pauseButton = null;     //일시정지 버튼(톱니바퀴)

    [Header("화면 옵션")]
    [SerializeField] Toggle fullscreenToggle = null;

    [Header("소리 옵션")]
    [SerializeField] Slider masterSlider    = null;
    [SerializeField] Slider bgmSlider       = null;
    [SerializeField] Slider sfxSlider       = null;
    [SerializeField] AudioSource bgmSource  = null;
    [SerializeField] AudioSource sfxSource  = null;

    [Header("현재 소리값")]
    private float curmasterVol  = 1f;
    private float curbgmVol     = 1f;
    private float cursfxVol     = 1f;

    [Header("봉인시킬 기능들")]
    [SerializeField] SgGunController sealGunControll = null;
    [SerializeField] GameObject sealControll         = null;
    [SerializeField] GameObject sealWeapon           = null;
    [SerializeField] GameObject sealBomb             = null;
    [SerializeField] GameObject sealMouseRotate      = null;
    [SerializeField] AISpawn sealSpawn               = null;
    [SerializeField] GameObject sealAITimer          = null;
    [SerializeField] AIController aiController       = null;

    void OnEnable()
    {
        try
        {
            //값이 변할때마다 AddListener을 통해 해당 함수 발동(인자가 없으면 그냥 함수명만, 있으면 delegate 활용)
            fullscreenToggle.onValueChanged.AddListener(delegate { SetFullScreen(); });

            pausePanel.SetActive(false);
        }
        catch
        {
            Debug.Log("SgPauseManager.OnEnable Error");
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
            Debug.Log("SgPauseMmanager.Start Error");
        }
    }

    #region 버튼들
    //일시정지 버튼
    public void PauseGame()
    {
        try
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            pauseButton.SetActive(false);
            AudioListener.pause = true;     //bgm 일시중단

            //일시정지 중에는 키 입력 기능들 봉인
            SealKey();
        }
        catch
        {
            Debug.Log("SgPauseManager.PauseGame Error");
        }
    }

    //뒤로가기 버튼
    public void OnBack()
    {
        try
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            pauseButton.SetActive(true);
            AudioListener.pause = false;    //bgm 다시재생

            //일시정지 풀리면 키 입력 기능들 봉인해제
            UnSealKey();
        }
        catch
        {
            Debug.Log("SgPauseManager.OnBack Error");
        }
    }

    public void UnSealKey()
    {
        try
        {
            sealGunControll.GetComponent<SgGunController>().enabled = true;
            sealControll.GetComponent<SgPlayerController>().enabled = true;
            sealWeapon.GetComponent<SgWeaponManager>().enabled = true;
            sealBomb.GetComponent<SgBombSpawn>().enabled = true;
            sealMouseRotate.GetComponent<SgMouseRotate>().enabled = true;
            sealAITimer.GetComponent<AITimer>().enabled = true;
            sealSpawn.GetComponent<AISpawn>().enabled = true;
            aiController.GetComponent<AIController>().enabled = true;
            aiController.GetComponent<AITurretController>().enabled = true;
        }
        catch
        {
            Debug.Log("SgPauseManager.UnSealKey Error");
        }
    }

    public void SealKey()
    {
        try
        {
            sealGunControll.GetComponent<SgGunController>().enabled = false;
            sealControll.GetComponent<SgPlayerController>().enabled = false;
            sealWeapon.GetComponent<SgWeaponManager>().enabled = false;
            sealBomb.GetComponent<SgBombSpawn>().enabled = false;
            sealMouseRotate.GetComponent<SgMouseRotate>().enabled = false;
            sealAITimer.GetComponent<AITimer>().enabled = false;
            sealSpawn.GetComponent<AISpawn>().enabled = false;
            aiController.GetComponent<AIController>().enabled = false;
            aiController.GetComponent<AITurretController>().enabled = false;
        }
        catch
        {
            Debug.Log("SgPauseManager.SealKey Error");
        }
    }

    //게임 나가기 버튼
    public void ExitGame()
    {
        try
        {
            Time.timeScale = 1;     //화면 바뀔 때 정지상태가 유지되는 현상 방지
            AudioListener.pause = false;    //bgm 다시재생
            Debug.Log("게임 나가기");
            SceneManager.LoadScene("UI");

        }
        catch
        {
            Debug.Log("SgPauseManager.ExitGame Error");
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
            Debug.Log("SgPauseManager.SetFullScreen Error");
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
            Debug.Log("SgPauseManager.SetMasterVolume Error");
        }
    }

    //BGM 볼륨 조절
    public void SetBGMVolume()
    {
        try
        {
            bgmSource.volume = bgmSlider.value;

            curbgmVol = bgmSlider.value;
            PlayerPrefs.SetFloat("BgmVolSize", curbgmVol);
            PlayerPrefs.Save();
            Debug.Log("변경된 BGM 값 : " + bgmSlider.value);
        }
        catch
        {
            Debug.Log("SgPauseManager.SetBGMVolume Error");
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
            Debug.Log("SgPauseManager.SetSFXVolume Error");
        }
    }
    #endregion
}

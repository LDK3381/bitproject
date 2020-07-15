using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform buttonScale = null;                //버튼 사이즈
    Vector3 defaultScale = new Vector3();               //현재 버튼크기

    //public AudioClip btnSound = null;
    //public AudioSource source = null;

    void Start()
    {
        defaultScale = buttonScale.localScale;
    }    

    //버튼에 마우스 갖다대면 버튼 사이즈 커짐
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    //마우스를 버튼 밖에 두면 사이즈 원래대로
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }

    ////버튼음 효과
    //public void PlaySound()
    //{
    //    source.PlayOneShot(btnSound);
    //}
}
